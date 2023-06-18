using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person
    {
        public long PersonId { get; set; }
        [Required]
        [Display(Name = "IsUpdated = True")]
        public string PersonName { get; set; }
        [Required]
        [Display(Name = "IsUpdated = False")]

        public string TaxNumber { get; set; }
        public Person()
        {

        }
        public Person(string personName, string taxNumber)
        {
            PersonName = ValidateName(personName);
            TaxNumber = taxNumber;
        }
        public string ValidateName(string name)
        {
            if (!String.IsNullOrEmpty(name) && name.Length > 3)
                return name;

            throw new Exception("Nome Invalido");
        }
    }
}
