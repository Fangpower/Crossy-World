using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSkin : MonoBehaviour
{
    [SerializeField] GameObject currentSelected;

    void Start(){
        if(PlayerPrefs.HasKey("SelectedSkinOutline")){
            GameObject child = GameObject.Find(PlayerPrefs.GetString("SelectedSkinOutline"));
            child.transform.GetChild(0).gameObject.SetActive(true);
            currentSelected = child.transform.GetChild(0).gameObject;
        } else {
            currentSelected.SetActive(true);
        }
    }

    public void OnClick(GameObject child){
        currentSelected.SetActive(false);
        child.SetActive(true);
        currentSelected = child;
        PlayerPrefs.SetString("SelectedSkinOutline", child.transform.parent.name);
    }
}
