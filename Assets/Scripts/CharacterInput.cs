using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CharacterInput : MonoBehaviour
{
	[SerializeField]
	XboxController controller;

	float xAxis, yAxis;

	float speed = 5f;

	public void Initialize(XboxController c)
	{
		controller = c;
	}

	void Update()
	{
		xAxis = XCI.GetAxis(XboxAxis.LeftStickX, controller);
		yAxis = XCI.GetAxis(XboxAxis.LeftStickY, controller);

		Vector3 dir = new Vector3(xAxis, 0f, yAxis).normalized;
		if (dir.magnitude == 1f)
		{
			transform.forward = dir;
		}

		transform.Translate(Vector3.forward * dir.magnitude * Time.deltaTime * speed, Space.Self);
		//transform.Translate(dir * Time.deltaTime * speed);
	}
}