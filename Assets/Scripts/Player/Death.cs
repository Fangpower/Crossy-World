using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public bool boat;
    [SerializeField] GroundSpawning gs;
    [SerializeField] CameraMovement camMov;
    [SerializeField] AdManager adMan;
    [SerializeField] Movement move;
    [SerializeField] float yPos;
    [SerializeField] GameObject glider;
    [SerializeField] Animator anim;
    [SerializeField] Animator animScreen;
    [SerializeField] GameObject DeathPrompt;
    [SerializeField] GameObject RespawnPrompt;
    [SerializeField] LoadNewScene sceneLoader;
    [SerializeField] GameObject score;
    [SerializeField] AudioSource spalsh;
    bool dead;
    bool respawned;

    void OnTriggerEnter(Collider col){        
        if(col.gameObject.CompareTag("Enemy")){
            dead = true;
            anim.SetTrigger("Runover");
        } else if(col.gameObject.CompareTag("Water") && !boat){
            dead = true;
            anim.SetTrigger("Drown");
        } else if(col.gameObject.CompareTag("Gyser")){
            StartCoroutine("Rise");
            StartCoroutine(camMov.Moved());
        }
    }

    private IEnumerator Rise(){
        while((yPos - transform.position.y) > 0.01f){
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            yield return new WaitForSeconds(0.0001f);
            StartCoroutine(camMov.Moved());
        }
        glider.SetActive(true);
        while(transform.position.y > 0.0001f){
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(camMov.Moved());
        }
        glider.SetActive(false);
        yield return null;
    }

    void Update(){
        if(Vector3.Distance(transform.position, camMov.transform.position) < 11 && !dead){
            dead = true;
            GameObject.Find("AdManager").GetComponent<AdManager>().camDeath = true;
        }

        if(dead){
            score.SetActive(false);
            move.canMove = false;
            DeathPrompt.SetActive(true);
            camMov.canUpdate = false;
            if(respawned){
                RespawnPrompt.SetActive(false);
            }
            dead = false;
        }

        if(transform.position.z >= 14 && boat || transform.position.z <= -14 && boat){
            transform.parent = null;
            boat = false;
            anim.SetTrigger("Drown");
        }
    }

    void OnTriggerStay(Collider col){
        if(col.gameObject.CompareTag("Boat")){
            if(transform.position.z > 14 || transform.position.z < -14){
                dead = true;
            }
            StartCoroutine(camMov.Moved());
        }
    }

    public void UnDie(){
        dead = false;
        respawned = true;
        score.SetActive(true);
        //
    }

    public void Continue(bool menu){
        GameObject.Find("MoneyManager").GetComponent<Money>().AddMoney(gameObject.GetComponent<Score>().score);
        DeathPrompt.SetActive(false);
        animScreen.SetTrigger("Dead");
        if(menu){
            sceneLoader.menu = true;
        }
    }
}
