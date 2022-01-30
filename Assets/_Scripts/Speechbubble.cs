using _Scripts;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Speechbubble : MonoBehaviour
{
	public float delay = 0.1f;
	public float delayDecreaseMultiplier = 0.25f;
	public string fullText;
	private string currentText = "";

	[SerializeField] private TMP_Text textbox;
	[SerializeField] private Image boximage;
	[SerializeField] private GameObject fullBox;

	private IEnumerator _speechCoroutine;

	private bool _isTextFinishedDisplaying;
	private bool _isDelayDecreaseActive;

	private Action _lastCallback;
	private Inputs _inputs;
	
	// Use this for initialization
	void Awake()
	{
		fullBox.SetActive(false);

		_inputs = new Inputs();
		_inputs.Enable();

		_inputs.Player.Jump.performed += HandleJump;
	}

	private void OnDisable()
	{
		_inputs.Player.Jump.performed -= HandleJump;
	}

	private void HandleJump(InputAction.CallbackContext _)
	{
		if (_isTextFinishedDisplaying)
		{
			Close();
		}
		else
		{
			_isDelayDecreaseActive = true;	
		}
	}

	[ContextMenu("TestOpen")]
	void TestOpen()
	{
		Open("Test", () => {});
	}

	IEnumerator ShowText()
	{
		for (int i = 0; i <= fullText.Length; i++)
		{
			currentText = fullText.Substring(0, i);
			textbox.text = currentText;
			//AudioManager.Instance.PlayAudioClip(AudioId.Voice);
			var nextDelay = delay * (_isDelayDecreaseActive ? delayDecreaseMultiplier : 1f);
			yield return new WaitForSeconds(nextDelay);
		}

		_isDelayDecreaseActive = false;
		_isTextFinishedDisplaying = true;
	}

	
	public void Open(string text, Action callback)
	{
		_isTextFinishedDisplaying = false;
		_lastCallback = callback;
		
		fullText = text;
		fullBox.SetActive(true);
		fullBox.transform.DOShakeScale(.5f, 2);
		
		if (_speechCoroutine != null)
		{
			StopCoroutine(_speechCoroutine);
		}
            
		_speechCoroutine = ShowText();
		StartCoroutine(_speechCoroutine);
	}

	[ContextMenu("Close")]
	public void Close()
	{
		fullBox.SetActive(false);
		StopCoroutine(_speechCoroutine);
		_lastCallback?.Invoke();
	}
}
