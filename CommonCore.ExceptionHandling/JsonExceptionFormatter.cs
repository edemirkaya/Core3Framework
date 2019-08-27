using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Reflection;
using System.Security;
using System.Security.Principal;

namespace CommonCore.ExceptionHandling
{
    public class JsonExceptionFormatter
    {
        public string GetMessage(Exception exception)
        {
            var environment = ProcessEnvironment();

            Dictionary<string, object> detail = null;
            if (exception != null)
            {
                detail = ProcessException(exception);
            }

            dynamic result = new ExpandoObject();
            result.Environment = environment;
            result.Exception = detail;

            return JsonConvert.SerializeObject(result);
        }

        private static Dictionary<string, object> ProcessException(Exception currException)
        {
            var result = new Dictionary<string, object>
            {
                {"ExceptionType", currException.GetType().FullName}
            };

            PropertyInfo[] arrPublicProperties = currException.GetType().GetProperties();
            foreach (PropertyInfo propinfo in arrPublicProperties)
            {
                if (propinfo.CanRead /*&& propinfo.GetIndexParameters().Length == 0*/)
                {
                    object propValue;
                    try
                    {
                        propValue = propinfo.GetValue(currException, null);
                    }
                    catch (TargetInvocationException)
                    {
                        propValue = "Access failed";
                    }

                    if (propValue == null)
                    {
                        result.Add(propinfo.Name, "Null");
                    }
                    else
                    {
                        if (propValue.GetType().ToString() == "System.Reflection.RuntimeMethodInfo")
                            continue;
                        if (!string.Equals(propinfo.Name, "InnerException", StringComparison.Ordinal))
                        {
                            if (string.Equals(propinfo.Name, "AdditionalInformation", StringComparison.Ordinal))
                            {
                                var currAdditionalInfo = propValue as NameValueCollection;

                                if (currAdditionalInfo != null && currAdditionalInfo.Count > 0)
                                {
                                    for (int i = 0; i < currAdditionalInfo.Count; i++)
                                    {
                                        result.Add(currAdditionalInfo.GetKey(i), currAdditionalInfo[i]);
                                    }
                                }
                            }
                            else
                            {
                                result.Add(propinfo.Name, propValue);
                            }
                        }
                        else
                        {
                            var ex = propValue as Exception;
                            if (ex != null)
                            {
                                result.Add(propinfo.Name, ProcessException(ex));
                            }
                            else
                            {
                                result.Add(propinfo.Name, propValue);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private Dictionary<string, string> ProcessEnvironment()
        {
            var summary = new Dictionary<string, string>()
            {
                {"MachineName", GetMachineName()},
                {"TimeStamp", DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")},
                {"FullName", Assembly.GetExecutingAssembly().FullName},
                {"AppDomainName", AppDomain.CurrentDomain.FriendlyName},
                {"WindowsIdentity", GetWindowsIdentity()}
            };

            return summary;
        }

        private static string GetWindowsIdentity()
        {
            try
            {
                var windowsIdentity = WindowsIdentity.GetCurrent();
                if (windowsIdentity != null)
                    return windowsIdentity.Name;
                return null;
            }
            catch (SecurityException)
            {
                return "Permission Denied";
            }
        }

        private static string GetMachineName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (SecurityException)
            {
                return "Permission Denied";
            }
        }
    }
}
