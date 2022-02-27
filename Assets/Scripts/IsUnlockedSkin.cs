using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsUnlockedSkin : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] bool DefualtEnabled;
    public GameObject skin;

    void Start(){
        if(PlayerPrefs.GetInt(title) == 1 || DefualtEnabled){
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void Enable(){
        PlayerPrefs.SetInt(title, 1);
        gameObject.GetComponent<Button>().interactable = true;
        print("Enabled: " + title);
    }
}
