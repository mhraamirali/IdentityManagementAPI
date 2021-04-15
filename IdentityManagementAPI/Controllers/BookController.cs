using AutoMapper;
using IdentityManagementAPI.Models;
using IdentityManagementAPI.Models.Dtos;
using IdentityManagementAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.Controllers
{
    [Route("book/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookController(IBookRepository bookRepository,IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookDto>))]
        public IActionResult GetBooks()
        {
            var objList = _bookRepository.GetBooks();
            var objDto = new List<BookDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<BookDto>(obj));
            }
            return Ok(objDto);
        }
        [HttpGet("{bookId:int}", Name = "GetBook")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetBook(int bookId)
        {
            var obj =_bookRepository.GetBook(bookId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<BookDto>(obj);
            return Ok(objDto);
        }
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_bookRepository.BookExists(bookDto.Name))
            {
                ModelState.AddModelError("", "Book already exist");
                return StatusCode(404, ModelState);
            }
            var objBook = _mapper.Map<Book>(bookDto);
            if (!_bookRepository.CreateBook(objBook))
            {
                ModelState.AddModelError("", $" Somthing went wrong {objBook.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetBook", new { bookId = objBook.Id }, objBook);
        }

        [HttpPatch("{bookId:int}", Name = "UpdateBook")]
        public IActionResult UpdateBook(int bookId, [FromBody] BookDto bookDto)
        {
            if (bookDto == null || bookId != bookDto.Id)
            {
                return BadRequest(ModelState);
            }
            var objBook = _mapper.Map<Book>(bookDto);
            if (!_bookRepository.UpdateBook(objBook))
            {
                ModelState.AddModelError("", $" Somthing went wrong {objBook.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{bookId:int}", Name = "DeleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return BadRequest();
            }
            var objBook = _bookRepository.GetBook(bookId);
            if (!_bookRepository.DeleteBook(objBook))
            {
                ModelState.AddModelError("", $" Somthing went wrong {objBook.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Book>>> Searh(string name)
        {
            try
            {
                var result = await _bookRepository.Search(name);
                if(result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while reteriving data");
            }
        }
    }
}
