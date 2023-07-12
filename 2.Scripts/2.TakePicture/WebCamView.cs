using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamView : MonoBehaviour
{
    public RawImage mainRawImg;
    public RawImage uiRawImg;
    WebCamDevice[] webCamDevices;
    WebCamTexture webCamTexture;


    void Start()
    {
        //현재 사용 가능한 카메라 리스트
        webCamDevices = WebCamTexture.devices;

        //사용할 카메라 선택
        //가장 처음 검색되는 후면 카메라 사용
        //int cameraIndex = -1;
        for(int i = 0; i < webCamDevices.Length; i++)
        {
            //폰 후면 카메라인지 체크
            //if(webCamDevices[i].isFrontFacing.Equals(false))
            //{
            //    //해당카메라 선택
            //    cameraIndex = i;
            //    break;
            //}

            //후면 카메라가 아닌지 체크
            if (webCamDevices[i].isFrontFacing.Equals(true))
            {
                //선택된 카메라에 대한 새로운 WebCamTexture생성
                webCamTexture = new WebCamTexture(webCamDevices[i].name);
                break;
            }
        }

        //원하는 FPS설정
        if(webCamTexture != null)
        {
            webCamTexture.requestedFPS = 60f;
            mainRawImg.texture = webCamTexture;
            uiRawImg.texture = webCamTexture;
            webCamTexture.Play();
        }
    }

    
    private void OnDestroy()
    {
        //WebCamTexture리소스 반환
        if(webCamTexture != null)
        {
            webCamTexture.Stop();
            WebCamTexture.Destroy(webCamTexture);
        }
    }
}
