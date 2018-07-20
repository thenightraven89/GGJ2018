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

	private float currentSpeed;
	private float targetSpeed;

	private float accel = 1f;

	public void SetSpeed(float target)
	{
		targetSpeed = target;
	}

	public void Initialize(Color color)
	{
		grid = FindObjectOfType<TerrainGrid>();
		currentPos = Vector3Int.RoundToInt(transform.position);
		var dir = Vector3Int.RoundToInt(transform.forward);
		var targetDir = grid.GetDirectionFor(currentPos, dir);
		//Debug.Log(targetDir);
		if (coloredPart != null)
		coloredPart.GetComponent<MeshRenderer>().materials[1].color = color;

		currentSpeed = 2f;
		targetSpeed = 2f;
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
			currentSpeed = Mathf.Clamp(
				currentSpeed + Mathf.Sign(targetSpeed - currentSpeed) * accel * Time.deltaTime,
				0,
				Mathf.Max(currentSpeed, targetSpeed));

			t += Time.deltaTime * currentSpeed;
			yield return null;
		}

		transform.position = fromPos + toDir;
		transform.forward = toDir;
		var targetDir = grid.GetDirectionFor(fromPos + toDir, toDir);
		if (targetDir == Vector3Int.zero)
		{
			Explode(false);
		}
		else
		{
		yield return StartCoroutine(Move(fromPos + toDir, toDir, targetDir));
		}
	}

	void OnTriggerEnter(Collider other)
	{
		var mp = other.GetComponent<MovingPart>();
		if (mp != null)
		{
			mp.Explode(false);
			Explode(false);
		}

		var c = other.GetComponent<Cliff>();
		if (c != null)
		{
			Explode(c.isSilent);
		}
	}


	public bool isInvincible;

	public void Explode(bool isSilent)
	{
		if (!isInvincible)
		{
			if (!isSilent)
			{
				Main.Instance.GenerateExplosionPS(transform.position, 50);
			}

			Destroy(gameObject);
		}
	}
}