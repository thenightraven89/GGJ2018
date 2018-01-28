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

	private float spawnTime = .6f;

	private void Start()
	{
		SpawnTrain(5);
	}

	public void SpawnTrain(int wagonCount)
	{
		StartCoroutine(SpawnTrainCoroutine(wagonCount));
	}

	private IEnumerator SpawnTrainCoroutine(int wagonCount)
	{
		var newLocomotive = Instantiate(locomotive, transform.position, transform.rotation);
		newLocomotive.GetComponent<MovingPart>().Initialize();

		for (int i = 0; i < wagonCount; i++)
		{
			yield return new WaitForSeconds(spawnTime);
			var newWagon = Instantiate(
				wagon,
				transform.position,
				transform.rotation);

			newWagon.GetComponent<MovingPart>().Initialize();
		}

		yield return new WaitForSeconds(spawnTime);
		
		var newGun = Instantiate(gun, transform.position, transform.rotation);
		newGun.GetComponent<MovingPart>().Initialize();
	}
}