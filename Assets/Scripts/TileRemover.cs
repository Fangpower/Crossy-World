using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRemover : MonoBehaviour
{
    void OnTriggerEnter (Collider col){
        if(!col.CompareTag( "Player")){
            GameObject.Destroy(col.gameObject);
        }
    }
}
