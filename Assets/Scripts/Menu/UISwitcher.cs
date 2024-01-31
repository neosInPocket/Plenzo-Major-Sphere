using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISwitcher : MonoBehaviour
{
	[SerializeField] private GameObject[] panels;
	[SerializeField] private float speed;
	private int screenHeight => Screen.height;

	public void Switch(int direction)
	{
		StopAllCoroutines();
		StartCoroutine(SwitchTransition(direction));
	}

	private IEnumerator SwitchTransition(int destination)
	{
		var goal = Destination(destination);
		var magnitude = Mathf.Abs(goal - transform.position.y) / screenHeight;
		var direction = (goal - transform.position.y) / magnitude;
		var currentPosition = transform.position;

		while (magnitude > 0)
		{
			currentPosition.y += direction * speed * magnitude * Time.deltaTime;
			transform.position = currentPosition;
			magnitude = Mathf.Abs(goal - transform.position.y) / screenHeight;
			yield return null;
		}

		currentPosition.y = Destination(destination);
		transform.position = currentPosition;
	}

	private int Destination(int direction)
	{
		return screenHeight * direction - screenHeight / 2;
	}

	public void LoadGameLevel()
	{
		SceneManager.LoadScene("Game");
	}
}
