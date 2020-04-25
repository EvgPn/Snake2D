using UnityEngine;

public class Teleport : MonoBehaviour
{
	private static float _maxPositionValue = 14.5f;
	private static float _minPositionValue = -14.5f;

	public static Vector3 CheckCurrentPosition(Vector3 position)
	{
		if (position.x < _minPositionValue)
		{
			position.x = _maxPositionValue;
		}
		if (position.x > _maxPositionValue)
		{
			position.x = _minPositionValue;
		}
		if (position.y < _minPositionValue)
		{
			position.y = _maxPositionValue;
		}
		if (position.y > _maxPositionValue)
		{
			position.y = _minPositionValue;
		}

		return position;
	}
}
