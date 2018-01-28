using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XboxCtrlrInput;

public class CharacterInput : MonoBehaviour
{
	[SerializeField]
	XboxController controller;

	NavMeshAgent agent;

	float xAxis, yAxis;

	float speed = 5f;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

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
		agent.Move(transform.forward * dir.magnitude * Time.deltaTime * speed);

		if (XCI.GetButtonDown(XboxButton.A, controller))
		{
			if (macaz != null)
			{
				macaz.SwitchSwitchType();
			}
		}
		//transform.Translate(Vector3.forward * dir.magnitude * Time.deltaTime * speed, Space.Self);
		//transform.Translate(dir * Time.deltaTime * speed);
	}

	Macaz macaz;

	void OnTriggerEnter(Collider other)
	{
		var m = other.GetComponent<Macaz>();
		if (m != null)
		{
			macaz = m;
			m.Highlight();
		}
	}

	void OnTriggerExit(Collider other)
	{
		var m = other.GetComponent<Macaz>();
		if (m != null && m == macaz)
		{
			macaz = null;
			m.UnHighlight();
		}
	}
}