using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PictureComposeManager : MonoBehaviour
{
    public RawImage backPickureImg;
    public GameObject uiCamera;

    void Start()
    {
        byte[] flieByte = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/BackImage/" + 
            PlayerPrefs.GetString("PSC_ChoiceBackImageName"));
        Texture2D texture = new Texture2D(0, 0);
        texture.LoadImage(flieByte);
        backPickureImg.texture = texture;
    }

    public void SceneMove(string _name)
    {
        GameManager.instance.SFXSound("ClickSound");
        SceneManager.LoadScene(_name);
    }

    public void ComposePictureSave()
    {
        GameManager.instance.SFXSound("ClickSound");
        StartCoroutine(_ComposePictureSave());
    }

    IEnumerator _ComposePictureSave()
    {
        uiCamera.GetComponent<Volume>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        UiCameraPictureShot.Static_UITakePictureShot(2400, 1600);

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("4_FilterChoice");
    }
}
