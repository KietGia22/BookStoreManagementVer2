using System.Net;
using BookStoreAPIVer2.Models;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPIVer2.Controllers;

[ApiController]
[Route("api/time")]
public class TimeKeepingController : Controller
{
    private readonly ITimeService _timeService;
    protected APIResponse _response;

    public TimeKeepingController(ITimeService timeService)
    {
        _timeService = timeService;
        this._response = new APIResponse();
    }
    
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetAllTime()
    {
        try
        {
            var timeKeepingList = await _timeService.GetAllAsync();
            
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = timeKeepingList;
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
    
    [HttpPost("{accId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> CreateTimeKeeping(int accId)
    {
        try
        {
            var newTimeKeeping = await _timeService.AddAsync(accId);
            
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = newTimeKeeping;
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
    
    [HttpPut("{accId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateTimeKeeping(int accId)
    {
        try
        {
            var updateTimeKeeping = await _timeService.UpdateAsync(accId);
            
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = updateTimeKeeping;
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