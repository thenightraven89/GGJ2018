using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

	Text text;
	int seconds = 60 * 4;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		StartCoroutine(CountDown());
		
	}

	IEnumerator CountDown()
	{
		while (seconds > -1)
		{
			seconds--;
			text.text = (seconds / 60).ToString("D2") + ":" + (seconds % 60).ToString("D2");
			yield return new WaitForSeconds(1f);
		}
		PlayerPrefs.SetInt("score", Score.Instance.GetScore());
		SceneManager.LoadScene("GameOver");
	}
}
