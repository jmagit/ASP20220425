using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Util.Core.DataAnnotations;
using Util.Core.Validators;

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
        [NIF]
        public string NIF { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.\d)(?=.[a-z])(?=.[A-Z])(?=.\W).{8,}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres con minúsculas, mayúsculas, dígitos y símbolos")]
        public string Contraseña { get; set; }
        [Display(Name = "F. Nacimiento")]
        [DataType(DataType.Date)]
        [Remote(action: "VerifyFecha", controller: "Demos")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now.AddDays(1);
        [Display(Name = "Fecha de baja")]
        [CustomValidation(typeof(Persona), nameof(Pasada), ErrorMessage = "Debe ser una fecha pasada")]
        public DateTime FechaBaja { get; set; }
        public bool Activo { get; set; } = true;

        public int Edad => DateTime.Now.Year - FechaNacimiento.Year - (DateTime.Now.DayOfYear > FechaNacimiento.DayOfYear ? -1 : 0);

        public void Jubilate() {
            Activo = false;
            FechaBaja = DateTime.Now;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (FechaNacimiento.Date.CompareTo(DateTime.Today) > 0)
                yield return new ValidationResult("Todavía no ha nacido", new[] { nameof(FechaNacimiento) });
        }

        public static ValidationResult Pasada(DateTime value) {
            return value.IsPast() ? ValidationResult.Success : new ValidationResult("Debe ser una fecha pasada");
        }
    }

    public class PersonaRepository {
        public IList<Persona> Get() {
            return new List<Persona> {
                new Persona() { Id = 1, Nombre = "Pepito", Apellidos = "Grillo", FechaNacimiento = DateTime.Parse("01/01/1999") },
                new Persona() { Id = 2, Nombre = "Carmelo", Apellidos = "Coton", FechaNacimiento = DateTime.Parse("02/02/2002") },
                new Persona() { Id = 3, Nombre = "Pedro", Apellidos = "Pica Piedra", FechaNacimiento = DateTime.Parse("12/12/1991") }
            };
        }
        public Persona Get(int id) {
            return new Persona() { Id = id, Nombre = "Pepito", Apellidos = "Grillo" };
        }
    }

}
