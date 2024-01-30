using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OptionsLoader : MonoBehaviour
{
	[SerializeField] private bool clearOptions;
	[SerializeField] private OptionsFile defaultOptions;
	private static string path => Application.persistentDataPath + "/Options.json";
	public static OptionsFile Options { get; private set; }

	private void Awake()
	{
		if (clearOptions)
		{
			ResetOptions();
		}
		else
		{
			GetSettings();
		}
	}

	public void ResetOptions()
	{
		Options = defaultOptions;
		SaveOptions();
	}

	public static void SaveOptions()
	{
		if (!File.Exists(path))
		{
			NewSaveFile();
		}
		else
		{
			WriteOptions();
		}
	}

	private static void GetSettings()
	{
		if (!File.Exists(path))
		{
			NewSaveFile();
		}
		else
		{
			string text = File.ReadAllText(path);
			Options = JsonUtility.FromJson<OptionsFile>(text);
		}
	}

	private static void NewSaveFile()
	{
		Options = new OptionsFile();
		File.WriteAllText(path, JsonUtility.ToJson(Options));
	}

	private static void WriteOptions()
	{
		File.WriteAllText(path, JsonUtility.ToJson(Options));
	}
}
