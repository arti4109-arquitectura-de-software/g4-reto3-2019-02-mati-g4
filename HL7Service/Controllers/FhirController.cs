using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using HL7Service.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HL7Service.Controllers
{
    [Route("fhir")]
    [ApiController]
    public class FhirController : ControllerBase
    {
        [HttpGet]
        public void Validate()
        {
        }

        [HttpPost]
        public Patient GetMessage([FromBody]ServerClient message)
        {

            /// Servidor de hl7 http://hapi.fhir.org/baseR4
            /// Usuario 37

            var client = new FhirClient(message.Server);
            var identity = ResourceIdentity.Build("Patient", message.Id);
            var patient = client.Read<Patient>(identity);

            return patient;
         }
    }
}