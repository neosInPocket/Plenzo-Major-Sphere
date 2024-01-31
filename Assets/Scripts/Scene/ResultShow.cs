using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultShow : MonoBehaviour
{
	[SerializeField] private TMP_Text rewardText;
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private TMP_Text buttonText;
	[SerializeField] private Animator animator;
	private Action CloseAction;

	public void ShowResult(LevelGameResult result)
	{
		gameObject.SetActive(true);
		string buttonCaption = default;
		string rewardCaption = default;
		string resultCaption = default;

		if (result.LevelResult == LevelResult.Win)
		{
			buttonCaption = "NEXT LEVEL";
			resultCaption = "YOU WIN!";
		}
		else
		{
			buttonCaption = "RETRY";
			resultCaption = "YOU LOSE";
		}

		rewardCaption = "+" + result.CoinsAdded.ToString();

		rewardText.text = rewardCaption;
		resultText.text = resultCaption;
		buttonText.text = buttonCaption;
	}

	public void Menu()
	{
		animator.SetTrigger("close");
		CloseAction = () => SceneManager.LoadScene("MenuScene");
	}

	public void Game()
	{
		animator.SetTrigger("close");
		CloseAction = () => SceneManager.LoadScene("Game");
	}

	public void Collapse()
	{
		gameObject.SetActive(false);
		CloseAction();
	}
}

public enum LevelResult
{
	Win,
	Lose
}

public class LevelGameResult
{
	private LevelResult levelResult;
	private int coinsAdded;
	public LevelResult LevelResult => levelResult;
	public int CoinsAdded => coinsAdded;

	public LevelGameResult(LevelResult result, int coinsGained)
	{
		coinsAdded = coinsGained;
		levelResult = result;
	}
}
