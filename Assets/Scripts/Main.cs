using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Main : MonoBehaviour
{
	[SerializeField]
	private GameObject charPrefab;

	[SerializeField]
	private Transform[] spawnPoints;

	void Awake()
	{
			if (PlayerPrefs.GetInt("player1") > 0)
			{
				var newChar = Instantiate(
					charPrefab,
					spawnPoints[0].position,
					Quaternion.identity);

				newChar.GetComponent<CharacterInput>().Initialize(XboxController.First);
			}

			if (PlayerPrefs.GetInt("player2") > 0)
			{
				var newChar = Instantiate(
					charPrefab,
					spawnPoints[1].position,
					Quaternion.identity);

					
				newChar.GetComponent<CharacterInput>().Initialize(XboxController.Second);
			}

			if (PlayerPrefs.GetInt("player3") > 0)
			{
				var newChar = Instantiate(
					charPrefab,
					spawnPoints[2].position,
					Quaternion.identity);

				newChar.GetComponent<CharacterInput>().Initialize(XboxController.Third);
			}

			if (PlayerPrefs.GetInt("player4") > 0)
			{
				var newChar = Instantiate(
					charPrefab,
					spawnPoints[3].position,
					Quaternion.identity);
					
				newChar.GetComponent<CharacterInput>().Initialize(XboxController.Fourth);
			}
	}
}