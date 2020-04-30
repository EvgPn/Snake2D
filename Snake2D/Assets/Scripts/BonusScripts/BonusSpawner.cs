using UnityEngine;
using System.Collections.Generic;

public class BonusSpawner : MonoBehaviour
{
	public static BaseBonus _bonus;
	private Bonus _bonusToSpawn;

	[SerializeField] private List<Bonus> _bonusList = new List<Bonus>();
	[SerializeField] private List<BaseBonus> _bonusProperties = new List<BaseBonus>();

	private int _maxSpawnPos = 14;
	private int _minSpaawnPos = -14;

	private Vector3 _bonusPosition;
	private GameObject _bonusGO;

	private void OnEnable()
	{
		BonusInteraction.SpawnNewBonus += SpawnBonus;
	}

	private void OnDisable()
	{
		BonusInteraction.SpawnNewBonus -= SpawnBonus;
	}

	public GameObject SpawnBonus()
	{
		ChooseRandomPosition();
		ChooseRandomBonus();
		SpawnBonusGO();
		return _bonusGO;
	}

	private void ChooseRandomPosition()
	{
		_bonusPosition.x = Random.Range(_minSpaawnPos, _maxSpawnPos) + 0.5f;
		_bonusPosition.y = Random.Range(_minSpaawnPos, _maxSpawnPos) + 0.5f;
	}

	private void ChooseRandomBonus()
	{
		System.Random rand = new System.Random();
		int indexOfRandomElement = rand.Next(_bonusList.Count);
		_bonusToSpawn = _bonusList[indexOfRandomElement];
		_bonus = _bonusProperties[indexOfRandomElement];
	}

	private void SpawnBonusGO()
	{
		_bonusGO = new GameObject(_bonusToSpawn.BonusName, typeof(SpriteRenderer));
		_bonusGO.GetComponent<SpriteRenderer>().sprite = _bonusToSpawn.BonusIcon;
		_bonusGO.GetComponent<SpriteRenderer>().sortingOrder = 1;
		_bonusGO.transform.position = _bonusPosition;
	}
}
