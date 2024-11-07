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
            throw new NotImplementedException();
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
