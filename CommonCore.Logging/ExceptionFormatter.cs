using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace CommonCore.Logging
{
    public class ExceptionFormatter
    {
        public string Header = "HEADER";

        private const string LineSeparator = "======================================";
        private readonly NameValueCollection additionalInfo;
        private readonly string applicationName;

        public ExceptionFormatter()
            : this(new NameValueCollection(), string.Empty)
        {
        }

        public ExceptionFormatter(string applicationName)
            : this(new NameValueCollection(), applicationName)
        {
        }

        public ExceptionFormatter(NameValueCollection additionalInfo, string applicationName)
        {
            this.additionalInfo = additionalInfo;
            this.applicationName = applicationName;
        }

        public string GetMessage(Exception exception)
        {
            var eventInformation = new StringBuilder();
            CollectAdditionalInfo();

            eventInformation.AppendFormat("{0}\n\n", additionalInfo.Get(Header));

            eventInformation.AppendFormat("\n{0} {1}:\n{2}",
                                          "Summary for", applicationName, LineSeparator);

            foreach (string info in additionalInfo)
            {
                if (info != Header)
                {
                    eventInformation.AppendFormat("\n--> {0}", additionalInfo.Get(info));
                }
            }

            if (exception != null)
            {
                Exception currException = exception;

                do
                {
                    eventInformation.AppendFormat("\n\n{0}\n{1}", "Exception Information Details:", LineSeparator);
                    eventInformation.AppendFormat("\n{0}: {1}", "Exception Type", currException.GetType().FullName);

                    ReflectException(currException, eventInformation);

                    if (currException.StackTrace != null)
                    {
                        eventInformation.AppendFormat("\n\n{0} \n{1}",
                                                      "StackTrace Information Details:", LineSeparator);
                        eventInformation.AppendFormat("\n{0}", currException.StackTrace);
                    }

                    currException = currException.InnerException;

                } while (currException != null);
            }

            return eventInformation.ToString();
        }

        private static void ReflectException(Exception currException, StringBuilder strEventInfo)
        {
            PropertyInfo[] arrPublicProperties = currException.GetType().GetProperties();
            foreach (PropertyInfo propinfo in arrPublicProperties)
            {
                if (propinfo.Name != "InnerException" && propinfo.Name != "StackTrace")
                {
                    if (propinfo.CanRead && propinfo.GetIndexParameters().Length == 0)
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
                            strEventInfo.AppendFormat("\n{0}: NULL", propinfo.Name);
                        }
                        else
                        {
                            ProcessAdditionalInfo(propinfo, propValue, strEventInfo);
                        }
                    }
                }
            }
        }

        private static void ProcessAdditionalInfo(PropertyInfo propinfo, object propValue, StringBuilder stringBuilder)
        {
            if (propinfo.Name == "AdditionalInformation")
            {
                if (propValue != null)
                {
                    var currAdditionalInfo = (NameValueCollection)propValue;

                    if (currAdditionalInfo.Count > 0)
                    {
                        stringBuilder.AppendFormat("\nAdditionalInformation:");

                        for (int i = 0; i < currAdditionalInfo.Count; i++)
                        {
                            stringBuilder.AppendFormat("\n{0}: {1}", currAdditionalInfo.GetKey(i), currAdditionalInfo[i]);
                        }
                    }
                }
            }
            else
            {
                stringBuilder.AppendFormat("\n{0}: {1}", propinfo.Name, propValue);
            }
        }

        private void CollectAdditionalInfo()
        {
            if (additionalInfo["MachineName:"] != null)
            {
                return;
            }

            additionalInfo.Add("MachineName:", "MachineName: " + GetMachineName());
            additionalInfo.Add("TimeStamp:", "TimeStamp: " + DateTime.UtcNow.ToString(CultureInfo.CurrentCulture));
            additionalInfo.Add("FullName:", "FullName: " + Assembly.GetExecutingAssembly().FullName);
            additionalInfo.Add("AppDomainName:", "AppDomainName: " + AppDomain.CurrentDomain.FriendlyName);
            additionalInfo.Add("WindowsIdentity:", "WindowsIdentity: " + GetWindowsIdentity());
        }

        private static string GetWindowsIdentity()
        {
            try
            {
                var windowsIdentity = WindowsIdentity.GetCurrent();
                if (windowsIdentity != null) return windowsIdentity.Name;
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
