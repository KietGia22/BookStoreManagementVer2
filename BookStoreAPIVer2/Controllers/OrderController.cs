using System.Net;
using System.Security.Claims;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Models;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPIVer2.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    protected APIResponse _response;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
        this._response = new APIResponse();
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> CreateOrder([FromBody] CreateOrderDTO order)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var orderDetail = await _orderService.CreateOrderAsync(order, userId);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = orderDetail;
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