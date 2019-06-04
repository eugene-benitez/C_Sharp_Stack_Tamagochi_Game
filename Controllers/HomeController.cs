using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dojodachi.Models;
using Newtonsoft.Json;


namespace Dojodachi.Controllers
{
    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes theobject to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu") == null)
            {
                HttpContext.Session.SetObjectAsJson("Pikachu", new Puppy());
            }

            ViewBag.Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");

            if (ViewBag.Pikachu.Fullness < 1 || ViewBag.Pikachu.Happiness < 1)
            {
                ViewBag.Pikachu.isDead = true;
                ViewBag.Pikachu.Log = "Oh no! The fullness or happiness dropped to 0. Game over!";
            }

            if (ViewBag.Pikachu.Energy > 100 && ViewBag.Pikachu.Fullness > 100 && ViewBag.Pikachu.Happiness > 100)
            {
                ViewBag.Pikachu.isDead = true;
                ViewBag.Pikachu.Log = "You did it! Energy, Fullness, and Happiness are over 100!";
            }

            ViewBag.Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            return View("Index");
        }

        [HttpGet("feed")]
        public IActionResult Food()
        {
            Puppy Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            Pikachu.Feed();
            @ViewBag.Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            HttpContext.Session.SetObjectAsJson("Pikachu", Pikachu);
            return RedirectToAction("Index");
        }

        [HttpGet("clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetObjectAsJson("Pikachu", new Puppy());
            @ViewBag.Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");

            return RedirectToAction("Index");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            Puppy Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            Pikachu.Play();
            @ViewBag.Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            HttpContext.Session.SetObjectAsJson("Pikachu", Pikachu);
            return RedirectToAction("Index");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            Puppy Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            Pikachu.Work();
            @ViewBag.Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            HttpContext.Session.SetObjectAsJson("Pikachu", Pikachu);
            return RedirectToAction("Index");
        }

        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            Puppy Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            Pikachu.Sleep();
            @ViewBag.Pikachu = HttpContext.Session.GetObjectFromJson<Puppy>("Pikachu");
            HttpContext.Session.SetObjectAsJson("Pikachu", Pikachu);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
