using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Random = UnityEngine.Random;
using System.Collections;
using System.Linq;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Vector3 spawnPosition;
	[SerializeField] private Rigidbody rigid;
	[SerializeField] private float force;
	[SerializeField] private Vector2 angular;
	[SerializeField] private GameObject explosionEffect;
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private AudioSource hopSource;
	private bool ground = true;
	public Action<int> Damage { get; set; }
	private RigidbodyConstraints rigidbodyConstraints;
	private Vector3 angularSpeed;
	private Vector3 currentSpeed;

	public bool Enabled
	{
		get => active;
		set
		{
			if (active == value) return;

			active = value;
			if (value)
			{
				Touch.onFingerDown += OnPlayerClick;
			}
			else
			{
				Touch.onFingerDown -= OnPlayerClick;
			}
		}
	}

	private bool active;
	private int lifesLeft;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		rigidbodyConstraints = rigid.constraints;
		transform.position = spawnPosition;
		lifesLeft = OptionsLoader.Options.healthUpgrade;
	}

	private void OnPlayerClick(Finger finger)
	{
		RaycastHit[] raycast = Physics.RaycastAll(Camera.main.ScreenPointToRay(finger.screenPosition));
		var ball = raycast.FirstOrDefault(x => x.collider.GetComponent<PlayerController>() != null);
		if (ball.collider != null)
		{
			PlayerClickHandler();
		}
	}

	private void PlayerClickHandler()
	{
		if (hopSource != null)
		{
			hopSource.Stop();
			hopSource.Play();
		}

		if (ground)
		{
			if (rigid != null)
			{
				rigid.velocity = Vector3.zero;
				rigid.angularVelocity = new Vector3(Random.Range(angular.x, angular.y), 0, 0);
				rigid.AddForce(Vector3.up * force, ForceMode.Impulse);
			}

			ground = false;
		}
		else
		{
			if (rigid != null)
			{
				rigid.velocity = Vector3.zero;
				rigid.AddForce(Vector3.down * force, ForceMode.Impulse);
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		ground = true;
	}

	private void OnTriggerEnter(Collider collider)
	{
		lifesLeft--;
		if (lifesLeft <= 0)
		{
			lifesLeft = 0;
			rigid.constraints = RigidbodyConstraints.FreezeAll;
			StartCoroutine(Explosion());
		}

		Damage?.Invoke(lifesLeft);
	}

	private IEnumerator Explosion()
	{
		explosionEffect.SetActive(true);
		meshRenderer.enabled = false;
		yield return new WaitForSeconds(1f);
		explosionEffect.SetActive(false);
	}

	public void Freeze()
	{
		rigid.constraints = RigidbodyConstraints.FreezeAll;
		currentSpeed = rigid.velocity;
		angularSpeed = rigid.angularVelocity;

		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;
	}

	public void UnFreeze()
	{
		rigid.constraints = rigidbodyConstraints;
		rigid.velocity = currentSpeed;
		rigid.angularVelocity = angularSpeed;
	}


	private void OnDestroy()
	{
		Touch.onFingerDown -= OnPlayerClick;
	}

	public static Vector2 ScreenSize()
	{
		return ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
	}

	public static Vector3 ScreenToWorldPoint(Vector2 input)
	{
		var objective = Camera.main.ScreenPointToRay(input);

		var direction = objective.direction;
		var origin = objective.origin;

		Vector3 normal = new Vector3(0, 0, 1);
		Vector3 point = new Vector3(0, 0, 0);

		float product = Vector3.Dot(direction, normal);

		float magnitude = Vector3.Dot(point - origin, normal) / product;

		Vector3 result = origin + magnitude * direction;
		return result;
	}
}
