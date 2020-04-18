using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
	private int _maxSpawnPos = 8;
	private int _minSpaawnPos = -8;

	[SerializeField] private Bonus _slowTime = null;
	[SerializeField] private Bonus _speedUp = null;
	[SerializeField] private Bonus _growUp = null;

	private Vector3 _bonusPosition;

	private Bonus _bonusToSpawn;
	private GameObject _bonusGO;

	private void OnEnable()
	{
		BonusInteraction.SpawnNewBonus += SpawnBonus;
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
		_bonusPosition.x = Random.Range(_minSpaawnPos, _maxSpawnPos);
		_bonusPosition.y = Random.Range(_minSpaawnPos, _maxSpawnPos);
	}

	private void ChooseRandomBonus()
	{
		int bonusNum = Random.Range(1, 4);
		switch (bonusNum)
		{
			case 1:
				_bonusToSpawn = _slowTime;
				break;
			case 2:
				_bonusToSpawn = _speedUp;
				break;
			case 3:
				_bonusToSpawn = _growUp;
				break;
		}
	}

	private void SpawnBonusGO()
	{
		_bonusGO = new GameObject(_bonusToSpawn.BonusName, typeof(SpriteRenderer));
		_bonusGO.GetComponent<SpriteRenderer>().sprite = _bonusToSpawn.BonusIcon;
		_bonusGO.GetComponent<SpriteRenderer>().sortingOrder = 1;
		_bonusGO.transform.position = _bonusPosition;
	}
}
