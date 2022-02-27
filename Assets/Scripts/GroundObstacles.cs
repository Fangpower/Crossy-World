using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObstacles : MonoBehaviour
{
    [SerializeField] float spawnHeight;
    [SerializeField] bool spawnObstacles;
    [SerializeField] GameObject[] obstacles;
    [SerializeField] GameObject[] rare;
    [SerializeField] int rarityOne;
    [SerializeField] GameObject[] superRare;
    [SerializeField] int rarityTwo;
    List <int> points = new List<int>();

    void Start(){
        if(spawnObstacles){
            for(int i = -12; i < 12; i+=2){
                points.Add(i);
            }
            int obCount = Random.Range(0, 4);
            for(int i = 0; i < obCount; i++){
                int spawnZ = points[Random.Range(0, points.Count)];
                points.Remove(spawnZ);
                if(Random.Range(0, rarityTwo) == 1){
                    var temp = Instantiate(superRare[Random.Range(0, superRare.Length)], new Vector3(transform.position.x, transform.position.y+spawnHeight, spawnZ), Quaternion.identity);
                    temp.transform.position -= new Vector3(1, 0, 1);
                    temp.transform.parent = gameObject.transform;
                }else if(Random.Range(0, rarityOne) == 1){
                    var temp = Instantiate(rare[Random.Range(0, rare.Length)], new Vector3(transform.position.x, transform.position.y+spawnHeight, spawnZ), Quaternion.identity);
                    temp.transform.parent = gameObject.transform;
                } else{
                    var temp = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(transform.position.x, transform.position.y+spawnHeight, spawnZ), Quaternion.identity);
                    temp.transform.parent = gameObject.transform;
                }
            }
        }
    }
}
