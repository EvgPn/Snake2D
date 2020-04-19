using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Text _maxScore = null;
	[SerializeField] private Text _maxLength = null;

	private void Start()
	{
		_maxScore.text = " Max score: " + PlayerPrefs.GetInt("Max score").ToString();
		_maxLength.text = " Max length: " + PlayerPrefs.GetInt("Max length").ToString();
	}

	public void PlayGameButton()
	{
		SceneManager.LoadScene("Level");
	}

	public void QuitButton()
	{
		Application.Quit();
	}
}
