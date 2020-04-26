using UnityEngine;

public interface States
{
	void ChangState(Player player);
}

public class HealthyState : States
{
	public void ChangState(Player player)
	{	
		Debug.Log("100% health");
	}
}

public class HurtState : States
{
	public void ChangState(Player player)
	{		
		Debug.Log("50% health");
	}
}

public class DeadState : States
{
	public void ChangState(Player player)
	{
		Debug.Log("Player is dead");
	}
}


