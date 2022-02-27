using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioSource sound;

    public void Play(){
        sound.pitch = 1;
        sound.pitch = Random.Range(sound.pitch - 0.2f, sound.pitch + 0.2f);
        sound.Play();
    }
}
