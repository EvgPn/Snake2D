using UnityEngine;
using System.Collections;

public class BonusInteraction : MonoBehaviour
{
	public delegate GameObject BonusSpawnerCallBack();
	public static event BonusSpawnerCallBack SpawnNewBonus;

	public delegate void IncreaseBodySizeCallBack();
	public static event IncreaseBodySizeCallBack AddBodyPart;

	public delegate void IncreaseSpeedCallBack();
	public static event IncreaseSpeedCallBack SpeedUp;

	public delegate void ScoreCounterCallBack(int scrore);
	public static event ScoreCounterCallBack IncreaseScoreUI;


	[SerializeField] private GameObject _snakeBody = null;
	private GameObject _bonusGO;
	private bool _bonusPositionIsReached;
	private int _bonusScoreCounter;

	[SerializeField] private GameObject _pickUpVisualEffect = null;
	[SerializeField] private AudioSource _pickUpAudioClip = null;

	private void Start()
	{
		_bonusScoreCounter = 0;
		_bonusGO = SpawnNewBonus?.Invoke();
	}

	private void Update()
	{
		CheckSnakeAndBonusPosition();

		if (_bonusPositionIsReached)
		{
			_bonusScoreCounter++;
			IncreaseScoreUI?.Invoke(_bonusScoreCounter);
			GameObject pickUpVFX = Instantiate(_pickUpVisualEffect, _bonusGO.transform.position, Quaternion.Euler(0, 180, 0));
			Destroy(pickUpVFX, 0.5f);
			ChooseInteractionVariant();
			Destroy(_bonusGO.gameObject);
			_bonusGO = SpawnNewBonus?.Invoke();
			_pickUpAudioClip.Play();
			AddBodyPart?.Invoke();
		}
	}

	private void CheckSnakeAndBonusPosition()
	{
		if (_snakeBody.transform.position.x == _bonusGO.transform.position.x &&
			_snakeBody.transform.position.y == _bonusGO.transform.position.y)
		{
			_bonusPositionIsReached = true;
		}
		else
		{
			_bonusPositionIsReached = false;
		}
	}

	private void ChooseInteractionVariant()
	{
		if (_bonusGO.name == "GrowUp")
		{
			AddBodyPart?.Invoke();
		}
		if (_bonusGO.name == "SlowTime")
		{
			Time.timeScale = 0.5f;
			StartCoroutine(IncreaseTimeScale());
		}
		if (_bonusGO.name == "SpeedUp")
		{
			SpeedUp?.Invoke();
		}
	}

	private IEnumerator IncreaseTimeScale()
	{
		yield return new WaitForSeconds(2);
		Time.timeScale = 1f;
	}
}
