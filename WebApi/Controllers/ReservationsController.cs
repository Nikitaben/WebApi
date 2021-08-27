using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.ModelsDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReservationsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<ReservationsController>
        [HttpGet]
        public ActionResult Get()
        {
            var reservations = _context.Reservations
                .Select(o => _mapper.Map<ReservationReadDto>(o))
                .ToList();
            var menuItems = _context.MenuItems
                .Select(p => _mapper.Map<MenuItemReadDto>(p))
                .ToList();
            var reservationMenuItems = _context.reservationMenuItems.ToList();


            foreach (var reservation in reservations)
            {
                List<MenuItemReadDto> menuItemsToAdd = new List<MenuItemReadDto>();
                foreach (var rm in reservationMenuItems)
                {
                    if (rm.ReservationId == reservation.Id)
                    {
                        MenuItemReadDto mItem = menuItems
                                .FirstOrDefault(m => m.Id == rm.MenuItemId);
                        if (mItem != null)
                        {
                            menuItemsToAdd.Add(mItem);
                        }
                    }
                }

                reservation.MenuItems = menuItemsToAdd;

            }
            return Ok(reservations);
        }

        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReservationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReservationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
