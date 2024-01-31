using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagment : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
	[SerializeField] private List<LaserGun> laserGuns;
	[SerializeField] private List<Image> lifes;
	[SerializeField] private Timer timer;
	[SerializeField] private TutorialManagment tutorialManagment;
	[SerializeField] private ReadyScreen readyScreen;
	[SerializeField] private ResultShow resultShow;
	private float seconds => 2 * Mathf.Log(OptionsLoader.Options.levelProgress + 1) + 10;
	private int levelReward => (int)(3 * Mathf.Log(OptionsLoader.Options.levelProgress + 1) + 3 + OptionsLoader.Options.extraTimeUpgrade);


	private void Start()
	{
		EnableLifes(OptionsLoader.Options.healthUpgrade);
		playerController.Damage += OnPlayerTakeDamage;

		if (!OptionsLoader.Options.alreadyEntered)
		{
			OptionsLoader.Options.alreadyEntered = true;
			OptionsLoader.SaveOptions();

			tutorialManagment.TutorialEnd += OnTutorialCompleted;
			tutorialManagment.PlayEntry();
		}
		else
		{
			OnTutorialCompleted();
		}
	}

	private void OnTutorialCompleted()
	{
		readyScreen.Show(OnReady);
	}

	private void OnReady()
	{
		EnableGuns();
		playerController.Enabled = true;
		timer.SetTimer(seconds, OnWin);
	}

	private void OnWin()
	{
		DisableAll();
		resultShow.ShowResult(new LevelGameResult(LevelResult.Win, levelReward));

		OptionsLoader.Options.coins += levelReward;
		OptionsLoader.Options.levelProgress++;
		OptionsLoader.SaveOptions();
	}

	private void OnPlayerTakeDamage(int lifesLeft)
	{
		if (lifesLeft == 0)
		{
			DisableAll();
			resultShow.ShowResult(new LevelGameResult(LevelResult.Lose, 0));
		}

		EnableLifes(lifesLeft);
	}

	private void EnableGuns()
	{
		laserGuns.ForEach(x => x.Active = true);
	}

	private void DisableGuns()
	{
		laserGuns.ForEach(x => x.Active = false);
	}

	private void DisableAll()
	{
		DisableGuns();
		playerController.Enabled = false;
		timer.StopTimer();
	}

	private void EnableLifes(int value)
	{
		lifes.ForEach(x => x.enabled = false);
		for (int i = 0; i < value; i++)
		{
			lifes[i].enabled = true;
		}
	}

	public void Menu()
	{
		SceneManager.LoadScene("MenuScene");
	}

	public void Retry()
	{
		SceneManager.LoadScene("Game");
	}
}
