using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoCtrl : MonoBehaviour {

    [SerializeField]
    Text _text;

    [SerializeField]
    Save _save;

	private void Start ()
    {
        _text.text = string.Empty;
        StartCoroutine("Squncer");
    }

    private IEnumerator Squncer()
    {
        yield return new WaitForSeconds(0.3f);
        _text.text = ":D";
        yield return new WaitForSeconds(0.5f);
        _text.text = ":D:D";
        yield return new WaitForSeconds(0.5f);
        _text.text = ":D:D:D";
        yield return new WaitForSeconds(0.6f);
        _text.text = ":D:D:D\nDef Dev Dap";
        yield return new WaitForSeconds(0.5f);
        _text.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (_save.isFirst)
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("InGame");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}