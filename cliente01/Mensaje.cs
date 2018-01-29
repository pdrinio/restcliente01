using System;
using System.Runtime.Serialization;

namespace cliente01
{ 
    //[DataContract(Name="Mensaje")]
public class Mensaje
    { 
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }

        public Mensaje() { }

        public Mensaje(int id, string from, string to, string body)
        {
            Id = id;
            Timestamp = DateTime.Now;
            From = from;
            To = to;
            Body = body;
        }

        public Mensaje(string texto)
        {
            Id = 0;
            Timestamp = DateTime.Now;
            From = "Origen";
            To = "Destino";
            Body = texto;
        }

        public override string ToString() => $"Message from '{From}' to '{To}': {Body}";
    }
}

