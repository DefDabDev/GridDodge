using AL.ALUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ResourcesPathes
{
    public const string Pattern = "PatternDatas/";
}

//[ExecuteInEditMode]
public class MapGenerator : ALComponentSingleton<MapGenerator>
{

    [SerializeField]
    Tile _tile;

    [SerializeField]
    Transform _tileParent;

    [SerializeField]
    private string _patternName;

    [SerializeField]
    private int _mapWidth = 8;
    public int mapWidth { get { return _mapWidth; } }

    [SerializeField]
    private int _mapHeight = 10;
    public int mapHeight { get { return _mapHeight; } }

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

    private Tile[,] _mapData;
    public Tile[,] mapData { get { return _mapData; } set { _mapData = value; } }
    
    public void init()
    {
        Patterner.instance.maxPatternCount = 1;
        _mapData = new Tile[_mapWidth, _mapHeight];
        CreateTiles();
        ParseData();
        CheckData();
    }

    private void CreateTiles()
    {
        for (int y = 0; y < _mapHeight; ++y)
        {
            for (int x = 0; x < _mapWidth; ++x)
            {
                Tile tile = Instantiate(_tile, Vector3.zero, Quaternion.identity, _tileParent);
                tile.transform.localPosition = new Vector2(_mapMarginWidht + (x * (_tileWidth + _tileMargin)), _mapMarginHeight + (y * (_tileHeight + _tileMargin)));
                tile.name = string.Format("Tile_{0}_{1}", x, y);
                tile.index = new Vector2(x, y);
                _mapData[x, y] = tile;
                _mapData[x, y].image = tile.gameObject.GetComponent<Image>();
            }
        }
    }

    private void ParseData()
    {
        TextAsset _textAsset = Resources.Load<TextAsset>(string.Format("{0}{1}", ResourcesPathes.Pattern, _patternName));
        string[] separator = { "|", "\r\n" };
        string[] temp = _textAsset.text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        int height = 0;
        int width = _mapWidth - 1;
        for (int a = temp.Length - 1; a > 0; --a)
        {
            if (width < 0)
            {
                ++height;
                width = _mapWidth - 1;
            }
            _mapData[width, height].patternValue = int.Parse(temp[a]);
            if (_mapData[width, height].patternValue > Patterner.instance.maxPatternCount)
                Patterner.instance.maxPatternCount = _mapData[width, height].patternValue;
            --width;
        }
    }

    private void ParseData(string patternName)
    {
        Patterner.instance.maxPatternCount = 1;
        TextAsset _textAsset = Resources.Load<TextAsset>(string.Format("{0}{1}", ResourcesPathes.Pattern, patternName));
        string[] separator = { "|", "\r\n" };
        string[] temp = _textAsset.text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        int height = 0;
        int width = _mapWidth - 1;
        for (int a = temp.Length - 1; a > 0; --a)
        {
            if (width < 0)
            {
                ++height;
                width = _mapWidth - 1;
            }
            _mapData[width, height].patternValue = int.Parse(temp[a]);
            if (_mapData[width, height].patternValue > Patterner.instance.maxPatternCount)
                Patterner.instance.maxPatternCount = _mapData[width, height].patternValue;
            --width;
        }
        Debug.Log(string.Format("{0} : {1}", patternName, Patterner.instance.maxPatternCount));
        CheckData();
    }

    private void CheckData()
    {
        for (int y = 0; y < _mapHeight; ++y)
        {
            for (int x = 0; x < _mapWidth; ++x)
            {
                _mapData[x, y].text.text = _mapData[x, y].patternValue.ToString();
            }
        }
    }

    public void SetRandomPattern(DIFFICULTY difficulty)
    {
        switch(difficulty)
        {
            case DIFFICULTY.EASY:
                ParseData(string.Format("Pattern_Easy{0}", Mathf.Floor(UnityEngine.Random.Range(1, 9))));
                break;

            case DIFFICULTY.NORMAL:
                ParseData(string.Format("Pattern_Easy{0}", Mathf.Floor(UnityEngine.Random.Range(1, 9))));
                break;

            case DIFFICULTY.HARD:
                ParseData(string.Format("Pattern_Hard{0}", Mathf.Floor(UnityEngine.Random.Range(1, 9))));
                break;
        }
    }

    public void SetPattern(string patternName)
    {
        ParseData(patternName);
    }
}
