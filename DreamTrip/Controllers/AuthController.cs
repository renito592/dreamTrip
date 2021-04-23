using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DreamTrip.Models;
using DreamTrip.Repositories.Abstractions;
using DreamTrip.Repositories.Implementations;
using DreamTrip.ViewModels.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamTrip.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User loggedUser = userRepository.GetByUsernameAndPassword(model.Username, model.Password);
            if (loggedUser == null)
            {
                ModelState.AddModelError("login error", "Wrong Username or Password");
                return View(model);
            }
            HttpContext.Session.SetInt32("loggedUserId", loggedUser.Id);
            AuthUser.LoggedUser = loggedUser;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = new User
            {
                Username = model.Username,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Role = model.Role,
                ImageUrl = model.ImageUrl

            };
            if (userRepository.GetByUsername(user.Username) != null)
            {
                ModelState.AddModelError("User error","This username is taken");
                return View(model);
            }
            userRepository.Insert(user);
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            AuthUser.LoggedUser = null;
            return RedirectToAction("Login");
        }
    }
}