using SmallService.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SmallService
{
    public class CustomerScoreComparer : IComparer<Customer>
    {
        public int Compare([AllowNull] Customer x, [AllowNull] Customer y)
        {
            if (x.Score == y.Score)
            {
                if (x.CustomerId == y.CustomerId)
                {
                    return 0;
                }

                if (x.CustomerId < y.CustomerId)
                {
                    return -1;
                }
                
                return 1;
            }

            if (x.Score < y.Score)
            {
                return 1;
            }
            
            return -1;
        }
    }
}
