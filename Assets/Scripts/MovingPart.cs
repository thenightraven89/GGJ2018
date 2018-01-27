using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
	int xPos, zPos;

	public void Initialize()
	{
		xPos = (int)Mathf.Floor(transform.position.x);
		zPos = (int)Mathf.Floor(transform.position.z);
	}

	private void Update()
	{
		transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
	}
}