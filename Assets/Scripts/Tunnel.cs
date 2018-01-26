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
	private float spawnTime;

	private void Awake()
	{
		SpawnTrain(4);
	}

	public void SpawnTrain(int wagonCount)
	{
		StartCoroutine(SpawnTrainCoroutine(wagonCount));
	}

	public void SetColor(Color color)
	{

	}

	private IEnumerator SpawnTrainCoroutine(int wagonCount)
	{
		var newLocomotive = Instantiate(locomotive, transform.position, transform.rotation);

		for (int i = 0; i < wagonCount; i++)
		{
			yield return new WaitForSeconds(spawnTime);
			Instantiate(wagon, transform.position, transform.rotation);
		}
	}
}