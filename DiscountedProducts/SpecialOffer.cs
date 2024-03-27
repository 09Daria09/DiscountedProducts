using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountedProducts
{
    public class SpecialOffer
    {
        public int PromotionID { get; set; }
        public int InterestID { get; set; }
        public int CountryID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PromotionDetails { get; set; }
    }

}
