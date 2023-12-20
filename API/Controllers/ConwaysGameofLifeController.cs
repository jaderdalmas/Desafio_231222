using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController, Route("[controller]")]
public class ConwaysGameofLifeController : ControllerBase
{
	private readonly ILogger<ConwaysGameofLifeController> _logger;
	private readonly IConwaysGameofLifeService _boardService;
	private readonly IBoardsRepository _boardRepos;

	public ConwaysGameofLifeController(ILogger<ConwaysGameofLifeController> logger, IConwaysGameofLifeService boardService, IBoardsRepository boardRepos)
	{
		_logger = logger;
		_boardService = boardService;
		_boardRepos = boardRepos;
	}

	[HttpPost("[action]")]
	[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Guid))]
	[ProducesResponseType((int)HttpStatusCode.NoContent)]
	public IActionResult Board([FromBody] bool[][] board)
	{
		if (board == null || board.Length == 0 || board.Any(x => x.Length == 0)) return NoContent();

		return Ok(_boardRepos.AddBoard(board));
	}

	[HttpGet("[action]")]
	[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool[][]))]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public IActionResult NextState([FromQuery] Guid id, [FromQuery] int iterations = 1)
	{
		var result = _boardRepos.GetBoard(id);
		if (result == null) return NotFound();

		result = _boardService.NextState(result, iterations);
		return Ok(result);
	}

	[HttpGet("[action]")]
	[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Tuple<bool[][], int>))]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
	public IActionResult FinalState([FromQuery] Guid id, [FromQuery] int limit = 1000)
	{
		var board = _boardRepos.GetBoard(id);
		if (board == null) return NotFound();

		var result = _boardService.FinalState(board, limit);
		if (!result.IsFinal()) return UnprocessableEntity(result);
		return Ok(result);
	}
}