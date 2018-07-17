using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macaz : MonoBehaviour
{
	[SerializeField]
	private SwitchType switchType;

	[SerializeField]
	private GameObject[] switchPrefabs;

	private GameObject currentSwitch;

	[SerializeField]
	private MeshRenderer macazRenderer;

	[SerializeField]
	private bool spawnGeometry;

	[SerializeField]
	private TerrainGrid grid;

	[SerializeField]
	private Animator animator;

	private int currentSwitchIndex;

	void Start()
	{
		if (spawnGeometry)
		{
			Spawn();
		}
	}

	public int TypeIndex
	{
		get
		{
			return (int)switchType;
		}
	}

	public void Spawn()
	{
		currentSwitchIndex = 0;

		currentSwitch = Instantiate(
			switchPrefabs[currentSwitchIndex],
			transform.position,
			switchPrefabs[currentSwitchIndex].transform.rotation);

		switchType = (SwitchType)currentSwitch.GetComponent<Macaz>().TypeIndex;

		grid.ChangeSwitch(Vector3Int.RoundToInt(transform.position), (int)switchType);
	}

	public void SwitchSwitchType()
	{
		if (currentSwitch != null)
		{
			Destroy(currentSwitch);
		}

		currentSwitchIndex = (currentSwitchIndex + 1) % switchPrefabs.Length;

		currentSwitch = Instantiate(
			switchPrefabs[currentSwitchIndex],
			transform.position,
			switchPrefabs[currentSwitchIndex].transform.rotation);

		switchType = (SwitchType)currentSwitch.GetComponent<Macaz>().TypeIndex;

		grid.ChangeSwitch(Vector3Int.RoundToInt(transform.position), (int)switchType);

		animator.SetTrigger("isUsed");
	}

	public void Highlight()
	{
		macazRenderer.material.color = Color.green;
	}

	public void UnHighlight()
	{
		macazRenderer.material.color = Color.white;
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