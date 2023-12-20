using API.Models;

namespace API.Services;

public interface IConwaysGameofLifeService
{
	FinalStateModel FinalState(bool[][] board, int limit = 1000);

	bool[][] NextState(bool[][] board, int iterations = 1);
}

public class ConwaysGameofLifeService : IConwaysGameofLifeService
{
	public FinalStateModel FinalState(bool[][] board, int limit)
	{
		var result = NextState(board);
		for (int iteration = 1; iteration < limit; iteration++)
		{
			if(result.All(x => x.All(y => !y)))
				return new FinalStateModel(result, iteration);

			result = NextState(result); 
		}

		return new FinalStateModel(result, limit);
	}

	public bool[][] NextState(bool[][] board, int iterations)
	{
		var result = NextState(board);
		for (int i = 1; i < iterations; i++)
			result = NextState(result);

		return result;
	}

	public bool[][] NextState(bool[][] board)
	{
		bool[][] result = new bool[board.Length][];
		if (board == null || board.Length == 0) return result;

		for (int x = 0; x < result.Length; x++)
		{
			result[x] = new bool[board[x].Length];
			for (int y = 0; y < result[x].Length; y++)
				result[x][y] = Rules(x, y, board);
		}

		return result;
	}

	public bool Rules(int x, int y, bool[][] previous)
	{
		if (previous == null || previous.Length == 0) return false;
		if (x < 0 || x >= previous.Length || y < 0 || y >= previous[x].Length) return false;
		var current = previous[x][y];
		var neighbours = Neighbours(x, y, previous);

		if (current)
		{ // Any live cell with
		  // fewer than two live neighbours dies, as if by underpopulation
			if (neighbours < 2) return false;
			//  more than three live neighbours dies, as if by overpopulation
			else if (neighbours > 3) return false;
			// two or three live neighbours lives on to the next generation
			else return true;
		}
		else
		{ // Any dead cell with
		  // exactly three live neighbours becomes a live cell, as if by reproduction
			if (neighbours == 3) return true;
			else return false;
		}
	}

	public int Neighbours(int x, int y, bool[][] previous)
	{
		if (previous == null || previous.Length == 0) return 0;
		if (x < 0 || x >= previous.Length || y < 0 || y >= previous[x].Length) return 0;
		var neighbours = new List<bool>();

		var xMinus = (previous.Length + x - 1) % previous.Length;
		var yMinus = previous[xMinus].Length == 0 ? y - 1 : (previous[xMinus].Length + y - 1) % previous[xMinus].Length;
		var yPlus = previous[xMinus].Length == 0 ? y + 1 : (previous[xMinus].Length + y + 1) % previous[xMinus].Length;
		if (xMinus != x || yMinus != y) neighbours.Add(Neighbour(xMinus, yMinus, previous));
		if (xMinus != x) neighbours.Add(Neighbour(xMinus, y, previous));
		if (xMinus != x || (yPlus != y && yMinus != yPlus)) neighbours.Add(Neighbour(xMinus, yPlus, previous));

		yMinus = previous[x].Length == 0 ? y - 1 : (previous[x].Length + y - 1) % previous[x].Length;
		yPlus = previous[x].Length == 0 ? y + 1 : (previous[x].Length + y + 1) % previous[x].Length;
		if (yMinus != y) neighbours.Add(Neighbour(x, yMinus, previous));
		if (yPlus != y) neighbours.Add(Neighbour(x, yPlus, previous));

		var xPlus = (previous.Length + x + 1) % previous.Length;
		yMinus = previous[xPlus].Length == 0 ? y - 1 : (previous[xPlus].Length + y - 1) % previous[xPlus].Length;
		yPlus = previous[xPlus].Length == 0 ? y + 1 : (previous[xPlus].Length + y + 1) % previous[xPlus].Length;
		if (xPlus != x || yMinus != y) neighbours.Add(Neighbour(xPlus, yMinus, previous));
		if (xPlus != x) neighbours.Add(Neighbour(xPlus, y, previous));
		if (xPlus != x || (yPlus != y && yMinus != yPlus)) neighbours.Add(Neighbour(xPlus, yPlus, previous));

		return neighbours.Count(x => x);
	}

	public bool Neighbour(int x, int y, bool[][] previous)
	{
		if (previous == null || previous.Length == 0) return false;
		if (x < 0 || x >= previous.Length || y < 0 || y >= previous[x].Length) return false;

		return previous[x][y];
	}
}