using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeContainer : MonoBehaviour
{
	[SerializeField] private GameObject status;
	[SerializeField] private Button button;
	[SerializeField] private TMP_Text priceText;
	[SerializeField] private int price;
	[SerializeField] private bool healthUpgrade;
	[SerializeField] private List<Image> points;
	public int Price => price;

	private void Start()
	{
		priceText.text = price.ToString();
	}

	public void Refresh()
	{
		int pointer = 0;
		bool buttonInteractable = false;
		bool upgraded = false;

		if (healthUpgrade)
		{
			pointer = OptionsLoader.Options.healthUpgrade;
			buttonInteractable = OptionsLoader.Options.healthUpgrade < 3 && OptionsLoader.Options.coins >= price;
			upgraded = OptionsLoader.Options.healthUpgrade >= 3;
		}
		else
		{
			pointer = OptionsLoader.Options.secondUpgrade;
			buttonInteractable = OptionsLoader.Options.secondUpgrade < 3 && OptionsLoader.Options.coins >= price;
			upgraded = OptionsLoader.Options.secondUpgrade >= 3;
		}

		button.interactable = buttonInteractable;
		status.SetActive(!buttonInteractable);

		if (upgraded)
		{
			status.SetActive(false);
		}
		points.ForEach(x => x.enabled = false);
		for (int i = 0; i < pointer; i++)
		{
			points[i].enabled = true;
		}
	}
}
