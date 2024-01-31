using UnityEngine;

public class LoadSFX : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Start()
	{
		audioSource.enabled = OptionsLoader.Options.effectsEnabled;
	}
}
