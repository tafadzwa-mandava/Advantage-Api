using System;
using System.Collections.Generic;

namespace Advantage.API
{
    public class Helpers
    {
        private static Random _rand = new Random();
        private static string GetRandom(IList<string> items)
        {
            return items[_rand.Next(items.Count)];
        }

        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxNames = bizPrefix.Count * bizSuffix.Count;

            if(names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Maximum number of unique names exceeded");
            }

            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);
            var bizName = prefix + suffix;
            
            if(names.Contains(bizName))
            {
                MakeUniqueCustomerName(names);
            }

            return bizName;
        }

        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com"; //Using interpollation to build a customer email
        }

        internal static string GetRandomState()
        {
            return GetRandom(usStates);
        }

        internal static decimal GetRandomOrderTotal()
        {
            return _rand.Next(100, 5000);
        }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now; //So that we don't have any orders to be placed in the future when we seed our data
            var start = end.AddDays(-90); //Getting the maximum historical date that an order was placed. We want the data in our database to go back a maximum of 90 days
            
            TimeSpan possibleSpan = end - start; //Return some  date infront of start but before end
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0); //Pass hours, minutes and seconds where will have zero hours and some number of minutes. The number of minutes will be between zero and the total number of minutes between the possibleSpan
                             //new TimeSpan(hours, minutes, seconds)

            return start + newSpan;
        }

        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7); //We have a minimum leadtime on our orders of 7 days
            var timePassed = now - orderPlaced;

            if(timePassed < minLeadTime)  //Criteria for whether or not a date should be null
            {
                return null;
            }

            return orderPlaced.AddDays(_rand.Next(7, 14));  //Random number of days between 7 days and 14 days
        }

        private static readonly List<string> usStates = new List<string> ()
        {
            "AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };


        private static readonly List<string> bizPrefix = new List<string>
        {
            "ABC",
            "XYZ",
            "MainSt",
            "Sales",
            "Enterprise",
            "Ready",
            "Quick",
            "Budget",
            "Peak",
            "Magic",
            "Family",
            "Comfort"
        };

        private static readonly List<string> bizSuffix = new List<string>
        {
            "Corporation",
            "Co",
            "Logistics",
            "Transit",
            "Bakery",
            "Goods",
            "Foods",
            "Cleaners",
            "Hotels",
            "Planners",
            "Automotive",
            "Books"
        };

    }

}