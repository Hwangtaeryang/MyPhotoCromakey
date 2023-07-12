using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackImageButtonCtrl : MonoBehaviour
{
    public int index = 0;

    void Start()
    {
        for(int i = 0; i < BackImageChoiceManager.instance.backImgMaxNumber; i++)
        {
            if (gameObject.name.Equals("Button " + (i + 1).ToString()))
                index = i;
        }
    }

    
    public void ButtonClickOn()
    {
        GameManager.instance.SFXSound("ClickSound");
        BackImageChoiceManager.instance.BackImageClickViewShow(index);
        BackImageChoiceManager.instance.ClickBackImageNameSave(index);
    }
}
