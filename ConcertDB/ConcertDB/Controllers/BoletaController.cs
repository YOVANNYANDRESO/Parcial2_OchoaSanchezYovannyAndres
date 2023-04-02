using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ConcertDB.Controllers
{
    public class BoletaController : Controller
    {
        private const string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidarBoleta(string boletaId)
        {
            Console.WriteLine("kfdjfkdfj");
            bool isUsed = false;
            DateTime date = DateTime.MinValue;
            string localidad = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
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
            using (SqlConnection connection = new SqlConnection(connectionString))
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
