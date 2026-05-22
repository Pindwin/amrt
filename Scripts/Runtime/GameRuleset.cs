using Common;

namespace Runtime
{
	public struct GameRuleset
	{
		public Vec2 BoardSize;
		
		[DoNotReplicate]
		public int FoodSpawnInterval;
		
		/// <summary>
		/// Use negative value for no timeout
		/// </summary>
		[DoNotReplicate]
		public float TurnTimeout;
	}
}