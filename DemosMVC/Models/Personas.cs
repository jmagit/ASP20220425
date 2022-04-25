using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemosMVC.Models {
    public class Persona {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
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
        public Persona Get(int id) {
            return new Persona() { Id = id, Nombre = "Pepito", Apellidos = "Grillo" };
        }
    }

}
