using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    [SerializeField] AdManager adMan;
    public bool menu;

    public void LoadNextScene(){
        if(adMan != null) adMan.RemList();
        PlayerPrefs.Save();
        if(menu) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(1);
    }

    public void CloseAnim(){
        GetComponent<Animator>().SetTrigger("Dead");
    }
}
