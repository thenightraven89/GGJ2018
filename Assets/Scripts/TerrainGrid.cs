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
		nwTile.AddRoute(new Vector3Int(1, 0, 0), new Vector3Int(0, 0, 1));
		availableTiles.Add(nwTile);

		var neTile = new TerrainTile();
		neTile.AddRoute(new Vector3Int(0, 0, -1), new Vector3Int(1, 0, 0));
		neTile.AddRoute(new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 1));
		availableTiles.Add(neTile);

		var seTile = new TerrainTile();
		seTile.AddRoute(new Vector3Int(0, 0, 1), new Vector3Int(1, 0, 0));
		seTile.AddRoute(new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, -1));
		availableTiles.Add(seTile);

		var swTile = new TerrainTile();
		swTile.AddRoute(new Vector3Int(0, 0, 1), new Vector3Int(-1, 0, 0));
		swTile.AddRoute(new Vector3Int(1, 0, 0), new Vector3Int(0, 0, -1));
		availableTiles.Add(swTile);

		var nsTile = new TerrainTile();
		nsTile.AddRoute(new Vector3Int(0, 0, -1), new Vector3Int(0, 0, -1));
		nsTile.AddRoute(new Vector3Int(0, 0, 1), new Vector3Int(0, 0, 1));
		availableTiles.Add(nsTile);

		var weTile = new TerrainTile();
		weTile.AddRoute(new Vector3Int(-1, 0, 0), new Vector3Int(-1, 0, 0));
		weTile.AddRoute(new Vector3Int(1, 0, 0), new Vector3Int(1, 0, 0));
		availableTiles.Add(weTile);

		switches = new Dictionary<Vector3Int, TerrainTile>();

		var macazuri = FindObjectsOfType(typeof(Macaz)) as Macaz[];
		foreach (var macaz in macazuri)
		{
			//Debug.Log(macaz.TypeIndex);
			switches.Add(
				Vector3Int.RoundToInt(macaz.transform.position),
				availableTiles[macaz.TypeIndex]);
		}
	}

	public void ChangeSwitch(Vector3Int pos, Macaz macaz)
	{
		switches[pos] = availableTiles[macaz.TypeIndex];
	}

	public Vector3Int GetDirectionFor(Vector3Int pos, Vector3Int direction)
	{
		if (switches.ContainsKey(pos))
		{
			var tile = switches[pos];
			if (tile.Routes.ContainsKey(direction))
			{
				return tile.Routes[direction];
			}
			else
			{
				Debug.Log(direction);
				return direction;
			}
		}
		else
		{
			return direction;
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