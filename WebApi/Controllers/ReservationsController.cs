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
        //    var reservations = _context.Reservations
        //        .Select(r => _mapper.Map<ReservationReadDto>(r))
        //        .ToList();
        //    var menuItems = _context.MenuItems
        //       .Select(p => _mapper.Map<MenuItemReadDto>(p))
        //       .ToList();
        //    var reservationMenuItems = _context.ReservationMenuItems.ToList();


            //foreach (var reservation in reservations)
            //{
            //    List<MenuItemReadDto> menuItemsToAdd = new List<MenuItemReadDto>();
            //    foreach (var rm in reservationMenuItems)
            //    {
            //        if (rm.ReservationId == reservation.Id)
            //        {
            //            MenuItemReadDto mItem = menuItems
            //                    .FirstOrDefault(m => m.Id == rm.MenuItemId);
            //            if (mItem != null)
            //            {
            //                menuItemsToAdd.Add(mItem);
            //            }
            //        }
            //    }

            //    reservation.MenuItems = menuItemsToAdd;

            //}
            var reservations = _context.Reservations
                .Select(r => new ReservationReadDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Date = r.Date,
                    MenuItems = _context.ReservationMenuItems
                    .Where(rm => rm.ReservationId == r.Id)
                    .Select(m => new MenuItemReadDto
                    {
                        Id = m.MenuItem.Id,
                        Name = m.MenuItem.Name,
                        Price = m.MenuItem.Price

                    })
                    .ToList()

                });
            return Ok(reservations);
        }

        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var resrvation =_context.Reservations
                .Where(r => r.Id == id)
                 .Select(r => new ReservationReadDto
                 {
                     Id = r.Id,
                     Name = r.Name,
                     Date = r.Date,
                     MenuItems = _context.ReservationMenuItems
                    .Where(rm => rm.ReservationId == r.Id)
                    .Select(m => new MenuItemReadDto
                    {
                        Id = m.MenuItem.Id,
                        Name = m.MenuItem.Name,
                        Price = m.MenuItem.Price

                    })
                    .ToList()

                 })
                 .FirstOrDefault();
            if (resrvation == null)
                return NotFound($"Reservation with id={id} doesn't exist.");
            return Ok(resrvation);
        }

        // POST api/<ReservationsController>
        [HttpPost]
        public ActionResult Post([FromBody] ReservationCreateDto value)
        {
            var newReservation = _mapper.Map<Reservation>(value);
            _context.Reservations.Add(newReservation);
            _context.SaveChanges();

            foreach(var id in value.MenuItemIds)
            {
                ReservationMenuItems rm = new ReservationMenuItems
                {
                    ReservationId = newReservation.Id,
                    MenuItemId = id
                };
                _context.ReservationMenuItems.Add(rm);
            }
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/<ReservationsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ReservationCreateDto value)
        {
            var reservationFromDb = _context.Reservations
                .FirstOrDefault(r => r.Id == id);

            if (reservationFromDb == null)
                return NotFound();

            _mapper.Map(value, reservationFromDb);

            var menuItemsToRemove = _context.ReservationMenuItems
                .Where(rm => rm.ReservationId == id);

            _context.ReservationMenuItems.RemoveRange(menuItemsToRemove);

            var menuItemsToAdd = value.MenuItemIds
                .Select(mId => new ReservationMenuItems
                {
                    ReservationId = id,
                    MenuItemId = mId
                });
            _context.ReservationMenuItems.AddRange(menuItemsToAdd);

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var reservationFromDb = _context.Reservations
               .FirstOrDefault(r => r.Id == id);

            if (reservationFromDb == null)
                return NotFound();
            var menuItemsToRemove = _context.ReservationMenuItems
               .Where(rm => rm.ReservationId == id);

            _context.ReservationMenuItems.RemoveRange(menuItemsToRemove);
            _context.Reservations.Remove(reservationFromDb);

            _context.SaveChanges();

            return NoContent();

        }
    }
}
