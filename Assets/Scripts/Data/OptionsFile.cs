using System;
using UnityEngine;

[Serializable]
public class OptionsFile : MonoBehaviour
{
	public int coins = 10;
	public bool alreadyEntered = true;

	public int levelProgress = 1;
	public bool mainTrackEnabled = true;
	public bool effectsEnabled = true;
}


