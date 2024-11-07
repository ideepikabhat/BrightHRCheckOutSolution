using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckOutLibrary
{
    public class ProductDetails
    {
        public string ProductName { get; set; }
        public int ProductActualPrice { get; set; }
        public bool IsProductUnderOffer { get; set; }
        public int ProductOfferPrice { get; set; }
        public int ProductCountToGetOffer { get; set; }
    }
}
