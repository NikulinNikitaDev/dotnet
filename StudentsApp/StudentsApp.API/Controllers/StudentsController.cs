using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentsApp.Core.Models;
using StudentsApp.Core.Services;
using StudentsApp.API.Resources;

namespace StudentsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<SaveStudentResource> _validator;
        
        public StudentsController(IStudentService studentService, IMapper mapper, AbstractValidator<SaveStudentResource> validator)
        {
            _mapper = mapper;
            _studentService = studentService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResource>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            var studentResources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            return Ok(studentResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResource>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            var studentResource = _mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        [HttpPost]
        public async Task<ActionResult<StudentResource>> CreateStudent([FromBody] SaveStudentResource saveStudentResource)
        {
            var validationResult = await _validator.ValidateAsync(saveStudentResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var studentToCreate = _mapper.Map<SaveStudentResource, Student>(saveStudentResource);
            var newStudent = await _studentService.CreateStudent(studentToCreate);
            var studentResource = _mapper.Map<Student, StudentResource>(newStudent);

            return Ok(studentResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResource>> UpdateStudent(int id, [FromBody] SaveStudentResource saveStudentResource)
        {
            var validationResult = await _validator.ValidateAsync(saveStudentResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var student = _mapper.Map<SaveStudentResource, Student>(saveStudentResource);

            await _studentService.UpdateStudent(id, student);

            var updatedStudent = await _studentService.GetStudentById(id);
            var updatedStudentResource = _mapper.Map<Student, StudentResource>(updatedStudent);

            return Ok(updatedStudentResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.GetStudentById(id);

            await _studentService.DeleteStudent(student);
            
            return NoContent();
        }
    }
}