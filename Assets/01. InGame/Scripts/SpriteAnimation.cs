using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour {

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite[] _frames;

    [SerializeField]
    private float _fps;

    [SerializeField]
    private float _fpsScale;

    private int _frameCount = 0;
    private bool _isAnimating = false;

    public void Play()
    {
        if (_isAnimating)
            StopAllCoroutines();
        StartCoroutine("Run");
    }

    private IEnumerator Run()
    {
        if (_isAnimating)
            yield break;
        _isAnimating = true;

        float time = 0f;
        _frameCount = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time >= _fps)
            {
                time = 0;
                ++_frameCount;
                if (_frameCount >= _frames.Length)
                    break;
                else
                    _image.sprite = _frames[_frameCount];
            }
            yield return null;
        }
        _isAnimating = false;
    }
}
