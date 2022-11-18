using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{
    public class HouseEntity
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string? Photo { get; set; }
    }
}
