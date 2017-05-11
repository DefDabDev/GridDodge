using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCtrl : MonoBehaviour {

    [SerializeField]
    Save _save;

    [SerializeField]
    Transform[] _targets;

    int _index = 0;
    [SerializeField]
    bool _isRun = false;

	void Awake ()
    {
        _save.isFirst = false;
	}
	
	public void Touch()
    {
        StartCoroutine("Pass");
    }

    private IEnumerator Pass()
    {
        if (_isRun)
            yield break;
        _isRun = true;
        if (_index.Equals(1))
        {
            SceneManager.LoadScene("InGame");
            yield break;
        }

        float time = 0f;
        while(true)
        {
            if (_targets[_index].localPosition.x.Equals(-720))
                break;
            time += Time.deltaTime;
            _targets[_index].localPosition = ALLerp.Lerp(_targets[_index].localPosition, new Vector3(-720f, 0f, 0f), time);
            yield return null;
        }
        _index++;
        _isRun = false;
    }
}
