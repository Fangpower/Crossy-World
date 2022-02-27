using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public GameObject player = null;
    public static PlayerInfo instance;
    private bool done;

    void Awake()
    {   
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        if(PlayerPrefs.HasKey("PlayerModel")){
            player = Resources.Load<GameObject>("Characters/" + PlayerPrefs.GetString("PlayerModel"));
        }
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode  mode){
        done = false;
        if(!done){
            GameObject hoppy = GameObject.Find("Hoppy");
            if(hoppy != null){
                var model = Instantiate(player, hoppy.transform.position, Quaternion.identity);
                model.transform.parent = hoppy.transform.GetChild(0).transform;
            }
            done = true;
        }
    }

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
    }
}
