using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;

public class Patterner : ALComponentSingleton<Patterner> {

    [SerializeField]
    private MapGenerator _mapGenerator;

    [SerializeField]
    private float _patternDelay = 0.5f;
    public float patternDelay { get { return _patternDelay; } set { _patternDelay = value; } }

    [SerializeField]
    private int _currentPatternCount;
    public int currentPatternCount { get { return _currentPatternCount; } }

    public int maxPatternCount { set; get; }
    public int mapWidth { get { return _mapGenerator.mapWidth; } }
    public int mapHeight { get { return _mapGenerator.mapHeight; } }
    public Tile[,] mapData { get { return _mapGenerator.mapData; } }
    private List<Tile> _patternTiles;

	void Start ()
    {
        _patternTiles = new List<Tile>();
        Debug.Log("Max Pattern Count : " + maxPatternCount);
	}

    public void runPattern()
    {
        StartCoroutine("RunPattern");
    }

    private IEnumerator RunPattern()
    {
        ParsePattern();
        _currentPatternCount = 1;
        while (true)
        {
            for (int i = 0; i < _patternTiles.Count; ++i)
            {
                if (_patternTiles[i].patternValue.Equals(_currentPatternCount))
                    _patternTiles[i].Excute();
                else if (_patternTiles[i].patternValue > _currentPatternCount)
                    break;
            }
            if (maxPatternCount >= _currentPatternCount + 1)
                ++_currentPatternCount;
            else
                break;
            yield return new WaitForSeconds(_patternDelay);
        }
    }

    public void ParsePattern()
    {
        _patternTiles.Clear();
        for (int x = 0; x < mapWidth; ++x)
        {
            for (int y = 0; y < mapHeight; ++y)
            {
                if (!mapData[x, y].patternValue.Equals(0))
                {
                    _patternTiles.Add(mapData[x, y]);
                }
            }
        }
        _patternTiles.Sort(TileSort);
        for (int i = 0; i < _patternTiles.Count; ++i)
        {
            Debug.Log(_patternTiles[i].patternValue + "|" + _patternTiles[i].name);
        }
    }

    private int TileSort(Tile t1, Tile t2)
    {
        if (t1.patternValue > t2.patternValue)
            return 1;
        else if (t1.patternValue < t2.patternValue)
            return -1;
        return 0;
    }
}
