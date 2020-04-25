using UnityEngine;
using UnityEngine.UI;

public class HUDinfo : MonoBehaviour
{
	[SerializeField] private Text _length = null;
	[SerializeField] private Text _score = null;
	[SerializeField] private Text _playTime = null;

	private float _playTimeCounter;

	private void OnEnable()
	{
		SnakeMoveScript.IncreaseLengthUI += SnakeLengthUI;
		BonusInteraction.IncreaseScoreUI += ScoreUI;
	}

	private void OnDisable()
	{
		SnakeMoveScript.IncreaseLengthUI -= SnakeLengthUI;
		BonusInteraction.IncreaseScoreUI -= ScoreUI;
	}

	private void Start()
	{
		_playTimeCounter = 0.0f;
	}

	private void Update()
	{
		_playTimeCounter += Time.deltaTime;
		_playTime.text = " Time: " + Mathf.RoundToInt(_playTimeCounter).ToString();
	}

	private void SnakeLengthUI(int bodySize)
	{
		_length.text = " Length: " + bodySize.ToString();
	}

	private void ScoreUI(int score)
	{
		_score.text = score.ToString();
	}
}
