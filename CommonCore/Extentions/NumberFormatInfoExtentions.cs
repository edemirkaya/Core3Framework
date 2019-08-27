using System;
using System.Globalization;

namespace CommonCore.Extentions
{
    public static class NumberFormatInfoExtentions
    {
        public static NumberFormatInfo GetNumberFormatInfo(String NumberDecimalSeparator)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = NumberDecimalSeparator;
            nfi.NumberGroupSeparator = "";

            return nfi;
        }


        public static String Mask(Int32 Digits)
        {
            String result = "0." + new String('0', Digits);

            return result;
        }
    }
}
