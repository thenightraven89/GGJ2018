using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCart : MonoBehaviour
{
	public Tunnel destination;

	void OnDestroy()
	{
		destination.ResetColor();
	}
}
