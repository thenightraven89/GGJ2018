using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGrid : MonoBehaviour
{
	private List<TerrainTile> availableTiles;

	private Dictionary<Vector3Int, TerrainTile> switches;
	
	private void Awake()
	{
		availableTiles = new List<TerrainTile>();

		var nwTile = new TerrainTile();
		nwTile.AddRoute(new Vector3Int(0, 0, -1), new Vector3Int(-1, 0, 0));
		nwTile.AddRoute(new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, -1));
		availableTiles.Add(nwTile);

		var neTile = new TerrainTile();
		neTile.AddRoute(new Vector3Int(0, 0, -1), new Vector3Int(1, 0, 0));
		neTile.AddRoute(new Vector3Int(1, 0, 0), new Vector3Int(0, 0, -1));

		var seTile = new TerrainTile();
		seTile.AddRoute(new Vector3Int(0, 0, 1), new Vector3Int(1, 0, 0));
		seTile.AddRoute(new Vector3Int(1, 0, 0), new Vector3Int(0, 0, 1));

		var swTile = new TerrainTile();
		swTile.AddRoute(new Vector3Int(0, 0, 1), new Vector3Int(-1, 0, 0));
		swTile.AddRoute(new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 1));

		var nsTile = new TerrainTile();
		nsTile.AddRoute(new Vector3Int(0, 0, -1), new Vector3Int(0, 0, -1));
		nsTile.AddRoute(new Vector3Int(0, 0, 1), new Vector3Int(0, 0, 1));

		var weTile = new TerrainTile();
		weTile.AddRoute(new Vector3Int(-1, 0, 0), new Vector3Int(-1, 0, 0));
		weTile.AddRoute(new Vector3Int(1, 0, 0), new Vector3Int(1, 0, 0));

		switches = new Dictionary<Vector3Int, TerrainTile>();

		var macazuri = FindObjectsOfType(typeof(Macaz)) as Macaz[];
		foreach (var macaz in macazuri)
		{
			switches.Add(
				Vector3Int.FloorToInt(macaz.transform.position),
				availableTiles[macaz.TypeIndex]);
		}
	}
}

public class TerrainTile
{
	public Dictionary<Vector3Int, Vector3Int> Routes {get; private set;}

	public TerrainTile()
	{
		Routes = new Dictionary<Vector3Int, Vector3Int>();
	}

	public void AddRoute(Vector3Int inDirection, Vector3Int outDirection)
	{
		Routes.Add(inDirection, outDirection);
	}
}