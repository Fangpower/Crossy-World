using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public string AndriodID = "4514711";
    public string IOSID = "4514710";
    public string videoType = "Rewarded_Android";
    private bool testMode = true;

    [SerializeField] Respawn respawn;
    [SerializeField] CameraMovement camMov;
    [SerializeField] Death death;
    [SerializeField] GameObject score;

    public bool camDeath;

    void Awake(){
        Advertisement.AddListener(this);
        #if UNITY_ANDROID
            Advertisement.Initialize(AndriodID, testMode);
        #else
            Advertisement.Initialize(IOSID, testMode);
        #endif
    }

    public void ShowAd(){
        Advertisement.Show(videoType);
    }

    public void OnUnityAdsReady(string placementId){

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult result){
        print(result);
        if(result == ShowResult.Finished){
            camMov.canUpdate = true;
            StartCoroutine(camMov.Moved());
            RemList();
            if(camDeath){
                respawn.Restart();
                respawn.canDestory = false;
            }
        }else if(result == ShowResult.Skipped){
            RemList();
        }else if(result == ShowResult.Failed){
            RemList();
            death.Continue(false);
        }
    }

    public void OnUnityAdsDidError(string message){
        print(message);
    }

    public void OnUnityAdsDidStart(string placementId){
        respawn.canDestory = true;
        StartCoroutine(camMov.Restart());
    }

    public void RemList(){
        Advertisement.RemoveListener(this);
    }
}
