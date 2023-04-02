﻿using ConcertDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace ConcertDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public ActionResult ValidarBoleta(string boletaId)
        {
            Console.WriteLine("kfdjfkdfj");
            bool isUsed = false;
            DateTime date = DateTime.MinValue;
            string localidad = string.Empty;

            using (SqlConnection connection = new SqlConnection(""))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Boletas WHERE IdBoleta = @id", connection);
                command.Parameters.AddWithValue("@id", boletaId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isUsed = (bool)reader["IsUsed"];

                    if (isUsed)
                    {
                        date = (DateTime)reader["Date"];
                        localidad = (string)reader["Localidad"];

                        ViewBag.ErrorMessage = "Esta boleta ya fue utilizada el " + date.ToString("dd/MM/yyyy") + " por la portería " + localidad;
                    }
                    else
                    {
                        string[] localidades = new string[] { "Norte", "Sur", "Oriental", "Occidental" };

                        ViewBag.Localidades = localidades;
                        ViewBag.BoletaId = boletaId;
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Boleta no válida";
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsoBoleta(string boletaId, string localidad)
        {
            using (SqlConnection connection = new SqlConnection(""))
            {
                SqlCommand command = new SqlCommand("UPDATE Boletas SET IsUsed = @isUsed, Date = @date, Localidad = @localidad WHERE IdBoleta = @id", connection);
                command.Parameters.AddWithValue("@isUsed", true);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@localidad", localidad);
                command.Parameters.AddWithValue("@id", boletaId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            ViewBag.SuccessMessage = "La boleta fue utilizada correctamente";
            return View("Index");
        }
    }
}