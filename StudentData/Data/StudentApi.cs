using Microsoft.EntityFrameworkCore;
using StudentData.Model;

namespace StudentData.Data
{
    public class StudentApi: DbContext
    {
        public StudentApi( DbContextOptions options):base(options)  {         

        }
        public DbSet<Student> students {  get; set; }
    }
}
