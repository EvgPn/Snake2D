using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour
{
	private Player _player;
	private int _clickCounter = 0;

	private void Start()
	{
		_player = new Player();
		_player.PlayerHit(_clickCounter);
	}

	public void ClickHitButton()
	{
		_clickCounter++;
		_player.PlayerHit(_clickCounter);
	}
}
