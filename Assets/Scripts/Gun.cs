using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform PlayerLocation;

    public Transform Turret;

    public ParticleSystem PS;

    public void Highlight()
	{
        var rs = GetComponentsInChildren<Renderer>();
        foreach (var r in rs)
		r.material.color = Color.green;
	}

	public void UnHighlight()
	{
        var rs = GetComponentsInChildren<Renderer>();
        foreach (var r in rs)
		r.material.color = Color.white;
	}

    public void StartShoot()
    {
        PS.Play(false);
    }

    public void EndShoot()
    {
        PS.Stop();
    }
}