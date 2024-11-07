using CheckOutLibrary;
using FluentAssertions;
using NUnit.Framework;

namespace CheckOutTest
{
    [TestFixture]
    public class CheckOutTests
    {
        private readonly string ScanExceptionMessage = "Scanned item is empty or null";
        private readonly string ExceptionMessageForUnIdentifiedItem = "Scanned item does not exist in Product list!!!";
        private readonly string ExceptionMessageNoProductDetails = "Product Details is EMPTY!!! Update it immediately!";
        private List<ProductDetails> _productDetails = new List<ProductDetails>
        {
            new ProductDetails{ ProductName="A", ProductActualPrice=50, IsProductUnderOffer=true, ProductCountToGetOffer = 3, ProductOfferPrice = 130},
            new ProductDetails{ ProductName="B", ProductActualPrice=30, IsProductUnderOffer=true, ProductCountToGetOffer = 2, ProductOfferPrice = 45},
            new ProductDetails{ ProductName="C", ProductActualPrice=20, IsProductUnderOffer=false, ProductCountToGetOffer = 0, ProductOfferPrice = 0},
            new ProductDetails{ ProductName="D", ProductActualPrice=15, IsProductUnderOffer=false, ProductCountToGetOffer = 0, ProductOfferPrice = 0},
        };
        private List<ProductDetails> _emptyProductDetials;
        private ICheckOut _emptyProductDetaislInstance;
        private ICheckOut _checkOutInstance;

        [SetUp]
        public void SetUp()
        {
            _checkOutInstance = new CheckOut(_productDetails);
            _emptyProductDetaislInstance = new CheckOut(_emptyProductDetials);
        }

        [Test]
        public void CheckOut_Scan_ThrowsExceptionWithInvalidData()
        {
            Action act = () =>_checkOutInstance.Scan("");

            act.Should().Throw<Exception>().WithMessage(ScanExceptionMessage);
        }

        [TestCase("A", 50)]
        [TestCase("B", 30)]
        public void CheckOut_GetTotalPrice_WhenSingleItemIsScanned(string productName, int expectedValue)
        {
            ScanItems(productName);
            var totalPrice = _checkOutInstance.GetTotalPrice();
            totalPrice.Should().Be(expectedValue);
        }

        [TestCase("A,B,C", 100)]
        [TestCase("B,C,D",65)]
        public void CheckOut_GetTotalPrice_WhenMultipleDifferentItemsAreScanned(string productNames, int expectedValue)
        {
            ScanItems(productNames);
            var totalPrice = _checkOutInstance.GetTotalPrice();
            totalPrice.Should().Be(expectedValue);
        }

        [TestCase("A,A,A", 130)]
        [TestCase("A,A,A,A", 180)]
        [TestCase("B,B",45)]
        [TestCase("C,C,C,C,C",100)]
        [TestCase("D,D,D",45)]
        public void CheckOut_GetTotalPrice_WhenSameItemsScannedMultipleTimes(string productNames, int expectedValue)
        {
            ScanItems(productNames);
            var totalPrice = _checkOutInstance.GetTotalPrice();
            totalPrice.Should().Be(expectedValue);
        }

        [TestCase("A,B,D,A,A,A,B,C,D", 275)]//4A(3A A) 2B C 2D
        [TestCase("A,B,C,D,D,C,B", 165)]//A 2B 2C 2D
        public void CheckOut_GetTotalPrice_WhenDifferentItemsScannedMultipleTimes(string productNames, int expectedValue)
        {
            ScanItems(productNames);
            var totalPrice = _checkOutInstance.GetTotalPrice();
            totalPrice.Should().Be(expectedValue);
        }

        [Test]
        public void CheckOut_GetTotalPrice_ThrowsExceptionWhenUnIdentifiedItemIsScanned()
        {
            ScanItems("E");
            Action act = () => _checkOutInstance.GetTotalPrice();
            act.Should().Throw<Exception>().WithMessage(ExceptionMessageForUnIdentifiedItem);

        }

        [Test]
        public void CheckOut_GetTotalPrice_ThrowsExceptionWhenProductDetailsIsEmpty()
        {
            _emptyProductDetaislInstance.Scan("A");
            Action act = () => _emptyProductDetaislInstance.GetTotalPrice();
            act.Should().Throw<Exception>().WithMessage(ExceptionMessageNoProductDetails);
        }

        private void ScanItems(string productNames)
        {
            foreach (var productName in productNames.Split(','))
            {
                _checkOutInstance.Scan(productName);
            }
        }

    }
}
