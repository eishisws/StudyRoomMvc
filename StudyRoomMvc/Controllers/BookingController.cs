using Microsoft.AspNetCore.Mvc;
using StudyRoomMvc.Data;
using StudyRoomMvc.Models;
using StudyRoomMvc.Services;

namespace StudyRoomMvc.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        public IActionResult Details(int id)
        {
            var booking = _service.GetById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _service.Add(booking);
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        public IActionResult Edit(int id)
        {
            var booking = _service.GetById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _service.Update(booking);
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        public IActionResult Delete(int id)
        {
            var booking = _service.GetById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
