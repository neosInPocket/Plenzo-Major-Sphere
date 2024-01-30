using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class MoneyRefresher : MonoBehaviour
{
	[SerializeField] private TMP_Text count;

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		count.text = OptionsLoader.Options.coins.ToString();
	}
}
