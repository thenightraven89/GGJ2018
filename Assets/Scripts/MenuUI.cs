using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;

public class MenuUI : MonoBehaviour
{
	[SerializeField]
	private GameObject[] players;

	public void ActivatePlayer(int index)
	{
		players[index].SetActive(true);
	}

	void Awake()
	{
		PlayerPrefs.DeleteAll();
	}

	void Update()
	{
		if (XCI.GetButtonDown(XboxButton.Start, XboxController.Any))
		{
			float playerCount = 0f;
			if (PlayerPrefs.GetInt("player1") > 0) playerCount++;
			if (PlayerPrefs.GetInt("player2") > 0) playerCount++;
			if (PlayerPrefs.GetInt("player3") > 0) playerCount++;
			if (PlayerPrefs.GetInt("player4") > 0) playerCount++;

			if (playerCount > 1)
			{
				SceneManager.LoadScene("Environment");
			}
		}

		if (XCI.GetButtonDown(XboxButton.A, XboxController.First))
		{
			PlayerPrefs.SetInt("player1", 1);
			ActivatePlayer(0);
		}

		if (XCI.GetButtonDown(XboxButton.A, XboxController.Second))
		{
			PlayerPrefs.SetInt("player2", 1);
			ActivatePlayer(1);
		}

		if (XCI.GetButtonDown(XboxButton.A, XboxController.Third))
		{
			PlayerPrefs.SetInt("player3", 1);
			ActivatePlayer(2);
		}

		if (XCI.GetButtonDown(XboxButton.A, XboxController.Fourth))
		{
			PlayerPrefs.SetInt("player4", 1);
			ActivatePlayer(3);
		}
	}
}