using Microsoft.AspNetCore.Mvc;
using PatientManager.Domain.Model;
using PatientManager.Domain.Ports;

namespace PatientManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpPost]
        public IActionResult RegisterPatient([FromBody] Patient patient)
        {
            _patientService.RegisterPatient(patient);
            return Ok("Paciente cadastrado com sucesso");
        }

        [HttpGet("{id}")]
        public IActionResult GetPatient(int id)
        {
            var patient = _patientService.GetPatient(id);
            return patient != null ? Ok(patient) : NotFound("Paciente não encontrado");
        }
    }
}
