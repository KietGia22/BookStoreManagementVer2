using System.Net;
using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Models;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPIVer2.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    // GET
   private readonly IAuthServiece _authServiece;
   protected APIResponse _response;
   private readonly IMapper _mapper;
   public AuthController(IAuthServiece AuthService, IMapper mapper)
   {
       _authServiece = AuthService;
       _mapper = mapper;
       this._response = new();
   }

   [HttpPost("registerEmployee")]
   public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeRequestDTO registerEmployeeRequestDTO)
   {
       try
       {
           var employee = await _authServiece.RegisterEmployee(registerEmployeeRequestDTO);
           _response.Result = employee;
           _response.StatusCode = HttpStatusCode.OK;
           return Ok(_response);
       }
       catch (Exception ex)
       {
           _response.IsSuccess = false;
           _response.ErrorMessages = new List<string>() { ex.Message };
           _response.StatusCode = HttpStatusCode.BadRequest;
           return BadRequest(_response);
       }
   }

   [HttpPost("login")]
   public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
   {
       try
       {
           var tokenDto = await _authServiece.Login(loginRequestDTO);
           _response.StatusCode = HttpStatusCode.OK;
           _response.IsSuccess = true;
           _response.Result = tokenDto;
           return Ok(_response);
       }
       catch (Exception ex)
       {
           _response.IsSuccess = false;
           _response.ErrorMessages = new List<string>() { ex.Message };
           _response.StatusCode = HttpStatusCode.BadRequest;
           return BadRequest(_response);
       }
   }
}