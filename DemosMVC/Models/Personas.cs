using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemosMVC.Models {
    public class Persona {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NIF { get; set; }
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public DateTime FechaBaja { get; set; }
        public bool Activo { get; set; } = true;

        public int Edad {
            get {
                return DateTime.Now.Year - FechaNacimiento.Year - (DateTime.Now.DayOfYear > FechaNacimiento.DayOfYear ? -1 : 0);
            }
        }

        public void Jubilate() {
            Activo = false;
            FechaBaja = DateTime.Now;
        }
    }

    public class PersonaRepository {
        public IList<Persona> Get() {
            var rslt = new List<Persona>();
            rslt.Add(new Persona() { Id = 1, Nombre = "Pepito", Apellidos = "Grillo", FechaNacimiento = DateTime.Parse("01/01/1999") });
            rslt.Add(new Persona() { Id = 2, Nombre = "Carmelo", Apellidos = "Coton", FechaNacimiento = DateTime.Parse("02/02/2002") });
            rslt.Add(new Persona() { Id = 3, Nombre = "Pedro", Apellidos = "Pica Piedra", FechaNacimiento = DateTime.Parse("12/12/1991") });
            return  rslt;
        }
        public Persona Get(int id) {
            return new Persona() { Id = id, Nombre = "Pepito", Apellidos = "Grillo", FechaNacimiento = DateTime.Parse("01/01/1999") };
        }
    }

}
