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
		SetColor(Color.white);
	}

	public bool IsBusy()
	{
		return tunnelRenderer.material.color != Color.white;
	}

	public void SetColor(Color color)
	{
		tunnelRenderer.material.color = color;
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
}