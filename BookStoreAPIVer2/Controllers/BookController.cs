using System.Net;
using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Models;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPIVer2.Controllers;

[ApiController]
[Route("api/book")]
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;
    protected APIResponse _response;

    public BookController(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
        this._response = new APIResponse();
    }
    
    [HttpGet("GetAllBooks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetAllBooks([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
    {
        try
        {
            IEnumerable<BookDTO> books;
            books = await _bookService.GetAllAsync();
            _response.Result = books;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        } 
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            _response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
    }
    
    [HttpGet("{id:int}", Name = "GetBookById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetBookById(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var category = await _bookService.GetAsync(id);
            if (category == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound();
            }

            _response.Result = category;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        } catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            _response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
        return _response;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CreateBookDTO bookDto)
    {
        try
        {
            if (bookDto == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { "Bad Request" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var bookToCreate = _mapper.Map<BookDTO>(bookDto);
            await _bookService.CreateAsync(bookToCreate);
            
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = "New book created";
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            _response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
    }

    [HttpPut("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateCategory(int id, [FromBody] UpdateBookDTO bookDto)
    {
        try
        {
            if (bookDto == null || id != bookDto.BookId)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { "Bad Request" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            
            var bookToUpdate = _mapper.Map<BookDTO>(bookDto);
            await _bookService.UpdateAsync(bookToUpdate);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = bookDto;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            _response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
    }
}