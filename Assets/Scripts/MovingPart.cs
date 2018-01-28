using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
	Vector3Int currentPos;
	Vector3Int newPos;
	TerrainGrid grid;

	[SerializeField]
	Transform coloredPart;

	private float speed = 2f;

	public void Initialize(Color color)
	{
		grid = FindObjectOfType<TerrainGrid>();
		currentPos = Vector3Int.RoundToInt(transform.position);
		var dir = Vector3Int.RoundToInt(transform.forward);
		var targetDir = grid.GetDirectionFor(currentPos, dir);
		//Debug.Log(targetDir);
		if (coloredPart != null)
		coloredPart.GetComponent<MeshRenderer>().materials[1].color = color;

		StartCoroutine(Move(currentPos, dir, targetDir));
	}

	public Color GetColor()
	{
		if (coloredPart != null)
		return coloredPart.GetComponent<MeshRenderer>().materials[1].color;
		else return Color.white;
	}

	private IEnumerator Move(
		Vector3Int fromPos,
		Vector3Int fromDir,
		Vector3Int toDir)
	{
		//Debug.LogFormat("moving from {0} to {1}", fromPos, fromPos + toDir);

		currentPos = fromPos;

		float t = 0f;
		while (t < 1f)
		{
			transform.position = Vector3.Lerp(fromPos, fromPos + toDir, t);
			transform.forward = Vector3.Lerp(fromDir, toDir, t);
			t += Time.deltaTime * speed;
			yield return null;
		}

		transform.position = fromPos + toDir;
		transform.forward = toDir;
		var targetDir = grid.GetDirectionFor(fromPos + toDir, toDir);

		yield return StartCoroutine(Move(fromPos + toDir, toDir, targetDir ));
	}

	void OnTriggerEnter(Collider other)
	{
		// var mp = other.GetComponent<MovingPart>();
		// if (mp != null)
		// {
		// 	mp.Explode();
		// 	Explode();
		// }
	}

	public void Explode()
	{
		Main.Instance.GenerateExplosionPS(transform.position, 50);
		Destroy(this);
	}
}