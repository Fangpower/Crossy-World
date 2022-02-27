using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinButtons : MonoBehaviour
{
    public void Skin(GameObject model){
        GameObject.Find("PlayerInfoObject").GetComponent<PlayerInfo>().player = model;
        PlayerPrefs.SetString("PlayerModel", model.name);
        PlayerPrefs.Save();
    }

    public void Quit(){
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
//Why???