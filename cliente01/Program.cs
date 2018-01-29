using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cliente01
{
    class Program
    {
        static void Main(string[] args)
        {
            var comando = "";

            while (comando != "salir" || comando != "quit") 
                {
                Console.Write("# ");
                comando = Console.ReadLine();
                if (comando.Contains("get")) ObtenerInformacion().Wait();
                else if (comando.Contains("post")) EnviarInformacion(comando.Replace("post ", "")).Wait();
                else Console.WriteLine("No entiendo...");
                }
            
        }

        private static async Task ObtenerInformacion()
        {           
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Accept.Clear();
           
            var stringTask = cliente.GetStringAsync("http://localhost:64000/api/Messaging/");

            var MensajesEnTexto = await stringTask;

            List<Mensaje> MensajesRecibidos = JsonConvert.DeserializeObject<List<Mensaje>>(MensajesEnTexto);

            foreach (var mensaje in MensajesRecibidos)
                Console.WriteLine(mensaje.ToString());

            //Console.ReadLine();
        }

        private static async Task EnviarInformacion(string texto)
        {
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Accept.Clear();            
            
            string textoJson = JsonConvert.SerializeObject(new Mensaje(texto), Formatting.Indented);

            StringContent contenido = new StringContent(textoJson, Encoding.UTF8, "application/json");

            HttpResponseMessage respuesta = await cliente.PostAsync("http://localhost:64000/api/Messaging/", contenido);

            string RespuestaJson = await respuesta.Content.ReadAsStringAsync();

            Console.WriteLine(RespuestaJson);

            //Console.ReadLine();
        }

        
    }
}
