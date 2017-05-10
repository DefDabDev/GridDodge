using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    [SerializeField]
    private Text _text;
    public Text text { get { return _text; } }

    [SerializeField]
    private Vector2 _index;
    public Vector2 index { get { return _index; } set { _index = value; } }

    [SerializeField]
    private int _patternValue;
    public int patternValue { get { return _patternValue; } set { _patternValue = value; } }

    private Image _image = null;
    public Image image { get { return _image; } set { _image = value; } }

    public void Excute()
    {
        StartCoroutine("FadeEffect");
    }

    public IEnumerator FadeEffect()
    {
        _image.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _image.color = Color.white;
    }
}
