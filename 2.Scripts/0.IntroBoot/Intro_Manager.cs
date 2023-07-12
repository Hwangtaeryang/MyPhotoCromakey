using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro_Manager : MonoBehaviour
{
    public GameObject clickText;
    public GameObject endPopup;


    void Start()
    {
        StartCoroutine(_ClickTextBlink()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            endPopup.SetActive(true);
        }
    }

    public void EndButtonOn()
    {
        endPopup.SetActive(true);
    }

    IEnumerator _ClickTextBlink()
    {
        yield return new WaitForSeconds(0.5f);
        clickText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clickText.SetActive(false);

        StartCoroutine(_ClickTextBlink());
    }

    public void SceneMove(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    public void AppQuitButtonOn()
    {
        Application.Quit();
    }

    public void SFXSound()
    {
        GameManager.instance.SFXSound("ClickSound");
    }
}
