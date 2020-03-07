using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class TempFoods
    {
        public TempFood TempFood { get; set; }
        public int Count { get; set; }
    }
}
