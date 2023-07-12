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
        //���� ��� ������ ī�޶� ����Ʈ
        webCamDevices = WebCamTexture.devices;

        //����� ī�޶� ����
        //���� ó�� �˻��Ǵ� �ĸ� ī�޶� ���
        //int cameraIndex = -1;
        for(int i = 0; i < webCamDevices.Length; i++)
        {
            //�� �ĸ� ī�޶����� üũ
            //if(webCamDevices[i].isFrontFacing.Equals(false))
            //{
            //    //�ش�ī�޶� ����
            //    cameraIndex = i;
            //    break;
            //}

            //�ĸ� ī�޶� �ƴ��� üũ
            if (webCamDevices[i].isFrontFacing.Equals(true))
            {
                //���õ� ī�޶� ���� ���ο� WebCamTexture����
                webCamTexture = new WebCamTexture(webCamDevices[i].name);
                break;
            }
        }

        //���ϴ� FPS����
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
        //WebCamTexture���ҽ� ��ȯ
        if(webCamTexture != null)
        {
            webCamTexture.Stop();
            WebCamTexture.Destroy(webCamTexture);
        }
    }
}
