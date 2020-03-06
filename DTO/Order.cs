using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DTO
{
    public class Order
    {
        public Dictionary<tblFood, int> Foods { get; set; }
        public tblTable Table { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Discount { get; set; }
        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;

                foreach (var item in Foods)
                {
                    totalPrice += item.Key.Price * item.Value;
                }
                return totalPrice - (totalPrice * Discount/100);
            }
        }
    }
}
