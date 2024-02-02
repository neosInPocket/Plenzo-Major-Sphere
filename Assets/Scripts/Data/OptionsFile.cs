using System;
using UnityEngine;

[Serializable]
public class OptionsFile
{
	public int coins = 5;
	public bool alreadyEntered = false;

	public int levelProgress = 1;
	public bool mainTrackEnabled = true;
	public bool effectsEnabled = true;
	public int healthUpgrade = 1;
	public int extraTimeUpgrade = 0;
}


