using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class TempBtnTable
    {
        public string Text { get; set; }
        public TempFoods[] Foods { get; set; }
        public TempTable Table { get; set; } = new TempTable();
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Discount { get; set; }
        public double TotalPrice { get; set; }
    }
}
