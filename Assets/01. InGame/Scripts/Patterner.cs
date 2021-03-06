﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;
using GM;

public enum DIFFICULTY
{
    EASY,
    NORMAL,
    HARD
}

public class Patterner : ALComponentSingleton<Patterner> {

    [SerializeField]
    private DIFFICULTY _difficulty = DIFFICULTY.EASY;

    [SerializeField]
    private MapGenerator _mapGenerator;

    [SerializeField]
    private AudioSource _se;

    [SerializeField]
    private float _patternDelay = 0.5f;
    public float patternDelay { get { return _patternDelay; } set { _patternDelay = value; } }

    [SerializeField]
    private int _currentPatternCount;
    public int currentPatternCount { get { return _currentPatternCount; } }

    [SerializeField]
    private Sprite[] _spriteList;

    public int maxPatternCount { set; get; }
    public int mapWidth { get { return _mapGenerator.mapWidth; } }
    public int mapHeight { get { return _mapGenerator.mapHeight; } }
    public Tile[,] mapData { get { return _mapGenerator.mapData; } }
    public Tile[,] nextMapData { get { return _mapGenerator.mapData; } }
    private List<Tile> _patternTiles;

	void Start ()
    {
        _patternTiles = new List<Tile>();
	}

    public void runPattern()
    {
        StartCoroutine("RunPattern");
    }

    private IEnumerator RunPattern()
    {
        _mapGenerator.SetPattern("Pattern_Start");
        ParsePattern();
        _currentPatternCount = 1;
        while (true)
        {
            if (GameManager.pause)
                yield return StartCoroutine("Pause");

            for (int i = 0; i < _patternTiles.Count; ++i)
            {
                if (_patternTiles[i].patternValue.Equals(_currentPatternCount))
                {
                    _patternTiles[i].ExcuteReady();
                    SetRandomObject(_patternTiles[i]);
                }
                else if (_patternTiles[i].patternValue > _currentPatternCount)
                    break;
            }
            yield return new WaitForSeconds(_patternDelay);
            for (int i = 0; i < _patternTiles.Count; ++i)
            {
                if (_patternTiles[i].patternValue.Equals(_currentPatternCount))
                {
                    _patternTiles[i].Excute();
                    SetInvisibleObject(_patternTiles[i]);
                }
                else if (_patternTiles[i].patternValue > _currentPatternCount)
                    break;
            }
            _se.Play();
            if (maxPatternCount >= _currentPatternCount + 1)
            {
                ++_currentPatternCount;
            }
            else
            {
                _currentPatternCount = 1;
                _mapGenerator.SetRandomPattern(_difficulty);
                ParsePattern();
            }
            yield return new WaitForSeconds(_patternDelay);
        }
    }

    private IEnumerator Pause()
    {
        while(GameManager.pause)
        {
            yield return null;
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
    }

    private void SetRandomObject(Tile tile)
    {
        int index = UnityEngine.Random.Range(1, _spriteList.Length);
        tile.tileObject.sprite = _spriteList[index];
    }

    private void SetInvisibleObject(Tile tile)
    {
        tile.tileObject.sprite = _spriteList[0];
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
