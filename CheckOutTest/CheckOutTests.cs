using CheckOutLibrary;
using NUnit.Framework;

namespace CheckOutTest
{
    [TestFixture]
    public class CheckOutTests
    {
        private List<ProductDetails> _productDetails = new List<ProductDetails>
        {
            new ProductDetails{ ProductName="A", ProductActualPrice=50, IsProductUnderOffer=true, ProductCountToGetOffer = 3, ProductOfferPrice = 130},
            new ProductDetails{ ProductName="B", ProductActualPrice=30, IsProductUnderOffer=true, ProductCountToGetOffer = 2, ProductOfferPrice = 45},
            new ProductDetails{ ProductName="C", ProductActualPrice=20, IsProductUnderOffer=false, ProductCountToGetOffer = 0, ProductOfferPrice = 0},
            new ProductDetails{ ProductName="D", ProductActualPrice=15, IsProductUnderOffer=false, ProductCountToGetOffer = 0, ProductOfferPrice = 0},
        };
        private ICheckOut _checkOutInstance;

        [SetUp]
        public void SetUp()
        {
            _checkOutInstance = new CheckOut(_productDetails);
        }

    }
}
