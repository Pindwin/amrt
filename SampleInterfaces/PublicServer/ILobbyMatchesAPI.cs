using Common;

namespace PublicServer
{
	/// <summary>
	/// Exposes simple CRUD-like API for matches.
	/// </summary>
	/// <remarks>
	/// There's a world of details to be explored when it comes to matchmaking, so I leave it empty intentionally, as I
	/// assume it goes out of the scope of this task. Each of those calls should:
	/// - validate legality of the request
	/// - perform it if legal (potentially spawning another process for a match)
	/// - send appropriate responses to interested parties (caller and potentially their opponent)
	/// </remarks>
	public interface ILobbyMatchesAPI
	{
		void CreateMatchRequest(PlayerIdentity player, PlayerIdentity opponent);
		void ListActiveMatchesRequest(PlayerIdentity playerIdentity);
		void ReconnectToMatchRequest(PlayerIdentity player, int matchId);
		void AbandonMatchRequest(PlayerIdentity player, int matchId);
	}
}