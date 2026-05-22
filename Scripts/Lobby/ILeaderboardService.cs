using Common;

namespace Lobby
{
	/// <summary>
	/// Updates player placement on leaderboards. Called internally, lobby side.
	/// </summary>
	internal interface ILeaderboardService
	{
		public void SubmitScore(PlayerIdentity player, int score);
	}
}