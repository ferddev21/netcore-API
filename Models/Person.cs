using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        [JsonConverter(typeof(StringEnumConverter))]
        public gender Gender { get; set; }
        public enum gender { Male, Female }

        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}