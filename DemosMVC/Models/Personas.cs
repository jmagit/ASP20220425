using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DemosMVC.Models {
    public class Persona : IValidatableObject {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength =2)]
        public string Nombre { get; set; }
        [MaxLength(10)]
        public string Apellidos { get; set; }
        [Display(Name = "N.I.F.")]
        public string NIF { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
        [Display(Name = "F. Nacimiento")]
        [DataType(DataType.Date)]
        [Remote(action: "VerifyFecha", controller: "Demos")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now.AddDays(1);
        [Display(Name = "Fecha de baja")]
        public DateTime FechaBaja { get; set; }
        public bool Activo { get; set; } = true;

        public int Edad => FechaNacimiento == null ? 0 : DateTime.Now.Year - FechaNacimiento.Year - (DateTime.Now.DayOfYear > FechaNacimiento.DayOfYear ? -1 : 0);

        public void Jubilate() {
            Activo = false;
            FechaBaja = DateTime.Now;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (FechaNacimiento.Date.CompareTo(DateTime.Today) == 1)
                yield return new ValidationResult("Todavía no ha nacido", new[] { nameof(FechaNacimiento) });
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
            return new Persona() { Id = id, Nombre = "Pepito", Apellidos = "Grillo" };
        }
    }

}
