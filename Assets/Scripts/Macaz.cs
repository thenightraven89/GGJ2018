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
		currentSwitch = Instantiate(
			switchPrefabs[(int)switchType],
			transform.position,
			switchPrefabs[(int)switchType].transform.rotation);

		grid.ChangeSwitch(Vector3Int.RoundToInt(transform.position), this);
	}

	public void SwitchSwitchType()
	{
		switchType = (SwitchType)(((int)switchType + 1) % System.Enum.GetNames(typeof(SwitchType)).Length);

		if (currentSwitch != null)
		{
			Destroy(currentSwitch);
		}

		currentSwitch = Instantiate(
			switchPrefabs[(int)switchType],
			transform.position,
			switchPrefabs[(int)switchType].transform.rotation);

		grid.ChangeSwitch(Vector3Int.RoundToInt(transform.position), this);
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