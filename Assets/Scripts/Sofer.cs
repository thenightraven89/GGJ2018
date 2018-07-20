using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sofer : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Halter"))
		{
			var mp = GetComponent<MovingPart>();
			Main.Instance.HaltAllTrainsOfColor(mp.GetColor());
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Halter"))
		{
			var mp = GetComponent<MovingPart>();
			Main.Instance.ResumeAllTrainsOfColor(mp.GetColor());
		}
	}
}
