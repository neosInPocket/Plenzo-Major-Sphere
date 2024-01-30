using System.Collections;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
	[SerializeField] private Vector2 rotationSpeeds;
	[SerializeField] private Vector2 directionChangeDelay;
	private float speed;
	public bool Active { get; set; }
	private bool changingDirection;

	private void Start()
	{
		speed = Random.Range(rotationSpeeds.x, rotationSpeeds.y);
		if (Random.Range(0, 2) == 0)
		{
			speed *= -1;
		}
	}

	private void Update()
	{
		if (!Active) return;

		var eulerAngles = transform.eulerAngles;
		eulerAngles.y += speed * Time.deltaTime;
		transform.eulerAngles = eulerAngles;

		if (changingDirection) return;
		changingDirection = true;

		StartCoroutine(ChangeDirection());
	}

	private IEnumerator ChangeDirection()
	{
		yield return new WaitForSeconds(Random.Range(directionChangeDelay.x, directionChangeDelay.y));
		speed *= -1;
		changingDirection = false;
	}
}
