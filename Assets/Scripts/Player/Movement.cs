using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Vector3 targetPos = new Vector3(0, 0, 0);
    [SerializeField] Rigidbody rb;
    [SerializeField] CameraMovement camMov;
    [SerializeField] GroundSpawning gs;
    [SerializeField] GameObject body;
    [SerializeField] AudioSource hop;
    [SerializeField] Animator anim;

    [SerializeField] LayerMask obstacle;
    [SerializeField] LayerMask boat;

    [SerializeField] float speed;
    [SerializeField] float yPos;
    public bool canMove=false;
    private bool allowMove = true;
    Transform safeBoatPoint;

    public void Forward(InputAction.CallbackContext context){
        if(context.performed && canMove){
            hop.pitch = 1;
            if(!CheckForBlock(new Vector3(1, 0, 0), 2) && allowMove){
                GetComponent<Death>().boat = CheckForBoat(new Vector3(1, 0, 0), 2, true);
                targetPos += new Vector3(2, 0, 0);
                StartCoroutine(camMov.Moved());
                gs.AddNew();
                body.transform.rotation = Quaternion.Euler(0, 0, 0);
                hop.pitch = Random.Range(hop.pitch - 0.2f, hop.pitch + 0.2f);
                hop.Play();
                StartCoroutine("WaitToMove");
            }
        }
    }

    public void Backward(InputAction.CallbackContext context){
        if(context.performed && canMove){
            hop.pitch = 1;
            if(!CheckForBlock(new Vector3(-1, 0, 0), 2) && allowMove){
                GetComponent<Death>().boat = CheckForBoat(new Vector3(-1, 0, 0), 2, true);
                targetPos += new Vector3(-2, 0, 0);
                body.transform.rotation = Quaternion.Euler(0, 180, 0);
                hop.pitch = Random.Range(hop.pitch - 0.2f, hop.pitch + 0.2f);
                hop.Play();
                StartCoroutine("WaitToMove");
            }
        }
    }

    public void Left(InputAction.CallbackContext context){
        if(context.performed && canMove){
            hop.pitch = 1;
            if(!CheckForBlock(new Vector3(0, 0, 2), 2) && allowMove){
                targetPos.z = Mathf.Clamp(transform.position.z + 2, -12, 12);
                //GetComponent<Death>().boat = CheckForBoat(new Vector3(0, 5, 0), 2, false);
                StartCoroutine(camMov.Moved());
                body.transform.rotation = Quaternion.Euler(0, 270, 0);

                if(GetComponent<Death>().boat){
                    Transform[] points = transform.parent.GetComponent<SideMovement>().safePoints;
                    safeBoatPoint = FindSidePoints(points, 1);
                }
                hop.pitch = Random.Range(hop.pitch - 0.2f, hop.pitch + 0.2f);
                hop.Play();
                StartCoroutine("WaitToMove");
            }
        }
    }

    public void Right(InputAction.CallbackContext context){
        if(context.performed && canMove){
            hop.pitch = 1;
            if(!CheckForBlock(new Vector3(0, 0, -2), 2) && allowMove){
                targetPos.z = Mathf.Clamp(transform.position.z - 2, -12, 12);
                //GetComponent<Death>().boat = CheckForBoat(new Vector3(0, 5, 0), 2, false);
                StartCoroutine(camMov.Moved());
                body.transform.rotation = Quaternion.Euler(0, 90, 0);

                if(GetComponent<Death>().boat){
                    Transform[] points = transform.parent.GetComponent<SideMovement>().safePoints;
                    safeBoatPoint = FindSidePoints(points, -1);
                }
                hop.pitch = Random.Range(hop.pitch - 0.2f, hop.pitch + 0.2f);
                hop.Play();
                StartCoroutine("WaitToMove");
            }
        }
    }

    void Update()
    {
        if(GetComponent<Death>().boat){
            targetPos = new Vector3(targetPos.x, targetPos.y, safeBoatPoint.position.z);
        } else{
            targetPos = new Vector3(targetPos.x, transform.position.y, ConfirmEven(targetPos.z));
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
    }

    private IEnumerator WaitToMove(){
        allowMove = false;
        yield return new WaitForSeconds(0.1f);
        allowMove = true;
    }

    //This function checks to see if the player can move forward.
    bool CheckForBlock(Vector3 dir, int length){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(dir), out hit, length, obstacle)){
            return true;
        } else {
            return false;
        }
    }

    //This checks to see if the player will land on a boat or die.
    bool CheckForBoat(Vector3 dir, int length, bool forward){
        RaycastHit hit;
        Ray ray;
        if(!forward){
            ray = new Ray(transform.position - new Vector3(0, 5, 0), transform.TransformDirection(dir));
        }else {
            ray = new Ray(transform.position, transform.TransformDirection(dir));
        }
        
        if(Physics.Raycast(ray, out hit, length, boat)){
            transform.parent = hit.transform;
            Transform[] safePoints = transform.parent.GetComponent<SideMovement>().safePoints;
            safeBoatPoint = FindSafePoint(safePoints);
            targetPos = new Vector3(targetPos.x, targetPos.y, safeBoatPoint.position.z);
            return true;
        }else {
            transform.parent = null;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            return false;
        }
    }

    Transform FindSafePoint(Transform[] safePoints){
        Transform pos = null;
        float safePoint = 10000;
        foreach(Transform point in safePoints) {
            if(Vector3.Distance(transform.position, point.position) < safePoint){
                safePoint = point.position.z;
                pos = point;
            }
        }
        return pos;
    }

    Transform FindSidePoints(Transform[] safePoints, float dir){
        Transform pos = null;
        float safePoint = 10000;
        foreach(Transform point in safePoints) {
            if(point.position == safeBoatPoint.position){
                continue;
            }else if(dir == 1){
                if(point.position.z > transform.position.z){
                    safePoint = point.position.z;
                    pos = point;
                }
            }else if(dir == -1){
                if(point.position.z < transform.position.z){
                    safePoint = point.position.z;
                    pos = point;
                }
            }
        }
        if(pos == null){
            return safeBoatPoint;
        }else{
            return pos;
        } 
    }

    float ConfirmEven(float targetPos){
        float tempNum = targetPos;
        tempNum = Mathf.Round(tempNum);
        if(tempNum % 2 != 0){
            tempNum -= 1;
        }
        return tempNum;
    }
}
//////WHyw
