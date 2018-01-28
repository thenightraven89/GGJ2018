using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	int score = 0;
	Text text;

	public static Score Instance {get; private set;}

	void Awake()
	{
		Instance = this;
		text = GetComponent<Text>();
	}

	public void Add()
	{
		score++;
		text.text = score.ToString();
	}

	public int GetScore()
	{
		return score;
	}
}
