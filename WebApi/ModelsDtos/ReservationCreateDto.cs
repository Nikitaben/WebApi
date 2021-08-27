using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ModelsDtos
{
    public class ReservationCreateDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<int> MenuItemIds { get; set; }
    }
}
