using UnityEngine;

[CreateAssetMenu(menuName = "DefaultSettings")]
public class DefaultSettings : ScriptableObject
{
	[SerializeField] private OptionsFile optionsFile;
	public OptionsFile Default => optionsFile;
}
