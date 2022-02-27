using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject characterMenu;

    public void Home(){
        anim.SetTrigger("Menu");
    }

    public void Character(){
        anim.SetTrigger("Skins");
    }
}
