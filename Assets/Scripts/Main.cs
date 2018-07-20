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

	private float trainTimeGap = 10f;

	private List<Color> activeColors;

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

		activeColors = new List<Color>();

		StartCoroutine(LaunchTrain());
	}

	public void RemoveActiveColor(Color color)
	{
		if (activeColors.Contains(color))
		{
			activeColors.Remove(color);
		}
		else
		{
			Debug.LogFormat("{0} not present in activeColors", color);
		}
	}

	private IEnumerator LaunchTrain()
	{
		yield return new WaitForSeconds(3f);

		while (true)
		{
			int busyCount = 0;
			foreach (var tunnel in tunnels)
			{
				if (tunnel.IsBusy()) busyCount++;
			}

			if (busyCount < tunnels.Length - 2)
			{
				Color color = colors[Random.Range(0, colors.Length)];
				while (activeColors.Contains(color))
				{
					color = colors[Random.Range(0, colors.Length)];
				}

				activeColors.Add(color);

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

				tunnels[fromTunnel].SpawnTrain(Random.Range(2, 5), color, tunnels[toTunnel]);

				yield return new WaitForSeconds(trainTimeGap);

				trainTimeGap = Mathf.Clamp(trainTimeGap - 0.25f, 4f, 10f);
			}

			yield return null;
		}
	}

	public void GenerateExplosionPS(Vector3 position, int count)
	{
		explodePS.transform.position = position;
		explodePS.Emit(count);
		var src = Camera.main.GetComponent<AudioSource>();
		src.pitch = Random.Range(0.8f, 1.2f);
		src.PlayOneShot(crashFX);
	}

	public void HaltAllTrainsOfColor(Color color)
	{
		var mps = FindObjectsOfType(typeof(MovingPart)) as MovingPart[];
		foreach (var mp in mps)
		{
			if (mp.GetColor() == color)
			mp.SetSpeed(0f);
		}
	}

	public void ResumeAllTrainsOfColor(Color color)
	{
		var mps = FindObjectsOfType(typeof(MovingPart)) as MovingPart[];
		foreach (var mp in mps)
		{
			if (mp.GetColor() == color)
			mp.SetSpeed(2f);
		}
	}

	[SerializeField]
	private AudioClip crashFX;

	[SerializeField]
	private ParticleSystem explodePS;

	[SerializeField]
	private Color[] colors;
}