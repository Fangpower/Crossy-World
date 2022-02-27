using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject ground;
    [SerializeField] Animator anim;
    [SerializeField] GameObject deadScreen;
    [SerializeField] Death death;
    [SerializeField] Movement move;
    [SerializeField] CameraMovement camMov;
    public bool canDestory;

    void OnTriggerStay(Collider col){
        if(canDestory){
            if(col.gameObject.CompareTag("Water") || col.gameObject.CompareTag("Animal")){
                print("Bro");
                canDestory = false;
                GameObject.Destroy(col.gameObject);
                Instantiate(ground, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
                Restart();
            }
        }
        if(col.gameObject.CompareTag("Obstacle") && canDestory){
            GameObject.Destroy(col.gameObject);
        }
    }

    public void Restart(){
        anim.ResetTrigger("Reset");
        move.canMove = true;
        if(transform.position.z >= 14){
            move.targetPos = new Vector3(move.targetPos.x, move.targetPos.y, 12);
            print("Greater");
        } else if(transform.position.z <= -14){
            move.targetPos = new Vector3(move.targetPos.x, move.targetPos.y, -12);
            print("less");
        }

        death.UnDie();
        anim.ResetTrigger("Runover");
        anim.ResetTrigger("Drown");
        anim.SetTrigger("Reset");
        deadScreen.SetActive(false);
    }
}
