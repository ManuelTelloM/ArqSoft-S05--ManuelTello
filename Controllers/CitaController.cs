using Citas_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Citas_App.Controllers
{
    public class CitaController : Controller
    {
        // Rutas a los archivos JSON
        private readonly string _jsonPath =
            Path.Combine(Directory.GetCurrentDirectory(), "Data", "cita.json");

        private readonly string _pacientesPath =
            Path.Combine(Directory.GetCurrentDirectory(), "Data", "paciente.json");

        private readonly string _medicosPath =
            Path.Combine(Directory.GetCurrentDirectory(), "Data", "medicos.json");

        // Leer citas
        private List<Cita> ObtenerCitasDesdeJson()
        {
            if (!System.IO.File.Exists(_jsonPath))
                return new List<Cita>();

            string jsonString = System.IO.File.ReadAllText(_jsonPath);

            return JsonSerializer.Deserialize<List<Cita>>(jsonString)
                   ?? new List<Cita>();
        }

        // Leer pacientes
        private List<Paciente> ObtenerPacientesDesdeJson()
        {
            if (!System.IO.File.Exists(_pacientesPath))
                return new List<Paciente>();

            string jsonString = System.IO.File.ReadAllText(_pacientesPath);

            return JsonSerializer.Deserialize<List<Paciente>>(jsonString)
                   ?? new List<Paciente>();
        }

        // Leer médicos
        private List<Medico> ObtenerMedicosDesdeJson()
        {
            if (!System.IO.File.Exists(_medicosPath))
                return new List<Medico>();

            string jsonString = System.IO.File.ReadAllText(_medicosPath);

            return JsonSerializer.Deserialize<List<Medico>>(jsonString)
                   ?? new List<Medico>();
        }

        // Muestra la agenda completa de citas
        public IActionResult Index()
        {
            var citas = ObtenerCitasDesdeJson();

            ViewBag.Pacientes = ObtenerPacientesDesdeJson();
            ViewBag.Medicos = ObtenerMedicosDesdeJson();

            return View(citas);
        }

        // Filtra las citas de un paciente específico
        public IActionResult PorPaciente(int pacienteId)
        {
            var citas = ObtenerCitasDesdeJson();
            var citasPaciente = citas
                .Where(c => c.PacienteId == pacienteId)
                .ToList();

            ViewBag.Pacientes = ObtenerPacientesDesdeJson();
            ViewBag.Medicos = ObtenerMedicosDesdeJson();

            return View(citasPaciente);
        }
    }
}