using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentData.Data;
using StudentData.Model;

namespace StudentData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentApi _studentApi;

        public StudentController(StudentApi studentApi)
        {
            _studentApi=studentApi;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            if (_studentApi.students==null)
            {
               return NotFound();
            }
            return await _studentApi.students.ToListAsync();
        }

                [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_studentApi.students==null)
            {
               return NotFound();
            }
            var student= await _studentApi.students.FindAsync(id);
            if (student == null) {
                return NotFound();
            }
            return student;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            try
            {
                _studentApi.students.Add(student);
                await _studentApi.SaveChangesAsync();

                return CreatedAtAction(nameof(GetStudent), new { id = student.Student_Id }, student);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Student>> PutStudent(int id, Student student)
        {
            if (id!=student.Student_Id)
            {
                return BadRequest();
            }
            _studentApi.Entry(student).State = EntityState.Modified;

            try
            {
                await _studentApi.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!StudentAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
                return Ok();

        }
        private bool StudentAvailable(int id)
        {
            return (_studentApi.students?.Any(S_A=>S_A.Student_Id==id)).GetValueOrDefault(); 
        }
        [HttpDelete ("{id}")]

        public async Task<ActionResult<Student>> Delete_Student(int id)
        {
            if (_studentApi.students == null)
            {
                return NotFound();
            }
            var deleteStudent =await _studentApi.students.FindAsync(id);
            if (deleteStudent==null)
            {
                return NotFound();
            }
            _studentApi.students.Remove(deleteStudent);
            await _studentApi.SaveChangesAsync();
            return Ok();

        }
    }
}
