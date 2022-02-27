using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    [SerializeField] Vector2 speed;
    [SerializeField] float spawnHeight;
    [SerializeField] bool alternate;
    float permSpeed;
    public float spawnDir;

    void Start()
    {
        if(!alternate){
            int[] dirs = new int[]{-1, 1};
            spawnDir = 60 * dirs[Random.Range(0, dirs.Length)];
        }
        StartCoroutine("Spawn");
        permSpeed = Random.Range(speed.x, speed.y);
    }

    private IEnumerator Spawn(){
        while(true){
            var temp = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(transform.position.x, transform.position.y+spawnHeight, spawnDir), Quaternion.identity);
            temp.GetComponent<SideMovement>().dir = spawnDir/60;
            temp.GetComponent<SideMovement>().speed = permSpeed;
            temp.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(Random.Range(2.5f, 5f));
        }
    }
}
