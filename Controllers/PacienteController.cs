using Citas_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Citas_App.Controllers
{
    public class PacienteController : Controller
    {
        // Apunta exactamente a Pacientes.json en tu carpeta Data
        private readonly string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "paciente.json");

        private List<Paciente> ObtenerPacientesDesdeJson()
        {
            if (!System.IO.File.Exists(_jsonPath)) return new List<Paciente>();

            string jsonString = System.IO.File.ReadAllText(_jsonPath);
            return JsonSerializer.Deserialize<List<Paciente>>(jsonString) ?? new List<Paciente>();
        }

        // Muestra todos los pacientes registrados
        public IActionResult Index()
        {
            var pacientes = ObtenerPacientesDesdeJson();
            return View(pacientes);
        }

        // Muestra el detalle de un paciente
        public IActionResult Detalle(int id)
        {
            var pacientes = ObtenerPacientesDesdeJson();
            var paciente = pacientes.FirstOrDefault(p => p.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
    }
}