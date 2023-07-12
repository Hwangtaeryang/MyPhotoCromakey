using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiCameraPictureShot : MonoBehaviour
{
    private static UiCameraPictureShot instance;

    

    private Camera uiCamera;
    private bool takeScreenShotOnNextFrame;


    private void Awake()
    {
        instance = this;
        uiCamera = gameObject.GetComponent<Camera>();
    }


    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }

    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext _context, Camera _camera)
    {
        OnPostRender();
    }

    private void OnPostRender()
    {
        if(takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = uiCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            //System.IO.File.WriteAllBytes(Application.persistentDataPath + "", byteArray);
            FolderPath(byteArray);

            RenderTexture.ReleaseTemporary(renderTexture);
            uiCamera.targetTexture = null;
        }
    }

    void FolderPath(byte[] _byteArray)
    {
        if(SceneManager.GetActiveScene().name.Equals("2_TakePicture"))
        {
            string folderPath = Application.persistentDataPath + "/BasicPictureShot";

            //폴더가 없으면 생성한다.
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/BasicPictureShot/BasicPicture.png", _byteArray);
            GameManager.instance.SFXSound("CameraShutter");
        }
        else if(SceneManager.GetActiveScene().name.Equals("3_PictureCompose"))
        {
            string folderPath = Application.persistentDataPath + "/PictureCompose";

            //폴더가 없으면 생성한다.
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/PictureCompose/ComposePicture.png", _byteArray);
        }
        else if(SceneManager.GetActiveScene().name.Equals("4_FilterChoice"))
        {
            string folderPath = Application.persistentDataPath + "/Print";

            //폴더가 없으면 생성한다.
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/Print/Print.png", _byteArray);
        }
    }    

    void UITakePictureShot(int _width, int _height)
    {
        uiCamera.targetTexture = RenderTexture.GetTemporary(_width, _height, 16);
        takeScreenShotOnNextFrame = true;
    }

    public static void Static_UITakePictureShot(int _width, int _height)
    {
        instance.UITakePictureShot(_width, _height);
    }

    public void PictureSave()
    {
        GameManager.instance.SFXSound("ClickSound");
        Static_UITakePictureShot(2400, 1600);
        StartCoroutine(_SceneMove());
    }

    IEnumerator _SceneMove()
    {
        yield return new WaitForSeconds(1f);

        if (SceneManager.GetActiveScene().name.Equals("2_TakePicture"))
            SceneManager.LoadScene("3_PictureCompose");
        else if (SceneManager.GetActiveScene().name.Equals("4_FilterChoice"))
            SceneManager.LoadScene("5_Print");
    }
}
