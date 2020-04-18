using UnityEngine;

public class SnakeMoveScript : MonoBehaviour
{
	[SerializeField] private Vector3 _moveDirection = new Vector3(0, 0, 0);
	[SerializeField] private float _moveSpeed = 0f;

	private Vector3 _upDirection = new Vector3(0, 1, 0);
	private Vector3 _downDirection = new Vector3(0, -1, 0);
	private Vector3 _rightDirection = new Vector3(1, 0, 0);
	private Vector3 _leftDirection = new Vector3(-1, 0, 0);

	private void Start()
	{
		transform.position = new Vector3(0.5f, 0.5f, 0);
	}

	private void Update()
	{
		Move();
		CheckKeyDownState();
		Rotate();
	}

	private void Move()
	{
		transform.position += _moveDirection * _moveSpeed;
		transform.position = Teleport.CheckCurrentPosition(transform.position);
	}

	private void Rotate()
	{
		float angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
	}

	private void CheckKeyDownState()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && _moveDirection != _downDirection)
		{
			RoundHorizontalPosition();
			_moveDirection = _upDirection;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) && _moveDirection != _upDirection)
		{
			RoundHorizontalPosition();
			_moveDirection = _downDirection;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && _moveDirection != _leftDirection)
		{
			RoundVerticalDirection();
			_moveDirection = _rightDirection;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) && _moveDirection != _rightDirection)
		{
			RoundVerticalDirection();
			_moveDirection = _leftDirection;
		}
	}

	private void RoundHorizontalPosition()
	{
		if (_moveDirection == _rightDirection)
		{
			transform.position = new Vector3(Mathf.Ceil(transform.position.x), transform.position.y);
		}
		if (_moveDirection == _leftDirection)
		{
			transform.position = new Vector3(Mathf.Floor(transform.position.x), transform.position.y);
		}
	}

	private void RoundVerticalDirection()
	{
		if (_moveDirection == _upDirection)
		{
			transform.position = new Vector3(transform.position.x, Mathf.Ceil(transform.position.y));
		}
		if (_moveDirection == _downDirection)
		{
			transform.position = new Vector3(transform.position.x, Mathf.Floor(transform.position.y));
		}
	}
}
