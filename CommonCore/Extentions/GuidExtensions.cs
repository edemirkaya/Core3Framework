﻿using System;

namespace CommonCore.Extentions
{
    public static class GuidExtensions
    {
        public static Boolean IsNullOrEmpty(this Guid guid)
        {
            var result = guid == null || guid == Guid.Empty;

            return result;
        }
    }
}
