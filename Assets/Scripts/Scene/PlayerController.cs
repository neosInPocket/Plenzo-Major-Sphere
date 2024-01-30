using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Vector3 spawnPosition;
	[SerializeField] private Animator animator;
	[SerializeField] private Rigidbody rigid;
	[SerializeField] private float force;
	[SerializeField] private Vector2 angular;
	private bool ground = true;

	public bool Enabled
	{
		get => active;
		set
		{
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

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		transform.position = spawnPosition;
	}

	private void OnPlayerClick(Finger finger)
	{
		if (ground)
		{
			animator.SetTrigger("jumpUp");
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = new Vector3(Random.Range(angular.x, angular.y), 0, 0);
			rigid.AddForce(Vector3.up * force, ForceMode.Impulse);
			ground = false;
		}
		else
		{
			rigid.velocity = Vector3.zero;
			rigid.AddForce(Vector3.down * force, ForceMode.Impulse);
			animator.SetTrigger("jumpDown");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		animator.SetTrigger("floor");
		ground = true;
	}

	private void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Dead");
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnPlayerClick;
	}
}
