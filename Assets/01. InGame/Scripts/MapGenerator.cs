using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour {

    [SerializeField]
    GameObject _tile;

    [SerializeField]
    Transform _tileParent;

    [SerializeField]
    private int _mapWidth = 8;

    [SerializeField]
    private int _mapHeight = 10;

    [SerializeField]
    private int _mapMarginWidht = 10;

    [SerializeField]
    private int _mapMarginHeight = 10;

    [SerializeField]
    private int _tileWidth = 70;

    [SerializeField]
    private int _tileHeight = 70;

    [SerializeField]
    private int _tileMargin = 10; 

	private void Awake ()
    {
        CreateTiles();
	}

    private void CreateTiles()
    {
        for (int y = 0; y < _mapHeight; ++y)
        {
            for (int x = 0; x < _mapWidth; ++x)
            {
                GameObject tile = Instantiate(_tile, Vector3.zero, Quaternion.identity, _tileParent);
                tile.transform.localPosition = new Vector2(_mapMarginWidht + (x * (_tileWidth + _tileMargin)), _mapMarginHeight + (y * (_tileHeight + _tileMargin)));
                tile.name = string.Format("Tile_{0}_{1}", x, y);
            }
        }
    }
}
