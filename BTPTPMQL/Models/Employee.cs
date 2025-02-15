using System.ComponentModel.DataAnnotations;

namespace BTPTPMQL.Models
{
    public class Employee : Person
    {
        public string Id { get; set; }
        public int Age { get; set; }
    }
}