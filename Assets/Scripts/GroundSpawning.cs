using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawning : MonoBehaviour
{
    [SerializeField] float renderDistance;
    [SerializeField] float currentX = 2;

    [SerializeField] Transform player;
    [SerializeField] GameObject[] ground;
    [SerializeField] GameObject[] sky;
    float waterDir = 60;

    void Start(){
        Thirty();
    }

    public void AddNew(){
        // var tempSky = Instantiate(sky[Random.Range(0, sky.Length)], new Vector3(currentX, 39, 0), Quaternion.identity);
        // tempSky.transform.parent = transform;
        var temp = Instantiate(ground[Random.Range(0, ground.Length)], new Vector3(currentX, -1, 0), Quaternion.identity);
        temp.transform.parent = transform;
        if(temp.transform.tag == "Water"){
            waterDir *= -1;
            temp.transform.GetComponent<MovingObstacles>().spawnDir = waterDir;
        }
        currentX += 2;
    }

    public void Thirty(){
        for(int i = 0; i < renderDistance; i++){
            // var tempSky = Instantiate(sky[Random.Range(0, sky.Length)], new Vector3(currentX, 39, 0), Quaternion.identity);
            // tempSky.transform.parent = transform;
            var temp = Instantiate(ground[Random.Range(0, ground.Length)], new Vector3(currentX, -1, 0), Quaternion.identity);
            temp.transform.parent = transform;
            if(temp.transform.tag == "Water"){
                waterDir *= -1;
                temp.transform.GetComponent<MovingObstacles>().spawnDir = waterDir;
            }
            currentX += 2;
        }
    }
}
