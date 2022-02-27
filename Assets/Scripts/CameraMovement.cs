using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float time;
    [SerializeField] float speed;
    [SerializeField] float increaseSpeed;
    [SerializeField] float increaseTime;
    [SerializeField] float offSetX;
    [SerializeField] float offSetZ;
    [SerializeField] float offSetY;

    public  bool canUpdate;

    private Vector3 newCamPos;

    void Update(){
        newCamPos = transform.position;
        newCamPos.z = player.transform.position.z - offSetZ;
        newCamPos.y = player.transform.position.y + offSetY;
        if(player.transform.position.x > newCamPos.x + offSetX){
            newCamPos.x = player.transform.position.x - offSetX;
        }

        if(canUpdate){
            transform.position = new Vector3(transform.position.x + (increaseSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            increaseSpeed += increaseTime * Time.deltaTime;
        }
    }

    public IEnumerator Moved(){
        float elapsedTime = 0;
        while(elapsedTime < time){
            transform.position = Vector3.Lerp(transform.position, newCamPos, 2 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }

    public IEnumerator Restart(){
        print(transform.position);
        transform.position = new Vector3(player.transform.position.x - offSetX, player.transform.position.y + offSetY, player.transform.position.z - offSetZ);
        print(transform.position);
        yield return new WaitForEndOfFrame();
    }
}
