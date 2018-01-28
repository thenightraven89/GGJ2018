using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
	[SerializeField]
	private GameObject locomotive;

	[SerializeField]
	private GameObject wagon;

	[SerializeField]
	private GameObject gun;

	[SerializeField]
	private Renderer tunnelRenderer;

	private float spawnTime = .6f;

	void Awake()
	{
		tunnelRenderer.material.color = Color.white;
	}

	public bool IsBusy()
	{
		return tunnelRenderer.material.color != Color.white;
	}

	public void SetColor(Color color, float availableTime)
	{
		tunnelRenderer.material.color = color;
		StartCoroutine(ResetColor(availableTime));
	}

	IEnumerator ResetColor(float availableTime)
	{
		yield return new WaitForSeconds(availableTime);
		tunnelRenderer.material.color = Color.white;
	}

	public void SpawnTrain(int wagonCount, Color color)
	{
		StartCoroutine(SpawnTrainCoroutine(wagonCount, color));
	}

	private IEnumerator SpawnTrainCoroutine(int wagonCount, Color color)
	{
		var newLocomotive = Instantiate(locomotive, transform.position, transform.rotation);
		newLocomotive.GetComponent<MovingPart>().Initialize(color);

		for (int i = 0; i < wagonCount; i++)
		{
			yield return new WaitForSeconds(spawnTime);
			var newWagon = Instantiate(
				wagon,
				transform.position,
				transform.rotation);

			newWagon.GetComponent<MovingPart>().Initialize(color);
		}

		yield return new WaitForSeconds(spawnTime);
		
		var newGun = Instantiate(gun, transform.position, transform.rotation);
		newGun.GetComponent<MovingPart>().Initialize(color);
	}

	void OnTriggerEnter(Collider other)
	{
		var mp = other.GetComponent<MovingPart>();
		if (mp != null && Vector3Int.RoundToInt(other.transform.forward) == Vector3Int.RoundToInt(-transform.forward))
		{
			if (mp.GetColor() == tunnelRenderer.material.color)
			{
				Score.Instance.Add();
				// earn points
				// play a nice sfx
			}
			else
			{
				Debug.Log("PENALTY");
				// lose points
				// play a nasty sfx
				// train explodes?
			}
		}
	}
}