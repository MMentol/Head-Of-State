using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public Rigidbody body;
    public Animator anim;
    public BoxCollider boxCollider;
    private float horizontalInput;
    private float verticalInput;

    private Vector2 position;
    private List<Vector3> pathVectorList;
    private int currentPathIndex;

    public HumanPathfinding humanPathfinding;

    private const float speed = 1f;

    public int randomMoveDistance = 5;

    [Range(0, 100)]
    public int testx;
    [Range(0, 100)]
    public int testy;



    // Start is called before the first frame update
    void Start()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        humanPathfinding = GetComponent<HumanPathfinding>();
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        //unitSkeleton.Update(Time.deltaTime);


        //if (Input.GetMouseButtonDown(0))
        //{
            
        //    //Debug.Log(UtilsClass.GetMouseWorldPosition() + " 38");
        //    Vector3 mospos = UtilsClass.GetMouseWorldPosition();
        //    //SetTargetPosition(mospos);
        //    //Debug.Log(mospos + " 39");
        //    SetTargetPosition(new Vector3(testx, testy));
        //}

    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = humanPathfinding.FindPath(GetPosition(), new Vector2(targetPosition.x,targetPosition.z));
        //Debug.Log(GetPosition() + " , " + targetPosition);
        //Debug.Log("VectorList: " + pathVectorList);
        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            
            pathVectorList.RemoveAt(0);
        }
    }

    private void HandleMovement()
    {
        //Debug.Log("Moving?");
        if (pathVectorList != null)
        {
            //Debug.Log("not null");
            Vector3 tilexy = pathVectorList[currentPathIndex] - new Vector3(0.5f, 0.5f ,0.5f);
            Vector3 targetPosition = new Vector3(tilexy.x, 0 , tilexy.y);
            //Debug.Log("targetPosb4: " + targetPosition)
            anim.SetBool("isWalking", true);

            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                //Debug.Log("MoveDir: " + moveDir);
                //Debug.Log("targetPos: " +  targetPosition + " , " + transform.position);

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //animatedWalker.SetMoveVector(moveDir);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                Debug.Log("updating");
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();

                    if ((transform.position.x % 1) != 0)
                        transform.position = transform.position + new Vector3(1-transform.position.x%1,0);
                    if ((transform.position.z % 1) != 0)
                        transform.position = transform.position + new Vector3(0, 0, 1 - transform.position.z % 1);
                    //Debug.Log("final targetPos: " + targetPosition + " , " + transform.position);
                    if (transform.position.x < targetPosition.x)
                    {
                        transform.position = transform.position + new Vector3(1f, 0);
                        Debug.Log("movedforce");
                    }
                    if (transform.position.x > targetPosition.x)
                    {
                        transform.position = transform.position - new Vector3(1f, 0);

                        Debug.Log("movedforce");
                    }
                    if (transform.position.z < targetPosition.z)
                    {
                        transform.position = transform.position + new Vector3(0, 0, 1f);

                        Debug.Log("movedforce");
                    }
                    if (transform.position.z > targetPosition.z)
                    {
                        transform.position = transform.position - new Vector3(0, 0, 1f);

                        Debug.Log("movedforce");
                    }
                    //animatedWalker.SetMoveVector(Vector3.zero);
                }
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            //animatedWalker.SetMoveVector(Vector3.zero);
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
        anim.SetBool("isWalking", false);
    }

    public Vector2 GetPosition()
    {
        return new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
    }

    
}
