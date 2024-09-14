using System.Net;
using BookStoreAPIVer2.DTOs.Cart;
using BookStoreAPIVer2.Models;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPIVer2.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    protected APIResponse _response;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
        this._response = new APIResponse();
    }

    [HttpGet("{customerId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetCartByCustomerId(int customerId)
    {
        try
        {
            var cartByCustomerId = await _cartService.GetCartByCustomerId(customerId);
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = cartByCustomerId;
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> RemoveItemToCart(int id)
    {
        try
        {
            await _cartService.RemoveItemAsync(id);
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

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateQuantityToCart([FromBody] UpdateCartDTO cart)
    {
        try
        {
            var cartToUpdate = await _cartService.UpdateQuantityAsync(cart);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = cartToUpdate;
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
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> AddItemToCart([FromBody] CreateCartDTO cart)
    {
        try
        {
            var cartToAdd = await _cartService.AddItemAsync(cart);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = cartToAdd;
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