using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FilterChoiceManager : MonoBehaviour
{
    public static FilterChoiceManager instance { get; private set; }

    public RawImage uiRawImg;
    public Transform makeParentPos; //필터버튼 생성 위치
    public GameObject copyPrefab;   //복사할 원본 오브젝트
    GameObject copyObj;

    FileInfo[] filterImgData;  //폴더 속 필터이미지정보
    FileInfo[] filterData;  //폴더 속 필터정보
    string folderPath;
    string filterPath;
    public int filterImgMaxIndex = 0;



    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;
    }

    void Start()
    {
        GetUI_Image();
        FilterButtonAccout();
    }

    //UI canvas에 합성한 사진 들고오기
    void GetUI_Image()
    {
        byte[] composeImgByte = File.ReadAllBytes(Application.persistentDataPath + "/PictureCompose/ComposePicture.png");
        Texture2D composeTexture = null;
        composeTexture = new Texture2D(0, 0);
        composeTexture.LoadImage(composeImgByte);

        uiRawImg.texture = composeTexture;
    }

    //필터버튼 생성
    void FilterButtonAccout()
    {
        folderPath = Application.persistentDataPath + "/Filter/FilterButtonImage";
        DirectoryInfo di = new DirectoryInfo(folderPath);
        filterImgData = di.GetFiles("*.png");
        filterImgMaxIndex = filterImgData.Length;

        //필터 갯수에 맞게 생성하기
        for (int i = 0; i < filterImgMaxIndex; i++)
        {
            copyObj = Instantiate(copyPrefab, makeParentPos);
            byte[] filterByte = File.ReadAllBytes(Application.persistentDataPath +
                "/Filter/FilterButtonImage/" + filterImgData[i].Name);
            Texture2D filterTexture = null;
            filterTexture = new Texture2D(0, 0);
            filterTexture.LoadImage(filterByte);

            copyObj.GetComponent<Image>().sprite =
                Sprite.Create(filterTexture, new Rect(0, 0, filterTexture.width, filterTexture.height), new Vector2(0, 0f));

            string name = filterImgData[i].Name;
            name = name.Replace(".png", "");
            copyObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            copyObj.name = "Button " + (i + 1).ToString();
        }
    }

    public Texture FilterChoiceViewShow(int _index)
    {
        filterPath = Application.persistentDataPath + "/Filter/Filter";
        DirectoryInfo di = new DirectoryInfo(filterPath);
        filterData = di.GetFiles("*.png");

        //string filterName = filterData[_index].Name;
        //filterName = filterName.Replace(".png", "");
        byte[] filterByte = File.ReadAllBytes(Application.persistentDataPath + "/Filter/Filter/" + filterData[_index].Name);
        Texture2D filterTexture = null;
        filterTexture = new Texture2D(0, 0);
        filterTexture.LoadImage(filterByte);

        return filterTexture;
    }

    public void SceneMove(string _name)
    {
        GameManager.instance.SFXSound("ClickSound");
        SceneManager.LoadScene(_name);
    }
}
