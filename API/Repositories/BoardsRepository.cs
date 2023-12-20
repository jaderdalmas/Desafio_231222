namespace API.Repositories;

public interface IBoardsRepository
{
	/// <summary>
	/// Add board and return its id
	/// </summary>
	/// <param name="board">board to be added</param>
	/// <returns>id of the added board</returns>
	Guid AddBoard(bool[][] board);

	/// <summary>
	/// Get a board by id
	/// </summary>
	/// <param name="id">id of the board</param>
	/// <returns>null or board if exists</returns>
	bool[][]? GetBoard(Guid id);
}

public class BoardsRepository : IBoardsRepository
{
	Dictionary<Guid, bool[][]> Boards { get; set; } = new Dictionary<Guid, bool[][]>();

	public Guid AddBoard(bool[][] board)
	{
		var id = Guid.NewGuid();
		Boards.Add(id, board);

		return id;
	}

	public bool[][]? GetBoard(Guid id)
	{
		if (Boards.TryGetValue(id, out bool[][]? result)) return result;

		return null;
	}
}