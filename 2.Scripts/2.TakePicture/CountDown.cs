using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Sprite[] countSprite;
    public Image countImg;
    public Button cameraBtn;


    void Start()
    {
        StartCoroutine(FiveSecondCount());
    }

    IEnumerator FiveSecondCount()
    {
        cameraBtn.interactable = false;
        //Debug.Log("µå··¿À´Ù");
        yield return new WaitForSeconds(1);
        countImg.sprite = countSprite[0];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1);
        countImg.sprite = countSprite[1];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1);
        countImg.sprite = countSprite[2];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1);
        countImg.sprite = countSprite[3];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1);
        countImg.sprite = countSprite[4];
        UiCameraPictureShot.Static_UITakePictureShot(2400, 1600);

        GameManager.instance.SFXSound("Countdown_end");
        yield return new WaitForSeconds(1);
        //countImg.gameObject.SetActive(false);
        StartCoroutine(_SceneMove());
    }

    IEnumerator _SceneMove()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("3_PictureCompose");
    }

}
