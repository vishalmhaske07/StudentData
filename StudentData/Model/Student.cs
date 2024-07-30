using System.ComponentModel.DataAnnotations;

namespace StudentData.Model
{
    public class Student
    {
        [Key]
        public int Student_Id { get; set; }
        public string Student_Name { get; set; } 
        public string Student_Stand { get; set; } 
    }
}
