using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int score = PlayerPrefs.GetInt("score");
		GetComponent<Text>().text = score.ToString();
		StartCoroutine(ReloadGame());
	}

	IEnumerator ReloadGame()
	{
		yield return new WaitForSeconds(10f);
		SceneManager.LoadScene("Menu");
	}
}