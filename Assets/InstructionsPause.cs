using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsPause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Pause());
	}
	
	IEnumerator Pause()
	{
		yield return new WaitForSeconds(10f);
		SceneManager.LoadScene(PlayerPrefs.GetString("level"));
	}
}
