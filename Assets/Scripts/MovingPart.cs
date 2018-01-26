using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
	private void Update()
	{
		transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
	}
}