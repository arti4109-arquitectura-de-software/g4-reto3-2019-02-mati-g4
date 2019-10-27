using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestHl7
{
    class Program
    {
        static void Main(string[] args)
        {
            int good = 0;
            int fail = 0;

            Console.WriteLine($"Init Test");
            Console.ReadKey();

            List<string> messages = new List<string>
            {
                "http://test.fhir.org/r3",
                "http://test.fhir.org/r4",
                "http://sandbox.hspconsortium.org/",
                "http://hspconsortium.org/#/",
                "https://healthservices.atlassian.net/wiki/display/HSPC/Healthcare+Services+Platform+Consortium",
                "http://hapifhir.io/",
                "http://hapi.fhir.org/baseDstu2",
                "http://hapi.fhir.org/baseDstu3",
                "http://hapi.fhir.org/baseR4",
                "http://hapi.fhir.org/baseR5/",
                "http://spark.furore.com/fhir",
                "http://nprogram.azurewebsites.net/",
                "http://fhir-dstu2-nprogram.azurewebsites.net/",
                "http://fhir.oridashi.com.au/",
                "http://demo.oridashi.com.au:8304/",
                "http://demo.oridashi.com.au:8305/",
                "http://demo.oridashi.com.au:8297/",
                "http://demo.oridashi.com.au:8298/",
                "http://demo.oridashi.com.au:8290/",
                "http://demo.oridashi.com.au:8290/",
                "https://pyrohealth.net/",
                "https://stu3.test.pyrohealth.net/fhir",
                "https://r4.test.pyrohealth.net/fhir",
                "https://fhir.smartplatforms.org/",
                "https://fhir-api.smartplatforms.org/",
                "https://fhir-open-api.smartplatforms.org/",
                "https://fhir.smartplatforms.org/",
                "http://worden.globalgold.co.uk:8080/FHIR_a/hosted_demo.html",
                "https://www.health-samurai.io/aidbox",
                "https://www.health-samurai.io/fhirbase",
                "http://fhir.cerner.com/",
                "https://fhir-myrecord.sandboxcerner.com/dstu2/0b8a0111-e8e6-4c26-a91c-5069cbc6b1ca",
                "https://fhir-ehr.sandboxcerner.com/dstu2/0b8a0111-e8e6-4c26-a91c-5069cbc6b1ca",
                "http://open.epic.com/Interface/FHIR",
                "https://fhir-terminology-demo.e-imo.com/api/swagger/ui/index",
                "http://wildfhir4.aegis.net/fhir4-0-0",
                "http://wildfhir3.aegis.net/fhir3-5-0",
                "http://wildfhir3.aegis.net/fhir3-2-0",
                "http://wildfhir2.aegis.net/fhir1-8-0",
                "http://wildfhir2.aegis.net/fhir1-6-0",
                "http://wildfhir2.aegis.net/fhir1-4-0",
                "http://wildfhir2.aegis.net/fhir1-0-2",
                "http://sqlonfhir-dstu2.azurewebsites.net/fhir",
                "http://sqlonfhir-stu3.azurewebsites.net/fhir",
                "http://health.gnusolidario.org:5000/",
                "https://en.wikibooks.org/w/index.php?title=GNU_Health/Using_the_FHIR_REST_server",
                "https://stu3.ontoserver.csiro.au/fhir",
                "https://r4.ontoserver.csiro.au/fhir",
                "http://fhir.i2b2.org/",
                "https://www.i2b2.org/webclient/",
                "https://tw171.open.allscripts.com/FHIR",
                "https://pro171.open.allscripts.com/FHIR",
                "https://scm163cu3.open.allscripts.com/FHIR",
                "http://twdev.open.allscripts.com/FHIR",
                "http://52.72.172.54:8080/fhir/home",
                "https://fhir.careevolution.com/Master.Adapter1.WebClient/fhir",
                "https://fhir.careevolution.com/Master.Adapter1.WebClient/api/fhir",
                "https://fhir.careevolution.com/Master.Adapter1.WebClient/api/fhir-stu3",
                "https://fhir.careevolution.com/Master.Adapter1.WebClient/api/fhir-r4"
            };

            foreach (string server in messages)
            {
                try
                {

                    Console.WriteLine($"Test server: {server}");
                    string response = Service(server);

                    Console.WriteLine($"Test Result: {response}");

                    good++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error {ex}");

                    fail++;
                }
            }

            Console.WriteLine($"Test Fine: {good}");
            Console.WriteLine($"Test Fail: {fail}");
            Console.WriteLine($"Test Total: {messages.Count()}");

            Console.ReadKey();
        }


        private static string Service(string hl7Server)
        {
            var message = new
            {
                Server = hl7Server,
                Id = 1
            };

            string output = JsonConvert.SerializeObject(message, Formatting.Indented);
            var url = "http://localhost/HL7Service/fhir";

            var postString = output;
            byte[] data = UTF8Encoding.UTF8.GetBytes(postString);

            HttpWebRequest request;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 10 * 1000;
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json; charset=utf-8";

            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string resp = reader.ReadToEnd();

            return resp;
        }
    }
}
