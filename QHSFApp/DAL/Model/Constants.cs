using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Constants
    {
    
        public enum Tabs
        {
            Overview = 1,
            Requirements = 2,
            Estimate = 3,
            Drafting = 4,
            Manufacture = 5,
            Installation = 6,
            Variations = 7,
            Delivery = 8,
            Finance = 9,
            QuoteChecker = 10
        }

        public enum Roles
        {
            SuperAdmin = 1,
            Admin = 2,
            Coordinator = 3
        }
    }
}
