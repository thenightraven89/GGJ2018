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

    void OnDestroy()
    {
        if (PlayerLocation.childCount > 0)
        {
            var player = PlayerLocation.GetChild(0);
            player.GetComponent<CharacterInput>().DisengageGun();
        }
    }
}