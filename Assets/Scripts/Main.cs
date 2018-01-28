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

	public static Main Instance {get; private set;}

	void Awake()
	{
		Instance = this;

		if (PlayerPrefs.GetInt("player1") > 0)
		{
			var newChar = Instantiate(
				charPrefab,
				spawnPoints[0].position,
				Quaternion.identity);

			newChar.GetComponent<CharacterInput>().Initialize(XboxController.First, colors[0]);
		}

		if (PlayerPrefs.GetInt("player2") > 0)
		{
			var newChar = Instantiate(
				charPrefab,
				spawnPoints[1].position,
				Quaternion.identity);

				
			newChar.GetComponent<CharacterInput>().Initialize(XboxController.Second, colors[1]);
		}

		if (PlayerPrefs.GetInt("player3") > 0)
		{
			var newChar = Instantiate(
				charPrefab,
				spawnPoints[2].position,
				Quaternion.identity);

			newChar.GetComponent<CharacterInput>().Initialize(XboxController.Third, colors[2]);
		}

		if (PlayerPrefs.GetInt("player4") > 0)
		{
			var newChar = Instantiate(
				charPrefab,
				spawnPoints[3].position,
				Quaternion.identity);
				
			newChar.GetComponent<CharacterInput>().Initialize(XboxController.Fourth, colors[3]);
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

			tunnels[toTunnel].SetColor(color, 30f);

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

	public void GenerateExplosionPS(Vector3 position, int count)
	{
		explodePS.transform.position = position;
		explodePS.Emit(count);
	}

	[SerializeField]
	private ParticleSystem explodePS;

	[SerializeField]
	private Color[] colors;
}