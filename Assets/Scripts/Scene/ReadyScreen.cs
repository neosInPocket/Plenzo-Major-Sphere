using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using FingerTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ReadyScreen : MonoBehaviour
{
	[SerializeField] private GameObject textObject;
	[SerializeField] private Button pauseButton;
	private Action Ready;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void Show(Action ready)
	{
		pauseButton.interactable = false;
		Ready = ready;
		textObject.SetActive(true);
		FingerTouch.onFingerDown += OnReady;
	}

	private void OnReady(Finger finger)
	{
		FingerTouch.onFingerDown -= OnReady;
		Ready();
		pauseButton.interactable = true;
		if (textObject != null)
		{
			textObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		FingerTouch.onFingerDown -= OnReady;
	}
}
