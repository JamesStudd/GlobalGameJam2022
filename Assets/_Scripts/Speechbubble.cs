using _Scripts;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
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
	
	// Use this for initialization
	void Awake()
	{
		fullBox.SetActive(false);

		Inputs inputs = new Inputs();
		inputs.Enable();

		inputs.Player.Attack.performed += _ =>
		{
			if (_isTextFinishedDisplaying)
			{
				Close();
			}
			else
			{
				_isDelayDecreaseActive = true;	
			}
		};
	}

	[ContextMenu("TestOpen")]
	void TestOpen()
	{
		Open("Test", () => {});
	}

	IEnumerator ShowText(Action callback)
	{
		for (int i = 0; i <= fullText.Length; i++)
		{
			currentText = fullText.Substring(0, i);
			textbox.text = currentText;
			var nextDelay = delay * (_isDelayDecreaseActive ? delayDecreaseMultiplier : 1f);
			yield return new WaitForSeconds(nextDelay);
		}

		_isDelayDecreaseActive = false;
		_isTextFinishedDisplaying = true;
		
		callback?.Invoke();
	}

	
	public void Open(string text, Action callback)
	{
		_isTextFinishedDisplaying = false;
		
		fullText = text;
		fullBox.SetActive(true);
		fullBox.transform.DOShakeScale(.5f, 2);
		
		if (_speechCoroutine != null)
		{
			StopCoroutine(_speechCoroutine);
		}
            
		_speechCoroutine = ShowText(callback);
		StartCoroutine(_speechCoroutine);
	}

	[ContextMenu("Close")]
	public void Close()
	{
		fullBox.SetActive(false);
		StopCoroutine(_speechCoroutine);
	}
}
