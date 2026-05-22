using Common;

namespace PublicServer
{
	/// <summary>
	/// Exposes simple read-only API for leaderboards.
	/// </summary>
	public interface ILobbyLeaderboardsAPI
	{
		void RequestOwnLeaderboardsPlacement(GameType gameType);
		
		/// <remarks>
		/// Paging should be validated server side against a reasonable value. 
		/// </remarks>
		void RequestLeaderboardsChunk(GameType gameType, int startIndex, int paging);
	}
}