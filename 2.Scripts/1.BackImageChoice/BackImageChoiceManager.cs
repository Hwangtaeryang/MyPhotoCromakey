using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackImageChoiceManager : MonoBehaviour
{
    public static BackImageChoiceManager instance { get; private set; }


    public RawImage viewRawImg;
    public Transform makeParentPos; //필터오브젝트 생성 위치
    public GameObject copyPrefab;  //복사할 오브젝트
    GameObject copyObj;


    FileInfo[] backImgData;  //폴더 속 사진 정보
    string folderPath;
    public int backImgMaxNumber;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        folderPath = Application.persistentDataPath + "/BackImage";

        if (Directory.Exists(folderPath).Equals(false))  //폴더없으면 생성
            Directory.CreateDirectory(folderPath);

        DirectoryInfo di = new DirectoryInfo(folderPath);
        backImgData = di.GetFiles("*.png");
        backImgMaxNumber = backImgData.Length;

        //백이미지 갯수에 맞게 생성하기
        for(int i = 0; i < backImgMaxNumber; i++)
        {
            copyObj = Instantiate(copyPrefab, makeParentPos);
            byte[] filebyte = File.ReadAllBytes(Application.persistentDataPath + "/BackImage/" + backImgData[i].Name);
            Texture2D backImgTexture = null;
            backImgTexture = new Texture2D(0, 0);
            backImgTexture.LoadImage(filebyte);
            copyObj.GetComponent<RawImage>().texture = backImgTexture;

            if(i.Equals(0))
                viewRawImg.texture = backImgTexture;

            copyObj.name = "Button " + (i + 1).ToString();
            //copyObj.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString() + "번";
        }
    }

    public void BackImageClickViewShow(int _index)
    {
        byte[] filebyte = File.ReadAllBytes(Application.persistentDataPath + "/BackImage/" + backImgData[_index].Name);
        Texture2D backImgTexture = null;
        backImgTexture = new Texture2D(0, 0);
        backImgTexture.LoadImage(filebyte);
        viewRawImg.texture = backImgTexture;
    }

    public void ClickBackImageNameSave(int _index)
    {
        string name = backImgData[_index].Name;
        PlayerPrefs.SetString("PSC_ChoiceBackImageName", name);
    }

    public void SFXSound()
    {
        GameManager.instance.SFXSound("ClickSound");
    }

    public void BackImageChoiceSceneMove(string _name)
    {
        SceneManager.LoadScene(_name);
    }
}
