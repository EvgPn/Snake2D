using UnityEngine;

public class BonusInteraction : MonoBehaviour
{
	public delegate GameObject BonusSpawnerCallBack();
	public static event BonusSpawnerCallBack SpawnNewBonus;

	[SerializeField] private GameObject _snakeHead = null;	
	private GameObject _bonusGO;

	private void Start()
	{
		_bonusGO = SpawnNewBonus?.Invoke();
	}

	private void Update()
	{
		if ((int)_snakeHead.transform.position.x == _bonusGO.transform.position.x && (int)_snakeHead.transform.position.y == _bonusGO.transform.position.y)
		{
			Destroy(_bonusGO.gameObject);
			_bonusGO = SpawnNewBonus?.Invoke();
		}
	}
}
