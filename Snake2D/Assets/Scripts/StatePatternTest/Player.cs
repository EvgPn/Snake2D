using UnityEngine;

public class Player : MonoBehaviour
{
	private States _currentState;
	
	public Player()
	{
		this._currentState = new HealthyState();
	}

	public void PlayerHit(int clickNum)
	{
		if(clickNum == 0)
		{
			this._currentState = new HealthyState();
		}
		else if (clickNum == 1)
		{
			this._currentState = new HurtState();
		}
		else
		{
			this._currentState = new DeadState();
		}

		_currentState.ChangState(this);
	}
}
