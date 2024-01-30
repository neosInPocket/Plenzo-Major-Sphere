using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsRefresher : MonoBehaviour
{
	[SerializeField] private Image music;
	[SerializeField] private Image sfx;
	[SerializeField] private Color offColor;
	[SerializeField] private Color onColor;
	[SerializeField] private OptionsLoader optionsLoader;
	[SerializeField] private GameObject progressResetWindow;
	[SerializeField] private List<MoneyRefresher> moneyRefreshers;
	[SerializeField] private StoreContainer storeContainer;
	private TrackController currentManager;

	private void Start()
	{
		currentManager = GameObject.FindFirstObjectByType<TrackController>();
		bool musicEnabled = OptionsLoader.Options.mainTrackEnabled;
		bool sfxEnabled = OptionsLoader.Options.effectsEnabled;

		if (musicEnabled)
		{
			music.color = onColor;
		}
		else
		{
			music.color = offColor;
		}

		if (sfxEnabled)
		{
			sfx.color = onColor;
		}
		else
		{
			sfx.color = offColor;
		}
	}

	public void ToggleMusic()
	{
		bool enabled = currentManager.Toggle();
		if (enabled)
		{
			music.color = onColor;
		}
		else
		{
			music.color = offColor;
		}
	}

	public void ToggleSFX()
	{
		if (sfx.color == onColor)
		{
			sfx.color = offColor;
			OptionsLoader.Options.effectsEnabled = false;
		}
		else
		{
			sfx.color = onColor;
			OptionsLoader.Options.effectsEnabled = true;
		}

		OptionsLoader.SaveOptions();
	}

	public void ResetProgress()
	{
		optionsLoader.ResetOptions();
		storeContainer.RefreshAll();
		moneyRefreshers.ForEach(x => x.Refresh());
		ShowResetWindow(false);
	}

	public void ShowResetWindow(bool value)
	{
		progressResetWindow.SetActive(value);
	}
}
