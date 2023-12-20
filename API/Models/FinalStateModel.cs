namespace API.Models;

public class FinalStateModel
{
	public FinalStateModel(bool[][] board, int iterations)
	{
		Board = board;
		Iterations = iterations;
	}

	public bool[][] Board { get; set; }
	public int Iterations { get; set; }

	public bool IsFinal() => Board?.All(x => x.All(y => !y)) == true;
}
