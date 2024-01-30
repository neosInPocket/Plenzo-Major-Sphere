using System.Collections.Generic;
using UnityEngine;

public class SceneManagment : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
	[SerializeField] private List<LaserGun> laserGuns;

	private void Start()
	{
		playerController.Enabled = true;
		EnableGuns();
	}

	private void EnableGuns()
	{
		laserGuns.ForEach(x => x.Active = true);
	}
}
