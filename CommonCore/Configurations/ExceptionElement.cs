namespace CommonCore.Configurations
{
    public class ExceptionElement
    { /// <summary>
      /// Individual exception setting element for exceptions defined in a policy.
      /// </summary>
      /// <remarks>
      /// Use this setting to cutomize the returned exception details in a service fault for a single exception type
      /// </remarks>
        //public class ExceptionElement : ConfigurationElement
        //{
        //    /// <summary>
        //    /// Type name of the exception to handle
        //    /// </summary>
        //    /// <remarks>
        //    /// Use full name of the type, such as "System.Security.SecurityException"
        //    /// </remarks>
        //    [ConfigurationProperty("Type", IsKey = true, IsRequired = true)]
        //    public string Type
        //    {
        //        get
        //        {
        //            return Convert.ToString(this["Type"]);
        //        }
        //        set
        //        {
        //            this["Type"] = value;
        //        }
        //    }

        //    /// <summary>
        //    /// The suggested handling action of this exception
        //    /// </summary>
        //    [ConfigurationProperty("HandlingAction", IsRequired = true)]
        //    public HandlingAction HandlingAction
        //    {
        //        get
        //        {
        //            return (HandlingAction)this["HandlingAction"];
        //        }
        //        set
        //        {
        //            this["HandlingAction"] = value;
        //        }
        //    }

        //    /// <summary>
        //    /// Replacement message for the original error message
        //    /// </summary>
        //    [ConfigurationProperty("ReplaceMessage", IsRequired = true)]
        //    public string ReplaceMessage
        //    {
        //        get
        //        {
        //            return (string)this["ReplaceMessage"];
        //        }
        //        set
        //        {
        //            this["ReplaceMessage"] = value;
        //        }
        //    }

        //    /// <summary>
        //    /// Custom error code for this exception
        //    /// </summary>
        //    [ConfigurationProperty("ErrorCode", IsRequired = true)]
        //    public string ErrorCode
        //    {
        //        get
        //        {
        //            return (string)this["ErrorCode"];
        //        }
        //        set
        //        {
        //            this["ErrorCode"] = value;
        //        }
        //    }

        //    /// <summary>
        //    /// Determines if exception info will be logged on server side or not
        //    /// </summary>
        //    [ConfigurationProperty("LogAfterHandling", IsRequired = true)]
        //    public bool LogAfterHandling
        //    {
        //        get
        //        {
        //            return (bool)this["LogAfterHandling"];
        //        }
        //        set
        //        {
        //            this["LogAfterHandling"] = value;
        //        }
        //    }

        //    /// <summary>
        //    /// Determines if the server will attach detailed debug info to the fault message sent to the service client
        //    /// </summary>
        //    /// <remarks>
        //    /// Default is false, client will receive detailed error message.
        //    /// </remarks>
        //    [ConfigurationProperty("HideDebugInfo", IsRequired = false)]
        //    public bool HideDebugInfo
        //    {
        //        get
        //        {
        //            return (bool)this["HideDebugInfo"];
        //        }
        //        set
        //        {
        //            this["HideDebugInfo"] = value;
        //        }
        //    }

        //    [ConfigurationProperty("ExceptionTypes")]
        //    public ExceptionsCollection ExceptionTypes
        //    {
        //        get
        //        {
        //            return (ExceptionsCollection)base["ExceptionTypes"];
        //        }
        //    }
    }
}