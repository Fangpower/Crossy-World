using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerObstacle : MonoBehaviour
{
    void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("Water") || col.gameObject.CompareTag("Animal")){
            GameObject.Destroy(gameObject);
        }else if(col.gameObject.CompareTag("Obstacle")){
            GameObject.Destroy(col.gameObject);
        }
    }
}
