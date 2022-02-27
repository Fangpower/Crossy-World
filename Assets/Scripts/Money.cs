using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public static float money = 0;
    TMP_Text moneyText;

    void Start(){
        if(GameObject.Find("Unlock")){
            moneyText = GameObject.Find("Unlock").transform.GetChild(0).GetComponent<TMP_Text>();
            moneyText.text = money + "/100";
        }

        if(PlayerPrefs.HasKey("PlayerMoney")){
            money = PlayerPrefs.GetFloat("PlayerMoney");
            print(PlayerPrefs.GetFloat("PlayerMoney"));
            moneyText.text = money + "/100";
        }
    }
    
    public void AddMoney(float amount){
        money = money + amount;
        PlayerPrefs.SetFloat("PlayerMoney", money);
        PlayerPrefs.Save();
    }

    public void RemoveMoney(float amount){
        money = money + amount;
        moneyText.text = money + "/100";
        PlayerPrefs.SetFloat("PlayerMoney", money);
        PlayerPrefs.Save();
    }

    public float GetMoney(){
        return money;
    }
}
