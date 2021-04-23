using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DreamTrip.Filters;
using DreamTrip.Models;
using DreamTrip.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamTrip.Controllers
{
    [AuthFilter]
    
    public class TripApplicationController : Controller
    {
        private readonly ITripApplicationRepository tripApplicationRepository;
        private readonly ITripRepository tripRepository;

        public TripApplicationController(ITripApplicationRepository tripApplicationRepository,ITripRepository tripRepository)
        {
            this.tripApplicationRepository = tripApplicationRepository;
            this.tripRepository = tripRepository;
        }
        [TravellerFilter]
        public IActionResult Apply(int? tripId)
        {
            if (!tripId.HasValue)
            {
                return RedirectToAction("List", "Trip");
            }
            TripApplication tripApp = tripApplicationRepository.GetByTripIdUserId(tripId.Value, HttpContext.Session.GetInt32("loggedUserId").Value);
            if (tripApp==null)
            {
                TripApplication application = new TripApplication()
                {
                    UserId = HttpContext.Session.GetInt32("loggedUserId").Value,
                    TripId = tripId.Value,
                    Status = Enums.ApplicationStatus.Pending
                };
                tripApplicationRepository.Insert(application);
            }

            return RedirectToAction("Details","Trip",new {id=tripId});
        }
        [TravellerFilter]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
            return RedirectToAction("List", "Trip");
            }
            TripApplication tripApp = tripApplicationRepository.GetById(id.Value);
            if (tripApp!=null && tripApp.UserId == AuthUser.LoggedUser.Id)
            {
                tripApplicationRepository.Delete(tripApp.Id);
            }
            return RedirectToAction("Details", "Trip", new { id = tripApp.TripId });
        }

        public IActionResult Accept(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "Trip");
            }
            TripApplication tripApp = tripApplicationRepository.GetById(id.Value);
            if (tripRepository.GetById(tripApp.TripId).UserId==HttpContext.Session.GetInt32("loggedUserId").Value)
            {
                if (Counters.AvailableSeats == 0)
                {
                    return RedirectToAction("Details", "Trip", new { id = tripApp.TripId });
                }
                tripApp.Status = Enums.ApplicationStatus.Accepted;
                tripApplicationRepository.Update(tripApp);
            }
            return RedirectToAction("Details", "Trip", new { id = tripApp.TripId });
        }

        public IActionResult Reject(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "Trip");
            }
            TripApplication tripApp = tripApplicationRepository.GetById(id.Value);
            if (tripRepository.GetById(tripApp.TripId).UserId == HttpContext.Session.GetInt32("loggedUserId").Value)
            {
                tripApp.Status = Enums.ApplicationStatus.Rejected;
                tripApplicationRepository.Update(tripApp);
            }
            return RedirectToAction("Details", "Trip", new { id = tripApp.TripId });
        }
    }
}