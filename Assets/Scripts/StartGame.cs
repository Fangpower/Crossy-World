using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    public CameraMovement camMove;
    public Movement move;
    public PlayerInput input;
    public bool canStart = false;

    void Start(){
        StartCoroutine("Intro");
    }

    public void Begin(InputAction.CallbackContext context){
        if(canStart && context.performed){
            camMove.canUpdate = true;
            canStart = false;
            GameObject.Destroy(this);
        }
    }

    private IEnumerator Intro() {
        yield return new WaitForSeconds(4);
        canStart = true;
        move.enabled = true;
    }
}
