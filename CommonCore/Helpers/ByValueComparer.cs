using System;
using System.Collections;
using System.Collections.Generic;

namespace CommonCore.Helpers
{
    public class ByValueComparer : IComparer
    {
        public static readonly IComparer Default = new ByValueComparer(Comparer<object>.Default);

        private readonly IComparer nonByValueComparer;

        private ByValueComparer(IComparer comparer)
        {
            nonByValueComparer = comparer;
        }

        int IComparer.Compare(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (ReferenceEquals(x, DBNull.Value))
            {
                x = null;
            }
            if (ReferenceEquals(y, DBNull.Value))
            {
                y = null;
            }

            if (x != null && y != null)
            {
                var xAsBytes = x as byte[];
                var yAsBytes = y as byte[];
                if (xAsBytes != null
                    && yAsBytes != null)
                {
                    var result = xAsBytes.Length - yAsBytes.Length;
                    if (result == 0)
                    {
                        var idx = 0;
                        while (result == 0
                               && idx < xAsBytes.Length)
                        {
                            var xVal = xAsBytes[idx];
                            var yVal = yAsBytes[idx];
                            if (xVal != yVal)
                            {
                                result = xVal - yVal;
                            }
                            idx++;
                        }
                    }
                    return result;
                }
            }

            return nonByValueComparer.Compare(x, y);
        }
    }
}
