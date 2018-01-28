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

	private Tunnel[] tunnels;

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

		tunnels = FindObjectsOfType(typeof(Tunnel)) as Tunnel[];

		StartCoroutine(LaunchTrain());
	}

	private float trainTimeGap = 10f;

	private IEnumerator LaunchTrain()
	{
		yield return new WaitForSeconds(3f);

		while (true)
		{

			Color color = colors[Random.Range(0, colors.Length)];

			int fromTunnel = Random.Range(0, tunnels.Length);
			int toTunnel = Random.Range(0, tunnels.Length);

			while (tunnels[toTunnel].IsBusy())
			{
				yield return null;
				toTunnel = Random.Range(0, tunnels.Length);
			}

			tunnels[toTunnel].SetColor(color);

			while (tunnels[fromTunnel].IsBusy())
			{
				yield return null;
				fromTunnel = Random.Range(0, tunnels.Length);
			}

			tunnels[fromTunnel].SpawnTrain(Random.Range(1, 3), color);

			yield return new WaitForSeconds(trainTimeGap);

			trainTimeGap = Mathf.Clamp(trainTimeGap - 0.25f, 4f, 10f);
		}
	}

	[SerializeField]
	private Color[] colors;
}

public class Objective
{
	public Tunnel fromTunnel;
	public Tunnel toTunnel;
	public MovingPart[] cargo;
}