using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
	Vector3Int currentPos;
	Vector3Int newPos;
	TerrainGrid grid;

	private float speed = 2f;

	public void Initialize()
	{
		grid = FindObjectOfType<TerrainGrid>();
		currentPos = Vector3Int.FloorToInt(transform.position);
		var dir = Vector3Int.FloorToInt(transform.forward);
		var targetDir = grid.GetDirectionFor(currentPos, dir);
		//Debug.Log(targetDir);
		StartCoroutine(Move(currentPos, dir, targetDir));
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

	private void Update()
	{
		// newPos = Vector3Int.FloorToInt(transform.position);
		// if (currentPos != newPos)
		// {
		// 	currentPos = newPos;
		// 	var dir = Vector3Int.FloorToInt(transform.forward);
		// 	var targetDir = grid.GetDirectionFor(currentPos, dir);
		// 	Debug.Log(targetDir);
		// 	StartCoroutine(Move(currentPos, dir, targetDir));
		// }
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(currentPos, currentPos + Vector3.right);
		Gizmos.DrawLine(currentPos, currentPos + Vector3.back);
		Gizmos.DrawLine(currentPos + Vector3.right, currentPos + Vector3.right + Vector3.back);
		Gizmos.DrawLine(currentPos + Vector3.back, currentPos + Vector3.right + Vector3.back);
	}
}