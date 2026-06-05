using Citas_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Citas_App.Controllers
{
    public class MedicoController : Controller
    {
        // Apunta exactamente a Medico.json en tu carpeta Data
        private readonly string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "medicos.json");

        private List<Medico> ObtenerMedicosDesdeJson()
        {
            if (!System.IO.File.Exists(_jsonPath)) return new List<Medico>();

            string jsonString = System.IO.File.ReadAllText(_jsonPath);
            return JsonSerializer.Deserialize<List<Medico>>(jsonString) ?? new List<Medico>();
        }

        // Lista todos los médicos disponibles
        public IActionResult Index()
        {
            var medicos = ObtenerMedicosDesdeJson();
            return View(medicos);
        }

        // Muestra el detalle de un médico y su especialidad
        public IActionResult Detalle(int id)
        {
            var medicos = ObtenerMedicosDesdeJson();
            var Medico = medicos.FirstOrDefault(m => m.Id == id);

            if (Medico == null)
            {
                return NotFound();
            }

            return View(Medico);
        }
    }
}