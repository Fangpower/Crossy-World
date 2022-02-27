using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovement : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    [SerializeField] Animator anim;
    public Transform[] safePoints;
    public float dir;
    public float speed;

    void Start(){
        if(dir < 0){
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Update()
    {
        if(transform.position.z > 25 || transform.position.z < -25){
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (speed * 30 * dir * Time.deltaTime));
        } else {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (speed * dir * Time.deltaTime));
        }
        if(transform.position.z > 65 || transform.position.z < -65){
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player" && sound != null){
            sound.pitch = 1;
            sound.pitch = Random.Range(sound.pitch - 0.3f, sound.pitch + 0.3f);
            sound.Play();

            if(anim != null){
                anim.SetTrigger("Player");
            }
        }
    }
}
