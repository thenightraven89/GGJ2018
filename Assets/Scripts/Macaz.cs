using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macaz : MonoBehaviour
{
	[SerializeField]
	private SwitchType switchType;

	public int TypeIndex
	{
		get
		{
			return (int)switchType;
		}
	}
}

public enum SwitchType
{
	NW = 0,
	NE = 1,
	SE = 2,
	SW = 3,
	NS = 4,
	WE = 5
}