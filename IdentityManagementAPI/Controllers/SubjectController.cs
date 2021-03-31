using AutoMapper;
using IdentityManagementAPI.Models;
using IdentityManagementAPI.Models.Dtos;
using IdentityManagementAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class SubjectController : ControllerBase
    {
        private ISubjectRepository _subjectRepository;
        private readonly IMapper _maper;
        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _maper = mapper;

        }
        [HttpGet]
        public IActionResult GetSubjects()
        {
            var objList = _subjectRepository.GetSubjects();
            var objDto = new List<SubjectDto>();
            foreach(var obj in objList)
            {
                objDto.Add(_maper.Map<SubjectDto>(obj));
            }
            return Ok(objDto);
        }
        [HttpGet("{subjectId:int}", Name = "GetSubject")]
        public IActionResult GetSubject(int subjectId)
        {
            var obj = _subjectRepository.GetSubject(subjectId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _maper.Map<SubjectDto>(obj);
            return Ok(objDto);
        }
        [HttpPost]
        public IActionResult Createsubject([FromBody] SubjectDto subjectDto)
        {
            if (subjectDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_subjectRepository.SubjectExists(subjectDto.Name))
            {
                ModelState.AddModelError("", "Subject already exist");
                return StatusCode(404, ModelState);
            }
            var objSubject = _maper.Map<Subject>(subjectDto);
            if(!_subjectRepository.CreateSubject(objSubject))
            {
                ModelState.AddModelError("", $" Somthing went wrong {objSubject.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetSubject", new {subjectId=objSubject.Id }, objSubject);
        }
        [HttpPatch("{subjectId:int}", Name = "UpdateSubject")]
        public IActionResult UpdateSubject(int subjectId, [FromBody] SubjectDto subjectDto)
        {
            if(subjectDto==null || subjectId!=subjectDto.Id)
            {
                return BadRequest(ModelState);
            }
            var objSubject = _maper.Map<Subject>(subjectDto);
            if(!_subjectRepository.UpdateSubject(objSubject))
            {
                ModelState.AddModelError("", $" Somthing went wrong {objSubject.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{subjectId:int}", Name = "DeleteSubject")]
        public IActionResult DeleteSubject(int subjectId)
        {
            if(!_subjectRepository.SubjectExists(subjectId))
            {
                return BadRequest();
            }
            var objSubject = _subjectRepository.GetSubject(subjectId);
            if(!_subjectRepository.DeleteSubject(objSubject))
            {
                ModelState.AddModelError("", $" Somthing went wrong {objSubject.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
