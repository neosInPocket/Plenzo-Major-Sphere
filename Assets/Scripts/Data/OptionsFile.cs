using System;
using UnityEngine;

[Serializable]
public class OptionsFile
{
	public int coins = 100;
	public bool alreadyEntered = true;

	public int levelProgress = 1;
	public bool mainTrackEnabled = true;
	public bool effectsEnabled = true;
	public int healthUpgrade = 1;
	public int secondUpgrade = 0;
}


