﻿using Church.DTO;
using Church.Mapper;
using Church.Models;
using Church.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly RequestMapper _requestMapper;

    public RequestController(IRequestService requestService, RequestMapper requestMapper)
    {
        _requestService = requestService;
        _requestMapper = requestMapper;
    }

    // GET: api/Request
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequestDTO>>> GetAllRequests()
    {
        var requests = await _requestService.GetAllRequests();
        return Ok(requests.Select(r => _requestMapper.MapToRequestDTO(r)));
    }

    // GET: api/Request/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RequestDTO>> GetRequest(string id)
    {
        var request = await _requestService.GetRequest(id);

        if (request == null)
        {
            return NotFound();
        }

        return _requestMapper.MapToRequestDTO(request);
    }

    // POST: api/Request
    [HttpPost]
    public async Task<ActionResult<RequestDTO>> PostRequest(Request request)
    {
        await _requestService.AddRequest(request);
        return CreatedAtAction("GetRequest", new { id = request.Id }, _requestMapper.MapToRequestDTO(request));
    }

    // DELETE: api/Request/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRequest(string id)
    {
        await _requestService.DeleteRequest(id);
        return Ok();
    }

    // GET: api/Request/Date/{date}
    [HttpGet("Date/{date}")]
    public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequestsByDate(DateTime date)
    {
        var requests = await _requestService.GetRequestsByDate(date);

        if (requests == null)
        {
            return NotFound();
        }

        return Ok(requests.Select(r => _requestMapper.MapToRequestDTO(r)));
    }
}
