using System.Net;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Models;
using BookStoreAPIVer2.Services;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPIVer2.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : Controller
{
    private readonly ICategory _categoryService;
    protected APIResponse _response;

    public CategoryController(ICategory categoryService)
    {
        _categoryService = categoryService;
        this._response = new APIResponse();
    }

    [HttpGet("GetAllCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetAllCategories([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
    {
        try
        {
            IEnumerable<CategoryDTO> categories;
            categories = await _categoryService.GetAllAsync();
            _response.Result = categories;
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
        return _response;
    }

    [HttpGet("{id:int}", Name = "GetCategoryById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetCategoryById(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var category = await _categoryService.GetAsync(id);
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
    public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CategoryDTO categoryDto)
    {
        try
        {
            if (categoryDto == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { "Bad Request" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            
            await _categoryService.CreateAsync(categoryDto);
            
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = categoryDto;
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

    [HttpPut("{id:int}", Name = "UpdateCategory")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateCategory(int id, [FromBody] CategoryDTO categoryDto)
    {
        try
        {
            if (categoryDto == null || id != categoryDto.CategoryId)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { "Bad Request" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            await _categoryService.UpdateAsync(categoryDto);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = categoryDto;
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

    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> RemoveCategory(int id)
    {
        try
        {
            await _categoryService.RemoveAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
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