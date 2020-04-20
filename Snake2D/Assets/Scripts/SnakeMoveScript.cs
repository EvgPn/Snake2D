using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class SnakeMoveScript : MonoBehaviour
{
	public delegate void SnakeLegthCallBack(int bodySize);
	public static event SnakeLegthCallBack IncreaseLengthUI;

	[SerializeField] private Vector3 _moveDirection = new Vector3(0, 0, 0);

	private Vector3 _upDirection = new Vector3(0, 1, 0);
	private Vector3 _downDirection = new Vector3(0, -1, 0);
	private Vector3 _rightDirection = new Vector3(1, 0, 0);
	private Vector3 _leftDirection = new Vector3(-1, 0, 0);

	private Vector2 _gridPosition;

	private float _timeToMove;
	[SerializeField] private float _moveRate = 0.1f;

	private int _snakeBodySize;
	private List<Vector2> _snakeBodyPositions;
	[SerializeField] private GameObject _bodyPrefab = null;

	[SerializeField] private Text _scoreUI = null;

	private void OnEnable()
	{
		BonusInteraction.AddBodyPart += IncreaseBodySize;
		BonusInteraction.SpeedUp += SpeedUp;
	}

	private void OnDisable()
	{
		BonusInteraction.AddBodyPart -= IncreaseBodySize;
		BonusInteraction.SpeedUp -= SpeedUp;
	}

	private void Start()
	{
		transform.position = new Vector3(1f, 1f, 0);
		_gridPosition = new Vector2(0.5f, 0.5f);
		_snakeBodySize = 0;
		_snakeBodyPositions = new List<Vector2>();
	}

	private void Update()
	{
		Move();
		CheckKeyDownState();
		CheckOnHeadCollisionWithBody();
	}

	private void Move()
	{
		transform.position = new Vector3(_gridPosition.x, _gridPosition.y);
		CheckMoveRate();

		transform.position = Teleport.CheckCurrentPosition(transform.position);
		_gridPosition = transform.position;
	}

	private void CheckMoveRate()
	{
		_timeToMove += Time.deltaTime;
		if (_timeToMove >= _moveRate)
		{

			_timeToMove = 0;
			_snakeBodyPositions.Insert(0, _gridPosition);
			transform.position += _moveDirection;
			ControllSnakeLength();

		}
	}

	private void ControllSnakeLength()
	{
		if (_snakeBodyPositions.Count >= _snakeBodySize + 1)
		{
			_snakeBodyPositions.RemoveAt(_snakeBodyPositions.Count - 1);
		}

		for (int i = 0; i < _snakeBodyPositions.Count; i++)
		{
			Vector2 snakeMovePosition = _snakeBodyPositions[i];
			GameObject bodySprite = Instantiate(_bodyPrefab, snakeMovePosition, Quaternion.identity);
			Destroy(bodySprite, _moveRate);
		}
	}

	private void CheckOnHeadCollisionWithBody()
	{
		foreach (Vector2 bodyPos in _snakeBodyPositions)
		{
			if (_gridPosition == bodyPos)
			{
				PlayerPrefs.SetInt("Reached length", _snakeBodySize);
				PlayerPrefs.SetInt("Reached score", int.Parse(_scoreUI.text));
				SceneManager.LoadScene("GameEnd");
			}
		}
	}

	private void CheckKeyDownState()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && _moveDirection != _downDirection && _moveDirection != _upDirection)
		{
			_moveDirection = _upDirection;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) && _moveDirection != _upDirection && _moveDirection != _downDirection)
		{
			_moveDirection = _downDirection;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && _moveDirection != _leftDirection && _moveDirection != _rightDirection)
		{
			_moveDirection = _rightDirection;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) && _moveDirection != _rightDirection && _moveDirection != _leftDirection)
		{
			_moveDirection = _leftDirection;
		}
	}

	private void IncreaseBodySize()
	{
		_snakeBodySize++;
		IncreaseLengthUI?.Invoke(_snakeBodySize);
	}

	private void SpeedUp()
	{
		_moveRate /= 2;
		StartCoroutine(SlowSpeed());
	}

	private IEnumerator SlowSpeed()
	{
		yield return new WaitForSeconds(2);
		_moveRate *= 2;
	}
}
