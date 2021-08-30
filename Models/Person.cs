using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace netcore.Models
{
    [Table("tb_m_persons")]
    public class Person
    {
        [Key] //Anontation
        public string NIK { get; set; } //Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public gender Gender { get; set; }
        public enum gender { Male, Female }
        public virtual Account Account { get; set; }
    }
}