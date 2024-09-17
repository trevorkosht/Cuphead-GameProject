using System.Collections.Generic;

public static class EnemyFactory
{
	public static BaseEnemy CreateEnemy(EnemyType type)
	{
		switch (type)
		{
			case EnemyType.AggravatingAcorn:
				return new AggravatingAcorn();
			case EnemyType.DeadlyDaisy:
				return new DeadlyDaisy();
			case EnemyType.MurderousMushroom:
				return new MurderousMushroom();
			case EnemyType.TerribleTulip:
				return new TerribleTulip();
			case EnemyType.AcornMaker:
				return new AcornMaker();
			case EnemyType.BothersomeBlueberry:
				return new BothersomeBlueberry();
			case EnemyType.ToothyTerror:
				return new ToothyTerror();
			// Add other cases for different enemies here
			default:
				return null;
		}
	}
}

public enum EnemyType
{
	AggravatingAcorn,
	DeadlyDaisy,
	MurderousMushroom,
	TerribleTulip,
	AcornMaker,
	BothersomeBlueberry,
	ToothyTerror
	// Add other enemies here
}
