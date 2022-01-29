using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Speechbubble : MonoBehaviour
{
	public float delay = 0.1f;
	public string fullText;
	private string currentText = "";

	[SerializeField] private TMP_Text textbox;
	[SerializeField] private Image boximage;
	[SerializeField] private GameObject fullBox;

	// Use this for initialization
	void Awake()
	{
		fullBox.SetActive(false);
	}

	[ContextMenu("TestOpen")]
	void TestOpen()
	{
		Open("Test");
	}

	IEnumerator ShowText()
	{
		for (int i = 0; i <= fullText.Length; i++)
		{
			currentText = fullText.Substring(0, i);
			textbox.text = currentText;
			yield return new WaitForSeconds(delay);
		}
	}

	
	void Open(string text)
	{
		fullText = text;
		fullBox.SetActive(true);
		fullBox.transform.DOShakeScale(.5f, 2);
		StartCoroutine(ShowText());
	}

	[ContextMenu("Close")]
	void Close()
	{
		fullBox.SetActive(false);
		
		StopCoroutine(ShowText());
	}
}
