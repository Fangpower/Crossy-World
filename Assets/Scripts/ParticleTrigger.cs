using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] string collName;
    
    void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag(collName)){
            particles.Play();
        }
    }
}
