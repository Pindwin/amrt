using System.Collections.Generic;
using Common;

namespace Runtime
{
	/// <summary>
	/// Represents a game of "Turowy Snake"®.
	/// </summary>
	/// <remarks>
	/// Game doesn't hold any sort of spatial data - indexes are ordered 0 to Width*Length - 1, left to right, top to bottom.
	/// Values held by Player1Indexes, Player2Indexes and FoodIndexes are indexes of single-dimensional array using such indexing.
	/// </remarks>
	public interface IGameplayRuntime
	{
		public GameRuleset GameRuleset { get; }
		
		/// <summary>
		/// Uses convention to store a snake. Index 0 represents a head.
		/// </summary>
		public IList<int> Player1Indexes { get; }
		
		/// <summary>
		/// Uses convention to store a snake. Index 0 represents a head.
		/// </summary>
		public IList<int> Player2Indexes { get; }
		
		public IList<int> FoodIndexes { get; }
		
		public int ActivePlayer { get; }
		
		[DoNotReplicate]
		public float TurnStartTime { get; }
		
		[DoNotReplicate]
		public float CurrentTime { get; set; }
		
		/// <summary>
		/// For current ActivePlayer, would a move in a direction be possible?
		/// </summary>
		bool CanMoveInDirection(Vec2 direction);
		
		/// <summary>
		/// Queries whether the game has finished - i.e. ActivePlayer is unable to make a legal move.
		/// </summary>
		/// <returns>
		/// - -1 if the game is not finished yet
		/// - Index of victorious player otherwise.
		/// </returns>
		int GetGameWinner();
	}
}