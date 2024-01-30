using System.Collections.Generic;
using UnityEngine;

public class StoreContainer : MonoBehaviour
{
	[SerializeField] private UpgradeContainer healthContainer;
	[SerializeField] private UpgradeContainer secondContainer;
	[SerializeField] private List<MoneyRefresher> moneyRefreshers;

	private void Start()
	{
		RefreshAll();
	}

	public void UpgradeHealth()
	{
		OptionsLoader.Options.coins -= healthContainer.Price;
		OptionsLoader.Options.healthUpgrade++;
		OptionsLoader.SaveOptions();
		RefreshAll();
	}

	public void UpgradeSecond()
	{
		OptionsLoader.Options.coins -= secondContainer.Price;
		OptionsLoader.Options.secondUpgrade++;
		OptionsLoader.SaveOptions();
		RefreshAll();
	}

	public void RefreshAll()
	{
		secondContainer.Refresh();
		healthContainer.Refresh();
		moneyRefreshers.ForEach(x => x.Refresh());
	}
}
