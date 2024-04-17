using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaCRUD.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string DocumentType { get; set; } = null!;
        public string DocumentNumber { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Telephone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
