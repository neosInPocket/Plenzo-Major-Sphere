using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.UI;

public class TutorialManagment : MonoBehaviour
{
	[SerializeField] private TMP_Text characterText;
	[SerializeField] private GameObject upArrow;
	[SerializeField] private GameObject downArrow;
	[SerializeField] private Button pauseButton;
	public Action TutorialEnd { get; set; }
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void PlayEntry()
	{
		pauseButton.interactable = false;
		gameObject.SetActive(true);
		Touch.onFingerDown += Play;
		characterText.text = "WELCOME TO PLENZO MAJOR SPHERE!";
	}

	private void Play(Finger finger)
	{
		Touch.onFingerDown -= Play;
		Touch.onFingerDown += Play1;
		characterText.text = "THE GOAL OF THE GAME IS TO DODGE THE ROTATING LASERS";
	}

	private void Play1(Finger finger)
	{
		Touch.onFingerDown -= Play1;
		Touch.onFingerDown += Play2;
		characterText.text = "BY PRESSING THE BALL YOU JUMP UP";
		upArrow.SetActive(true);
	}

	private void Play2(Finger finger)
	{
		Touch.onFingerDown -= Play2;
		Touch.onFingerDown += Play3;
		upArrow.SetActive(false);
		downArrow.SetActive(true);
		characterText.text = "TAPING THE BALL IN THE AIR WILL RUSH IT DOWN";
	}

	private void Play3(Finger finger)
	{
		Touch.onFingerDown -= Play3;
		Touch.onFingerDown += Play4;
		downArrow.SetActive(false);
		characterText.text = "CAN YOU SUSTAIN THE LEVEL UNTIL THE TIMER ENDS? LET'S FIND OUT!";
	}

	private void Play4(Finger finger)
	{
		Touch.onFingerDown -= Play4;
		TutorialEnd?.Invoke();
		gameObject.SetActive(false);
		pauseButton.interactable = true;
	}
}
