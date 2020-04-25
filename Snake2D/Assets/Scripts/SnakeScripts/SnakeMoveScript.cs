using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	private List<GameObject> _snakeBodyParts;
	[SerializeField] private GameObject _bodyPrefab = null;

	[SerializeField] private Text _scoreUI = null;

	private Vector3 _previousBodyPartPos = new Vector3(0, 0, 0);
	private Vector3 _currentBodyPartPos = new Vector3(0, 0, 0);

	private void OnEnable()
	{
		BonusInteraction.AddBodyPart += IncreaseBodySize;
		GrowUp.AddBodyPart += IncreaseBodySize;
		SpeedUp.SpeedUpEvent += IncreaseSpeed;
	}

	private void OnDisable()
	{
		BonusInteraction.AddBodyPart -= IncreaseBodySize;
		GrowUp.AddBodyPart -= IncreaseBodySize;
		SpeedUp.SpeedUpEvent -= IncreaseSpeed;
	}

	private void Start()
	{
		transform.position = new Vector3(1f, 1f, 0);
		_gridPosition = new Vector2(0.5f, 0.5f);
		_snakeBodySize = 0;
		_snakeBodyParts = new List<GameObject>();
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
			_previousBodyPartPos = transform.position;
			transform.position += _moveDirection;
			MoveSnakeBodyParts();
		}
	}

	private void MoveSnakeBodyParts()
	{
		foreach (GameObject objectPos in _snakeBodyParts)
		{
			_currentBodyPartPos = objectPos.transform.position;
			objectPos.transform.position = _previousBodyPartPos;
			_previousBodyPartPos = _currentBodyPartPos;
		}
	}

	private void CheckOnHeadCollisionWithBody()
	{
		foreach (GameObject bodyPos in _snakeBodyParts)
		{
			if (_gridPosition.x == bodyPos.transform.position.x && _gridPosition.y == bodyPos.transform.position.y)
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
		GameObject bodySprite = Instantiate(_bodyPrefab);
		bodySprite.transform.position = _currentBodyPartPos;
		_snakeBodyParts.Add(bodySprite);
		IncreaseLengthUI?.Invoke(_snakeBodySize);
	}

	private void IncreaseSpeed()
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
