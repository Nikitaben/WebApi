using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.ModelsDtos
{
    public class ReservationReadDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public List<MenuItemReadDto> MenuItems { get; set; }

    }
}
