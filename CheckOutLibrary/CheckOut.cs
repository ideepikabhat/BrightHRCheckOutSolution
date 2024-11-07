namespace CheckOutLibrary
{
    public class CheckOut : ICheckOut
    {
        private readonly List<ProductDetails> _products;

        private Dictionary<string, int> _scannedProductDetails = new Dictionary<string, int>();
        public CheckOut(List<ProductDetails> products) 
        {
            _products = products;
        }
        public int GetTotalPrice()
        {
            int totalPrice = 0;

            if(_products != null)
            {
                foreach (var scannedItem in _scannedProductDetails)
                {
                    var matchedProduct = _products.FirstOrDefault(p => p.ProductName == scannedItem.Key);

                    if (matchedProduct != null)
                    {
                        if (matchedProduct.IsProductUnderOffer)
                        {
                            var itemsToCalOnActualPrice = scannedItem.Value % matchedProduct.ProductCountToGetOffer;
                            var groupOfItemsValidForOffer = scannedItem.Value / matchedProduct.ProductCountToGetOffer;

                            totalPrice += (itemsToCalOnActualPrice * matchedProduct.ProductActualPrice) + (groupOfItemsValidForOffer * matchedProduct.ProductOfferPrice);
                        }
                        else
                        {
                            totalPrice += scannedItem.Value * matchedProduct.ProductActualPrice;
                        }
                    }
                    else
                    {
                        throw new Exception("Scanned item does not exist in Product list!!!");//these exceptions can be handled where this library is used.
                    }
                }
            }
            else
            {
                throw new Exception("Product Details is EMPTY!!! Update it immediately!"); //these exceptions can be handled where this library is used.
            }

            return totalPrice;
        }

        public void Scan(string scannedItem)
        {
            if(!string.IsNullOrEmpty(scannedItem))
            {
                if(!_scannedProductDetails.ContainsKey(scannedItem))
                {
                    _scannedProductDetails.Add(scannedItem, 1);
                }
                else
                {
                    _scannedProductDetails[scannedItem]++;
                }
            }
            else
            {
                throw new Exception("Scanned item is empty or null");
            }
        }
    }
}
