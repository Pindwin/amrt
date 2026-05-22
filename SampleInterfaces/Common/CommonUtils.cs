using System;

namespace Common
{
	public enum CommandInvalidationReason
	{
		None,
		InvalidMove,
		WrongTurnOrder
	}
	
	/// <remarks>
	/// Intentionally empty - connection is an implementation detail.
	/// </remarks>
	public struct ConnectionDetails
	{ }
	
	/// <remarks>
	/// Only here to make code a little more expressive; selection of replicated fields is an implementation detail.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class DoNotReplicateAttribute : Attribute
	{ }
	
	public enum GameType
	{
		Undefined,
		PvP,
		PvE
	}
	
	/// <remarks>
	/// Intentionally empty - auth is an implementation detail.
	/// </remarks>
	public interface IAuth
	{ }
	
	/// <remarks>
	/// Intentionally empty - network identity is an implementation detail.
	/// </remarks>
	public struct PlayerIdentity
	{ }
	
	/// <remarks>
	/// Use this instead of Unity Vector2Int - it'll keep Runtime agnostic of 3rd party libraries.
	/// </remarks>
	public struct Vec2
	{
		public int X;
		public int Y;
	}
}