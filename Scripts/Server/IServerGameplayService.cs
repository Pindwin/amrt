using Common;

namespace Server
{
	/// <summary>
	/// Handles player moves in a tandem with an active IGameRuntime object (held as a dependency).
	/// Works internally, server side.
	/// </summary>
	internal interface IServerGameplayService
	{
		/// <summary>
		/// Proposes next move to be commited by an active player.
		/// </summary>
		/// <param name="playerIndex">Index must IGameRuntime.ActiveRuntime for call to be validated.</param>
		/// <param name="direction">Offset from Snake head</param>
		/// <returns>
		/// - CommandInvalidationReason.Invalid move if direction has length != 1 or if cell is already occupied
		/// - CommandInvalidationReason.WrongTurnOrder if playerIndex is not IGameRuntime.ActiveIndex
		/// - CommandInvalidationReason.None if the action was valid.
		/// </returns>
		CommandInvalidationReason RequestMove(int playerIndex, Vec2 direction);
	}
}