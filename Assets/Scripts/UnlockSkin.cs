using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkin : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject chest;
    [SerializeField] Animator anim;
    [SerializeField] GameObject skinParent;

    public void OnClick(){
        if(GameObject.Find("MoneyManager").GetComponent<Money>().GetMoney() >= 100){
            GameObject[] skins = new GameObject[panel.transform.childCount];
            int i = 0;

            foreach(Transform skin in panel.transform){
                if(!skin.gameObject.GetComponent<Button>().IsInteractable()){
                    skins[i] = skin.gameObject;
                    i++;
                }
            }

            int randSkin = Random.Range(0, i);
            skins[randSkin].GetComponent<IsUnlockedSkin>().Enable();
            chest.SetActive(true);
            var newSkin = Instantiate(skins[randSkin].GetComponent<IsUnlockedSkin>().skin, skinParent.transform.position, Quaternion.identity);
            newSkin.transform.parent = skinParent.transform;
            anim.SetTrigger("Open");
            GameObject.Find("MoneyManager").GetComponent<Money>().RemoveMoney(100);
        }
    }

    public void EndAnim(){
        chest.SetActive(false);
        GameObject.Destroy(skinParent.transform.GetChild(0).gameObject);
        anim.ResetTrigger("Open");
    }
}
