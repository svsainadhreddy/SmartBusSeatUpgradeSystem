using BusSeatManagement.Data;
using BusSeatManagement.Models;
using BusSeatManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusSeatManagement.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // VERIFY PAGE
        // =========================

        public IActionResult Verify()
        {
            return View();
        }

        // =========================
        // VERIFY BOOKING
        // =========================

        [HttpPost]
        public IActionResult Verify(
            BookingVerificationViewModel model)
        {
            var booking = _context.Bookings
                .Include(b => b.Seat)
                .Include(b => b.Bus)
                .FirstOrDefault(b =>
                    b.BookingId == model.BookingId &&
                    b.PhoneNumber == model.PhoneNumber);

            if (booking == null)
            {
                ViewBag.Error =
                    "Invalid Booking Details";

                return View();
            }

            return RedirectToAction(
                "SeatSelection",
                new { bookingId = booking.BookingId });
        }

        // =========================
        // SEAT SELECTION PAGE
        // =========================

        public IActionResult SeatSelection(int bookingId)
        {
            var booking = _context.Bookings
                .Include(b => b.Seat)
                .Include(b => b.Bus)
                .FirstOrDefault(b =>
                    b.BookingId == bookingId);

            if (booking == null)
            {
                return RedirectToAction("Verify");
            }

            // GET ALL SEATS

            var seats = _context.Seats
                .Where(s => s.BusId == booking.BusId)
                .OrderBy(s => s.SeatId)
                .ToList();

            ViewBag.Booking = booking;

            return View(seats);
        }

        // =========================
        // PAYMENT PAGE
        // =========================

        public IActionResult Payment(
            int bookingId,
            int newSeatId)
        {
            var booking = _context.Bookings
                .Include(b => b.Seat)
                .FirstOrDefault(b =>
                    b.BookingId == bookingId);

            if (booking == null)
            {
                return RedirectToAction("Verify");
            }

            var newSeat = _context.Seats
                .FirstOrDefault(s =>
                    s.SeatId == newSeatId);

            if (newSeat == null)
            {
                TempData["Error"] =
                    "Seat not found";

                return RedirectToAction(
                    "SeatSelection",
                    new { bookingId });
            }

            // PREVENT SAME SEAT

            if (newSeat.SeatId == booking.CurrentSeatId)
            {
                TempData["Error"] =
                    "You already selected this seat";

                return RedirectToAction(
                    "SeatSelection",
                    new { bookingId });
            }

            // PREVENT BOOKED SEAT

            if (newSeat.IsBooked)
            {
                TempData["Error"] =
                    "Seat already booked";

                return RedirectToAction(
                    "SeatSelection",
                    new { bookingId });
            }

            decimal difference =
                newSeat.Price -
                booking.CurrentFare;

            string actionType =
                difference > 0
                ? "Upgrade"
                : "Downgrade";

            SeatChangePaymentViewModel model =
                new SeatChangePaymentViewModel
                {
                    BookingId = booking.BookingId,

                    OldSeatId = booking.CurrentSeatId,

                    NewSeatId = newSeat.SeatId,

                    CurrentSeatNumber =
                        booking.Seat.SeatNumber,

                    NewSeatNumber =
                        newSeat.SeatNumber,

                    CurrentFare =
                        booking.CurrentFare,

                    NewFare =
                        newSeat.Price,

                    // KEEP NEGATIVE FOR REFUND

                    DifferenceAmount =
                        difference,

                    ActionType =
                        actionType
                };

            return View(model);
        }

        // =========================
        // CONFIRM PAYMENT + UPDATE DB
        // =========================

        [HttpPost]
        public IActionResult ConfirmSeatChange(
            SeatChangePaymentViewModel model)
        {
            var booking = _context.Bookings
                .Include(b => b.Seat)
                .FirstOrDefault(b =>
                    b.BookingId == model.BookingId);

            if (booking == null)
            {
                TempData["Error"] =
                    "Booking not found";

                return RedirectToAction("Verify");
            }

            var oldSeat = _context.Seats
                .FirstOrDefault(s =>
                    s.SeatId == booking.CurrentSeatId);

            var newSeat = _context.Seats
                .FirstOrDefault(s =>
                    s.SeatId == model.NewSeatId);

            if (newSeat == null || newSeat.IsBooked)
            {
                TempData["Error"] =
                    "Seat unavailable";

                return RedirectToAction(
                    "SeatSelection",
                    new { bookingId = booking.BookingId });
            }

            decimal difference =
                newSeat.Price -
                booking.CurrentFare;

            string actionType =
                difference > 0
                ? "Upgrade"
                : "Downgrade";

            // FREE OLD SEAT

            oldSeat.IsBooked = false;

            // BOOK NEW SEAT

            newSeat.IsBooked = true;

            // UPDATE BOOKING

            booking.CurrentSeatId =
                newSeat.SeatId;

            booking.Seat = newSeat;

            booking.CurrentFare =
                newSeat.Price;

            // SAVE TRANSACTION

            SeatTransaction transaction =
                new SeatTransaction
                {
                    BookingId = booking.BookingId,

                    OldSeatId = oldSeat.SeatId,

                    NewSeatId = newSeat.SeatId,

                    OldFare = oldSeat.Price,

                    NewFare = newSeat.Price,

                    DifferenceAmount = difference,

                    ActionType = actionType
                };

            _context.SeatTransactions
                .Add(transaction);

            _context.SaveChanges();

            // SUCCESS MESSAGE

            if (difference > 0)
            {
                TempData["Success"] =
                    $"Payment Successful. ₹{difference} Paid.";
            }
            else if (difference < 0)
            {
                TempData["Success"] =
                    $"Seat Downgraded. Refund ₹{Math.Abs(difference)} Initiated.";
            }
            else
            {
                TempData["Success"] =
                    "Seat Updated Successfully.";
            }

            return RedirectToAction(
                "SeatSelection",
                new { bookingId = booking.BookingId });
        }
    }
}