using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsApp.Core.Models;
using StudentsApp.Core.Services;
using StudentsApp.API.Resources;
using StudentsApp.API.Validators;

namespace StudentsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IMarkService _markService;
        private readonly IMapper _mapper;
        
        public MarksController(IMarkService markService, IMapper mapper)
        {
            _mapper = mapper;
            _markService = markService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarkResource>> GetMarkById(int id)
        {
            var mark = await _markService.GetMarkById(id);
            var markResource = _mapper.Map<Mark, MarkResource>(mark);

            return Ok(markResource);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarkResource>>> GetAllMarks()
        {
            var marks = await _markService.GetAllWithStudent();
            var markResources = _mapper.Map<IEnumerable<Mark>, IEnumerable<MarkResource>>(marks);

            return Ok(markResources);
        }

        [HttpPost]
        public async Task<ActionResult<MarkResource>> CreateMark([FromBody] SaveMarkResource saveMarkResource)
        {
            var validator = new SaveMarkResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMarkResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
    
            var markToCreate = _mapper.Map<SaveMarkResource, Mark>(saveMarkResource);
            var newMark = await _markService.CreateMark(markToCreate);
            var markResource = _mapper.Map<Mark, MarkResource>(newMark);

            return Ok(markResource);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<MarkResource>> UpdateMark(int id, [FromBody] SaveMarkResource saveMarkResource)
        {
            var validator = new SaveMarkResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMarkResource);
            
            var requestIsInvalid = id == 0 || !validationResult.IsValid;
            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);
            
            var mark = _mapper.Map<SaveMarkResource, Mark>(saveMarkResource);

            await _markService.UpdateMark(id, mark);

            var updatedMark = await _markService.GetMarkById(id);
            var updatedMarkResource = _mapper.Map<Mark, MarkResource>(updatedMark);

            return Ok(updatedMarkResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMark(int id)
        {
            if (id == 0)
                return BadRequest();
    
            var mark = await _markService.GetMarkById(id);
            if (mark == null)
                return NotFound();

            await _markService.DeleteMark(mark);

            return NoContent();
        }
    }
}