using API.Services;

namespace Test.Services;

public class ConwaysGameofLifeServiceTest
{
	public static ConwaysGameofLifeService Service => new();

	[Fact]
	public void NextState_Empty()
	{
		// Arrange
		var board = Array.Empty<bool[]>();
		// Act
		var result = Service.NextState(board);
		// Assert
		Assert.Equal(board, result);
	}

	[Fact]
	public void NextState_Single()
	{
		// Arrange
		var board = new bool[][] { new bool[] { true } };
		// Act
		var result = Service.NextState(board);
		// Assert
		Assert.NotEqual(board, result);
	}

	[Fact]
	public void NextState_SingleEmpty()
	{
		// Arrange
		var board = new bool[][] { Array.Empty<bool>(), new bool[] { true } };
		// Act
		var result = Service.NextState(board);
		// Assert
		Assert.Equal(board, result);
	}

	[Fact]
	public void NextState_3x3()
	{
		// Arrange
		var board = new bool[3][];
		board[0] = new bool[] { true, false, true };
		board[1] = new bool[] { false, true, false };
		board[2] = new bool[] { true, false, true };
		// Act
		var result = Service.NextState(board);
		// Assert
		Assert.NotEqual(board, result);
		Assert.DoesNotContain(result, x => x.Any(y => y));
	}

	[Fact]
	public void Rules_3x3_IsFalse()
	{
		// Arrange
		var board = new bool[3][];
		board[0] = new bool[] { true, false, true };
		board[1] = new bool[] { false, true, false };
		board[2] = new bool[] { true, false, true };
		// Act
		var result = Service.Rules(0, 0, board);
		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Rules_3x3_IsTrue()
	{
		// Arrange
		var board = new bool[3][];
		board[0] = new bool[] { true, false, false };
		board[1] = new bool[] { false, true, false };
		board[2] = new bool[] { false, false, true };
		// Act
		var result = Service.Rules(1, 1, board);
		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Neighbours_Empty_Is0()
	{
		// Arrange
		var board = Array.Empty<bool[]>();
		// Act
		var result = Service.Neighbours(0, 0, board);
		// Assert
		Assert.Equal(0, result);
	}

	[Fact]
	public void Neighbours_OutRange_Is0()
	{
		// Arrange
		var board = new bool[][] { new bool[] { true } };
		// Act
		var result = Service.Neighbours(-1, -1, board);
		// Assert
		Assert.Equal(0, result);
	}

	[Fact]
	public void Neighbours_OnRange_Is0()
	{
		// Arrange
		var board = new bool[][] { new bool[] { true } };
		// Act
		var result = Service.Neighbours(0, 0, board);
		// Assert
		Assert.Equal(0, result);
	}

	[Fact]
	public void Neighbours_3x3_Is4()
	{
		// Arrange
		var board = new bool[3][];
		board[0] = new bool[] { true, false, true };
		board[1] = new bool[] { false, true, false };
		board[2] = new bool[] { true, false, true };
		// Act
		var result = Service.Neighbours(0, 0, board);
		// Assert
		Assert.Equal(4, result);
	}

	[Fact]
	public void Neighbour_Empty_IsFalse()
	{
		// Arrange
		var board = Array.Empty<bool[]>();
		// Act
		var result = Service.Neighbour(-1, -1, board);
		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Neighbour_Range_IsFalse()
	{
		// Arrange
		var board = new bool[][] { new bool[] { true } };
		// Act
		var result = Service.Neighbour(-1, -1, board);
		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Neighbour_Range_IsTrue()
	{
		// Arrange
		var board = new bool[][] { new bool[] { true } };
		// Act
		var result = Service.Neighbour(0, 0, board);
		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Neighbour_3x3_IsFalse()
	{
		// Arrange
		var board = new bool[3][];
		board[0] = new bool[] { true, false, true };
		board[1] = new bool[] { false, true, false };
		board[2] = new bool[] { true, false, true };
		// Act
		var result = Service.Neighbour(-1, -1, board);
		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Neighbour_3x3_IsTrue()
	{
		// Arrange
		var board = new bool[3][];
		board[0] = new bool[] { true, false, true };
		board[1] = new bool[] { false, true, false };
		board[2] = new bool[] { true, false, true };
		// Act
		var result = Service.Neighbour(1, 1, board);
		// Assert
		Assert.True(result);
	}
}