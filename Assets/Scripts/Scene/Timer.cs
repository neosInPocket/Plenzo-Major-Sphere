using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	[SerializeField] private Image fill;
	[SerializeField] private TMP_Text timerText;
	[SerializeField] private TMP_Text levelCaption;
	[SerializeField] private List<LaserGun> laserGuns;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private ReadyScreen readyScreen;
	[SerializeField] private GameObject pauseWindow;
	private float timeElapsed;
	private float goal;
	private bool started = false;
	private Action WinAction;

	public void SetTimer(float seconds, Action winAction)
	{
		WinAction = winAction;
		timeElapsed = seconds;
		goal = seconds;
		started = true;
	}

	private void Update()
	{
		if (!started) return;
		if (timeElapsed <= 0)
		{
			started = false;
			WinAction();
			return;
		}

		fill.fillAmount = timeElapsed / goal;
		timerText.text = ((int)timeElapsed).ToString();
		timeElapsed -= Time.deltaTime;
	}

	public void StopTimer()
	{
		started = false;
	}

	public void PauseGame()
	{
		pauseWindow.SetActive(true);
		laserGuns.ForEach(x => x.Active = false);
		playerController.Freeze();
		started = false;
	}

	public void UnPause()
	{
		pauseWindow.SetActive(false);
		readyScreen.Show(OnUnPause);
	}

	private void OnUnPause()
	{
		laserGuns.ForEach(x => x.Active = false);
		playerController.Freeze();
		started = false;
	}

	public void Menu()
	{
		SceneManager.LoadScene("MenuScene");
	}
}
