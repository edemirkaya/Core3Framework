using CommonCore.Helpers;
using CommonCore.Interfaces;
using CommonCore.Server.Infrastructure;
using CommonCore.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonCore.Server.Base
{
    public abstract class ContextBase : DbContext 
    {
        protected static Dictionary<string, HashSet<string>> NonAuditables = new Dictionary<string, HashSet<string>>();

        private bool AuditInJson;

        public string userName;
        public int userId;

        protected ContextBase(DbContextOptions options) : base(options)
        {

        }

        public AuditContext AuditContext { get; set; }
        public IEnumerable<IAuditTableEntity> AuditTables { get; set; }

        public override int SaveChanges()
        {

            var auditLogEntities = new List<Tuple<EntityEntry, IAuditLogEntity>>();

            if (AuditTables != null)
            {
                foreach (
                    EntityEntry entity in
                        ChangeTracker.Entries())
                //.Where(p => AuditTables.Any(k => k.GetEntityType() == p.Entity.GetType())))
                {
                    Dictionary<string, string> oldValues = null;
                    Dictionary<string, string> newValues = null;
                    string dbOperationType = null;

                    if (entity.State == EntityState.Added || entity.State == EntityState.Modified || entity.State == EntityState.Deleted)
                    {
                        IEnumerable<string> excludedHash = GetExcludedProperties(entity);
                        List<string> excluded = excludedHash.ToList();
                        switch (entity.State)
                        {
                            case EntityState.Added:
                                {
                                    dbOperationType = DbOperationType.Insert;
                                    newValues = entity.CurrentValues.Properties.Where(p => !excluded.Contains(p.Name)).Select(p => new { Key = p.Name, Val = GetPropertyValueString(p.Name, entity.CurrentValues[p.Name]) })
                                        .ToDictionary(x => x.Key, x => x.Val);
                                }
                                break;
                            case EntityState.Modified:
                                {
                                    dbOperationType = DbOperationType.Update;
                                    PropertyValues dbValues = entity.GetDatabaseValues();
                                    GetChangedProperties(dbValues, entity.CurrentValues, excluded, out oldValues, out newValues);
                                }
                                break;
                            case EntityState.Deleted:
                                {
                                    dbOperationType = DbOperationType.Delete;
                                    PropertyValues dbValues = entity.GetDatabaseValues();


                                    oldValues = dbValues.Properties.Where(p => !excluded.Contains(p.Name)).Select(
                                        propName => new { Key = propName.Name, Val = GetPropertyValueString(propName.Name, dbValues[propName]) })
                                        .ToDictionary(x => x.Key, x => x.Val);
                                }
                                break;

                            default:
                                continue;
                        }
                    }
                    else
                        continue;

                    IAuditLogEntity auditLogEntity = GetAuditLogEntity();
                    //auditLogEntity.IliskiId = AuditContext.CorrelationId;
                    auditLogEntity.TableName = GetTableNameString(entity);
                    auditLogEntity.ProcessType = dbOperationType;
                    auditLogEntity.ProcessOwner = userName;
                    auditLogEntity.ProcessTime = DateTime.Now;
                    auditLogEntity.OldValues = oldValues == null ? null : GetAuditString(oldValues);
                    auditLogEntity.NewValues = newValues == null ? null : GetAuditString(newValues);
                    //auditLogEntity.EkVeri = AuditContext.AdditionalData == null ? null : GetAuditString(AuditContext.AdditionalData);
                    if (entity.State != EntityState.Added)
                        auditLogEntity.KeyValues = GetKeyFieldsValuesString(entity);

                    auditLogEntities.Add(Tuple.Create(entity, auditLogEntity));
                }
            }

            int result = base.SaveChanges();

            try
            {
                foreach (var auditLogEntity in auditLogEntities)
                {
                    if (auditLogEntity.Item2.ProcessType == DbOperationType.Insert)
                        auditLogEntity.Item2.KeyValues = GetKeyFieldsValuesString(auditLogEntity.Item1);
                    AddAuditLogEntityToDbSet(auditLogEntity.Item2);
                }
            }
            catch
            {
                // Silently Supress 
            }

            base.SaveChanges();

            return result;
        }

        private string GetAuditString(Dictionary<string, string> data)
        {
            if (AuditInJson)
            {
                return JsonConvert.SerializeObject(data);
            }
            return string.Join(",", data.Select(i => $"{i.Key}:\"{i.Value}\""));
        }

        private HashSet<string> GetExcludedProperties(EntityEntry entity)
        {

            var entityType = entity.Entity.GetType();
            var entityName = entityType.FullName;

            HashSet<string> excluded;
            if (NonAuditables.TryGetValue(entityName, out excluded))
                return excluded;

            excluded = new HashSet<string>();
            NonAuditables.Add(entityName, excluded); // Caching to avoid excessive reflection
            PropertyValues propertyValues = null;
            if (entity.State != EntityState.Deleted)
            {
                propertyValues = entity.CurrentValues;
            }
            else
            {
                propertyValues = entity.OriginalValues;
            }
            foreach (var propertyName in propertyValues.Properties)
            {
                PropertyInfo pi = entityType.GetProperty(propertyName.Name);
                if (Attribute.IsDefined(pi, typeof(NonAuditableAttribute)))
                {
                    excluded.Add(propertyName.Name);
                }
            }

            return excluded;

        }

        protected abstract IAuditLogEntity GetAuditLogEntity();
        protected abstract void AddAuditLogEntityToDbSet(IAuditLogEntity entity);


        protected static void GetChangedProperties(PropertyValues dbValues, PropertyValues currentValues, List<string> excluded,
            out Dictionary<string, string> oldValues, out Dictionary<string, string> newValues)
        {
            oldValues = new Dictionary<string, string>();
            newValues = new Dictionary<string, string>();

            foreach (
                var propName in
                    currentValues.Properties.Where(p => !excluded.Contains(p.Name)
                    && ByValueComparer.Default.Compare(dbValues[p.Name], currentValues[p.Name]) != 0))
            {
                oldValues.Add(propName.Name, GetPropertyValueString(propName.Name, dbValues[propName]));
                newValues.Add(propName.Name, GetPropertyValueString(propName.Name, currentValues[propName]));
            }
        }

        protected string GetKeyFieldsValuesString(EntityEntry entity)
        {
            IDictionary<string, object> keyWValues = entity.Metadata.FindPrimaryKey()
             .Properties
             .ToDictionary(x => x.Name, x => entity.Property(x.Name).CurrentValue);
            string s = string.Join(";", keyWValues.Select(x => x.Key + "=" + x.Value).ToArray());
            return s;
        }

        protected static string GetTableNameString(EntityEntry entity)
        {
            Type entityType = entity.Entity.GetType();
            return entityType.FullName.StartsWith("System.Data.Entity.DynamicProxies") &&
                   entityType.BaseType != null
                ? entityType.BaseType.Name
                : entityType.Name;
        }


        protected static string GetPropertyValueString(string propName, object value)
        {
            string valueString;
            if (value == null)
                valueString = "NULL";
            else if (value is byte[])
                valueString = "[varbinary]";
            else
            {
                valueString = value.ToString();
                if (valueString.Length > 1024)
                    valueString = "[varchar(MAX)]";
            }

            return valueString;
        }

        public void SetModified<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected static class DbOperationType
        {
            internal const string Insert = "Insert";
            internal const string Update = "Update";
            internal const string Delete = "Delete";
        }



    }
}
