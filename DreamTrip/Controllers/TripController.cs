using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DreamTrip.Filters;
using DreamTrip.Models;
using DreamTrip.Repositories.Abstractions;
using DreamTrip.Repositories.Implementations;
using DreamTrip.ViewModels.Trip;
using DreamTrip.ViewModels.TripApplication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamTrip.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripRepository tripRepository;
        private readonly ITripApplicationRepository tripApplicationRepository;

        public TripController(ITripRepository tripRepository,ITripApplicationRepository tripApplicationRepository)
        {
            this.tripRepository = tripRepository;
            this.tripApplicationRepository = tripApplicationRepository;
        }


        public IActionResult List()
        {
            List<TripListVM> modelList = tripRepository.GetAllWithUser().Select(t => new TripListVM()
            {
                Id = t.Id,
                Title = t.Title,
                Date = t.Date,
                Days = t.Days,
                Destinations = t.Destinations,
                ImageUrl = t.ImageUrl,
                Price = t.Price,
                UserId = t.UserId,
                UserFirstLastNames = t.User.FirstName + " " + t.User.LastName

            }).ToList();
            return View(modelList);
        }

        [AuthFilter]
        public IActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Trip trip = tripRepository.GetByIdWithUser(id.Value);
                if (trip != null)
                {
                    TripDetailsVM model = new TripDetailsVM()
                    {
                        Id = trip.Id,
                        Title = trip.Title,
                        Date = trip.Date,
                        Days = trip.Days,
                        Destinations = trip.Destinations,
                        ImageUrl = trip.ImageUrl,
                        UserId = trip.UserId,
                        FromPlace = trip.FromPlace,
                        Seats = trip.Seats,
                        Description = trip.Description,
                        Price = trip.Price,
                        UserFirstLastName = trip.User.FirstName + " " + trip.User.LastName
                        
                    };
                    List<TripApplication> tripApplications = new List<TripApplication>();
                    
                        tripApplications = tripApplicationRepository.GetByTripIdWithUser(model.Id);
                    
                    List<TripApplicationDetailsVM> appModel = tripApplications.Select(ta => new TripApplicationDetailsVM()
                    {
                        Id = ta.Id,
                        UserId = ta.UserId,
                        TripId = ta.TripId,
                        UserEmail = ta.User.Email,
                        UserFirstLastName = ta.User.FirstName + " " + ta.User.LastName,
                        ImageUrl = ta.User.ImageUrl,
                        Status = ta.Status
                    }).ToList();
                    model.TripApplications = appModel;
                    Counters.AvailableSeats = model.Seats - appModel.Where(ta => ta.Status == Enums.ApplicationStatus.Accepted).Count();
                    model.AvailableSeats = Counters.AvailableSeats;
                    return View(model);
                }              
            }
            return RedirectToAction("List");
        }

        [AuthFilter]
        [TripCreatorFilter]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                //Create
                TripEditVM model = new TripEditVM();
                return View(model);
            }
            else {
                //Edit
               Trip trip = tripRepository.GetById(id.Value,HttpContext.Session.GetInt32("loggedUserId").Value);
                if (trip == null)
                {
                    return RedirectToAction("List");
                }
                else {
                    TripEditVM model = new TripEditVM()
                    {
                        Id = trip.Id,
                        Title = trip.Title,
                        Date = trip.Date,
                        Days = trip.Days,
                        Destinations = trip.Destinations,
                        ImageUrl = trip.ImageUrl,
                        UserId = trip.UserId,
                        FromPlace = trip.FromPlace,
                        Seats = trip.Seats,
                        Description = trip.Description,
                        Price = trip.Price
                    };
                    return View(model);
                }
            }
        }

        [HttpPost]
        [AuthFilter]
        [TripCreatorFilter]
        public IActionResult Edit(TripEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Date<DateTime.Now)
            {
                ModelState.AddModelError("Date Error", "The deparature date must be in the future!");
                return View(model);
            }
            Trip tripCheck = tripRepository.GetById(model.Id, HttpContext.Session.GetInt32("loggedUserId").Value);
            Trip trip  = new Trip()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Date = model.Date,
                    Days = model.Days,
                    Destinations = model.Destinations,
                    ImageUrl = model.ImageUrl,
                    UserId = HttpContext.Session.GetInt32("loggedUserId").Value,
                    FromPlace = model.FromPlace,
                    Seats = model.Seats,
                    Description = model.Description,
                    Price = model.Price
                };
            if (tripCheck == null)
            {
                tripRepository.Insert(trip);
            }
            else
            {
                tripRepository.Update(trip);
            }
            return RedirectToAction("List");
        }

        [AuthFilter]
        [TripCreatorFilter]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }
            Trip trip = tripRepository.GetById(id.Value, HttpContext.Session.GetInt32("loggedUserId").Value);
            if (trip==null)
            {
                return RedirectToAction("List");
            }
            tripRepository.Delete(trip.Id);
            return RedirectToAction("List");
        }
    }
}