using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckOutLibrary
{
    public interface ICheckOut
    {
        void Scan(string scannedItem);
        int GetTotalPrice();
    }
}
