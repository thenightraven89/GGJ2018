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

	bool isGunning;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		isGunning = false;
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

		if (!isGunning)
		{
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

			if (XCI.GetButtonDown(XboxButton.A, controller))
			{
				if (gun != null)
				{
					isGunning = true;
					transform.parent = gun.PlayerLocation;
					transform.localPosition = Vector3.zero;
					transform.localRotation = Quaternion.identity;
				}
			}
		}
		else
		{
			if (dir.magnitude == 1f)
			{
				gun.Turret.forward = dir;
			}

			if (XCI.GetButtonDown(XboxButton.A, controller))
			{
				gun.StartShoot();
			}

			if (XCI.GetButtonUp(XboxButton.A, controller))
			{
				gun.EndShoot();
			}

			if (XCI.GetButtonDown(XboxButton.B, controller))
			{
				gun = null;
				isGunning = false;
				transform.parent = null;
				transform.position = new Vector3(
					transform.position.x,
					0.5f,
					transform.position.z);
				transform.rotation = Quaternion.identity;
			}
		}




		//transform.Translate(Vector3.forward * dir.magnitude * Time.deltaTime * speed, Space.Self);
		//transform.Translate(dir * Time.deltaTime * speed);
	}

	Macaz macaz;
	Gun gun;


	void OnTriggerEnter(Collider other)
	{
		var m = other.GetComponent<Macaz>();
		if (m != null)
		{
			macaz = m;
			m.Highlight();
		}

		var g = other.GetComponent<Gun>();
		if (g != null)
		{
			gun = g;
			g.Highlight();
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

		var g = other.GetComponent<Gun>();
		if (g != null && g == gun)
		{
			gun = null;
			g.UnHighlight();
		}
	}
}