using UnityEngine;
using System.Collections;

public class BonusInteraction : MonoBehaviour
{
	public delegate GameObject BonusSpawnerCallBack();
	public static event BonusSpawnerCallBack SpawnNewBonus;

	public delegate void ScoreCounterCallBack(int scrore);
	public static event ScoreCounterCallBack IncreaseScoreUI;

	public delegate void IncreaseBodySizeCallBack();
	public static event IncreaseBodySizeCallBack AddBodyPart;


	[SerializeField] private GameObject _snakeBody = null;
	private GameObject _bonusGO;
	private bool _bonusPositionIsReached;
	private int _bonusScoreCounter;

	[SerializeField] private GameObject _pickUpVisualEffect = null;
	[SerializeField] private AudioSource _pickUpAudioClip = null;


	private SpeedUp _increaseSpeed;
	private GrowUp _increaeLength;
	private SlowTime _slowTime;

	private void Start()
	{
		_increaseSpeed = new SpeedUp();
		_increaeLength = new GrowUp();
		_slowTime = new SlowTime();

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
			_increaeLength.PickUp();
		}
		if (_bonusGO.name == "SlowTime")
		{
			Time.timeScale = 0.5f;
			StartCoroutine(IncreaseTimeScale());
		}
		if (_bonusGO.name == "SpeedUp")
		{
			_increaseSpeed.PickUp();
		}
	}

	private IEnumerator IncreaseTimeScale()
	{
		yield return new WaitForSeconds(2);
		Time.timeScale = 1f;
	}
}
