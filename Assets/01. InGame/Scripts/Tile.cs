using CHAR;
using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    [SerializeField]
    private Image _tileObject;
    public Image tileObject { get { return _tileObject; } }

    [SerializeField]
    private Vector2 _index;
    public Vector2 index { get { return _index; } set { _index = value; } }

    [SerializeField]
    private int _patternValue;
    public int patternValue { get { return _patternValue; } set { _patternValue = value; } }

    private Image _image = null;
    public Image image { get { return _image; } set { _image = value; } }

    public void ExcuteReady()
    {
        StartCoroutine("ReadyEffect");
    }

    public IEnumerator ReadyEffect()
    {
        _image.color = Color.yellow;
        yield return new WaitForSeconds(1f);
        _image.color = Color.white;
    }

    public void Excute()
    {
        StartCoroutine("ExcuteEffect");
    }

    public IEnumerator ExcuteEffect()
    {
        Point value;
        value.posX = (uint)index.x;
        value.posY = (uint)index.y;
        GameManager.instance.bombCheck(value);
        _image.color = Color.red;
        yield return new WaitForSeconds(1f);
        _image.color = Color.white;
    }
}
