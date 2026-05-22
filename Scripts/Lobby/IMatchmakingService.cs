using Common;

namespace Lobby
{
	/// <summary>
	/// Exposes method for inter-process communication server side, to report match finished by a game.
	/// </summary>
	public interface IMatchmakingService
	{
		public void ReportMatchResult(int matchId, PlayerIdentity winner, int score);
	}
}