using Common;

namespace Client
{
	/// <summary>
	/// Exposes response methods to server-side RPCs. 
	/// </summary>
	/// <remarks>
	/// Methods are bunched together for simplicity; if we wanted to be extra "correct", we should split them between Server and Lobby responses.
	/// </remarks>
	public interface IGameClientNetworkEndpoint
	{
		void ReceiveRuntimeReplication(byte[] replicationData);
		void ReceiveMoveErrorCode(CommandInvalidationReason errorCode);
		void ReceiveGameResult(bool localPlayerWon);
		void ReceiveLeaderboardPlacement(int firstIndex, int[] scores, PlayerIdentity[] playerIdentities);
		void ReceiveActiveMatchConnectionDetails(ConnectionDetails connectionDetails);
	}
}