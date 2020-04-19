using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
	[SerializeField] private Text _reachedScoreUI = null;
	[SerializeField] private Text _reachedLengthUI = null;

	private int _maxScore = 0;
	private int _maxLength = 0;
	private int _reachedScore = 0;
	private int _reachedLength = 0;

	private void Start()
	{
		GetPlayerPrefs();

		_reachedScoreUI.text = " Reached score: " + PlayerPrefs.GetInt("Reached score").ToString();
		_reachedLengthUI.text = " Reached length: " + PlayerPrefs.GetInt("Reached length").ToString();

		SetHigherScoreAndLenght();
	}

	private void GetPlayerPrefs()
	{
		_maxScore = PlayerPrefs.GetInt("Max score");
		_maxLength = PlayerPrefs.GetInt("Max length");
		_reachedScore = PlayerPrefs.GetInt("Reached score");
		_reachedLength = PlayerPrefs.GetInt("Reached length");
	}

	private void SetHigherScoreAndLenght()
	{
		if(_reachedScore > _maxScore)
		{
			PlayerPrefs.SetInt("Max score", _reachedScore);
		}
		if(_reachedLength > _maxLength)
		{
			PlayerPrefs.SetInt("Max length", _reachedLength);
		}
	}

	public void PlayAgainButton()
	{
		SceneManager.LoadScene("Level");
	}

	public void MainMenuButton()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
