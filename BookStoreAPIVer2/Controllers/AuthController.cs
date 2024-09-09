using System.Net;
using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Helper;
using BookStoreAPIVer2.Models;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPIVer2.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
   private readonly IAuthServiece _authServiece;
   protected APIResponse _response;
   private readonly IMapper _mapper;
   public AuthController(IAuthServiece AuthService, IMapper mapper)
   {
       _authServiece = AuthService;
       _mapper = mapper;
       this._response = new();
   }

   [HttpPost("RegisterEmployee")]
   [Authorize]
   public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeRequestDTO registerEmployeeRequestDTO)
   {
       var role = CheckPermission.CheckRoleOfAccount(Request);
       if (role != "Quan ly")
       {
           _response.IsSuccess = false;
           _response.ErrorMessages = new List<string>() { "You do not have permission to access this resource." };
           _response.StatusCode = HttpStatusCode.Unauthorized;
           return Unauthorized(_response);
       }
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

   [HttpPost("RegisterCustomer")]
   [Authorize]
   public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequestDTO registerCustomerRequestDTO)
   {
       try
       {
           var customer = await _authServiece.RegisterCustomer(registerCustomerRequestDTO);
           _response.Result = customer;
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

   [HttpPut("UpdateEmployee")]
   [Authorize]
   public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDTO updateNhanVienDTO)
   {
       var role = CheckPermission.CheckRoleOfAccount(Request);
       if (role != "Quan ly")
       {
           _response.IsSuccess = false;
           _response.ErrorMessages = new List<string>() { "You do not have permission to access this resource." };
           _response.StatusCode = HttpStatusCode.Unauthorized;
           return Unauthorized(_response);
       }
       try
       {
            var employee = await _authServiece.UpdateEmployee(updateNhanVienDTO);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            _response.Result = employee;
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

   [HttpDelete("{id:int}", Name = "RemoveEmployee")]
   [Authorize]
   public async Task<IActionResult> RemoveEmployee(int id)
   {
       var role = CheckPermission.CheckRoleOfAccount(Request);
       if (role != "Quan ly")
       {
           _response.IsSuccess = false;
           _response.ErrorMessages = new List<string>() { "You do not have permission to access this resource." };
           _response.StatusCode = HttpStatusCode.Unauthorized;
           return Unauthorized(_response);
       }
       try
       {
           await _authServiece.RemoveEmployee(id);
           _response.StatusCode = HttpStatusCode.NoContent;
           _response.IsSuccess = true;
           return Ok(_response);
       }
       catch (KeyNotFoundException ex)
       {
           _response.IsSuccess = false;
           _response.ErrorMessages = new List<string>() { ex.Message };
           _response.StatusCode = HttpStatusCode.NotFound;
           return NotFound(_response);
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