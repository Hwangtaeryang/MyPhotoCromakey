using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




//https://stackoverflow.com/questions/49188307/how-to-direct-printing-of-photo-or-text-using-unity-without-preview
public class PrintManager : MonoBehaviour
{
    public RawImage viewRawImg;
    public GameObject printPopup;
    public Button printBtn;

    string printImgPath;

    void Start()
    {
        PrintViewPictureInit();
    }

    void PrintViewPictureInit()
    {
        printImgPath = Application.persistentDataPath + "/Print/Print.png";
        byte[] printImgData = File.ReadAllBytes(printImgPath);
        Texture2D printTexture = null;
        printTexture = new Texture2D(0, 0);
        printTexture.LoadImage(printImgData);
        viewRawImg.texture = printTexture;
    }

    public void PrintButtonOn()
    {
        GameManager.instance.SFXSound("ClickSound");

        string printerName = "Sinfonia CHC-S2245";
        string _rePrintPath = Regex.Replace(printImgPath, "/", "\\");
        string printFullCommand = 
            "rundll32 C:\\WINDOWS\\system32\\shimgvw.dll,ImageView_PrintTo " + "\"" + _rePrintPath + "\"" + " " + "\"" + printerName + "\"";
        
        PrinterStart(printFullCommand);
    }

    void PrinterStart(string _cmd)
    {
        try
        {
            Process myProcess = new Process();
            //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.Arguments = "/c " + _cmd;
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();

            printBtn.interactable = false;
            StartCoroutine(Printing());
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
            printPopup.SetActive(true);
            printPopup.transform.GetChild(1).GetComponent<Text>().text = "프린트 하는 중 에러가 발생하였습니다.\n"
                + e;
        }
    }

    IEnumerator Printing()
    {
        yield return new WaitForSeconds(1f);
        printPopup.SetActive(true);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다.";
        yield return new WaitForSeconds(1f);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다..";
        yield return new WaitForSeconds(1f);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다...";
        yield return new WaitForSeconds(1f);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다.";
        yield return new WaitForSeconds(1f);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다..";
        yield return new WaitForSeconds(1f);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다...";
        yield return new WaitForSeconds(1f);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다.";
        yield return new WaitForSeconds(1f);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다..";
        yield return new WaitForSeconds(1f);
        printPopup.transform.GetChild(1).GetComponent<Text>().text = "사진이 인쇄중입니다...";
        yield return new WaitForSeconds(1f);
        printPopup.SetActive(false);
    }

    public void SceneMove(string _name)
    {
        GameManager.instance.SFXSound("ClickSound");
        SceneManager.LoadScene(_name);
    }
}
