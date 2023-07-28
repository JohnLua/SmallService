using SmallService.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SmallService.Service
{
    public static class MemoryDatabaseService
    {
        private static SortedList<Customer, Customer> rankList = new SortedList<Customer, Customer>(new CustomerComparer());
        private static Dictionary<Int64, Customer> customers = new Dictionary<long, Customer>();

        public static decimal Update(Int64 customerId, decimal score)
        {
            if (customers.ContainsKey(customerId))
            {
                var item = customers[customerId];
                var itemIndex = rankList.IndexOfValue(item);
                rankList.RemoveAt(itemIndex);
                rankList.Add(item, item);

                item.Score += score;
                CheckScore(item);

                return item.Score;
            }

            var newItem = new Customer { CustomerId = customerId, Score = score };
            CheckScore(newItem);
            rankList.Add(newItem, newItem);
            customers.Add(customerId, newItem);

            return newItem.Score;
        }

        private static void CheckScore(Customer customer)
        {
            if (customer.Score < -1000)
            {
                customer.Score = -1000;
            }

            if (customer.Score > 1000)
            {
                customer.Score = 1000;
            }
        }

        public static IEnumerable<Customer> Range(int start, int end)
        {
            var rangeStart = start - 1;
            var rangeEnd = rankList.Count - 1;
            if (end < rankList.Count + 1)
            {
                rangeEnd = end;
            }

            if (rangeStart > rangeEnd)
            {
                return Enumerable.Empty<Customer>();
            }

            var result = new List<Customer>();
            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                rankList.Values[i].Rank = i + 1;
                result.Add(rankList.Values[i]);
            }

            return result;
        }

        public static IEnumerable<Customer> RangeByCustomerId(Int64 customerId, int high = 0, int low = 0)
        {
            if (!customers.ContainsKey(customerId))
            {
                return Enumerable.Empty<Customer>();
            }

            var customerScore = customers[customerId].Score;
            var findCustomer = new Customer { CustomerId = customerId, Score = customerScore };

            var customerRank = rankList.IndexOfKey(findCustomer);
            var rankStart = 0;
            if (customerRank - high >= 0)
            {
                rankStart = customerRank - high;
            }

            var rankEnd = rankList.Count;
            if (customerRank + low + 1 <= rankList.Count)
            {
                rankEnd = customerRank + low + 1;
            }

            var results = new List<Customer>();
            for(int i = rankStart; i < rankEnd; i++)
            {
                rankList.Values[i].Rank = i + 1;
                results.Add(rankList.Values[i]);
            }

            return results;
        }

        class CustomerComparer : IComparer<Customer>
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
                    else
                    {
                        return 1;
                    }
                }

                if (x.Score < y.Score)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
