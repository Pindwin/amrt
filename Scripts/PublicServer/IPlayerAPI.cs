using Common;

namespace PublicServer
{
	/// <summary>
	/// Exposed to Client via RPCs.
	/// </summary>
	/// <remarks>
	/// Note, that player doesn't pass their index nor PlayerIdentity. Implementation needs to make sure it was
	/// a valid player that sent the request (to not allow cheating by forcing other player moves).
	/// </remarks>
	public interface IPlayerAPI
	{
		void MakeMoveRequest(Vec2 direction);
	}
}