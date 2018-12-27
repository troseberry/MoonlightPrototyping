using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public static SnakeController Instance;

    private bool hasStarted = false;

    public Vector2 currentHeadDirection = Vector2.zero;

    public GameObject bodySegmentPrefab;

    public HeadController headSegment;
    private Vector2 lastHeadPosition;
    public List<BodyController> bodySegments;

    private bool canInput = true;

    
    void Start()
    {
        Instance = this;   
    }

    
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                currentHeadDirection = Vector2.right;
                headSegment.InitiateMove();
                headSegment.SetMoveDirection(currentHeadDirection);
                
                hasStarted = true;
            }
        }

        if (canInput)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (currentHeadDirection == Vector2.right || currentHeadDirection == Vector2.left) return;

                if (Input.GetAxis("Horizontal") > 0)
                {   // Debug.Log("Right");
                    currentHeadDirection = Vector2.right;
                    headSegment.SetMoveDirection(currentHeadDirection);
                    canInput = false;
                }
                
                if (Input.GetAxis("Horizontal") < 0)
                {   // Debug.Log("Left");
                    currentHeadDirection = Vector2.left;
                    headSegment.SetMoveDirection(currentHeadDirection);
                    canInput = false;
                }
            }
            else if(Input.GetAxis("Vertical") != 0)
            {
                if (currentHeadDirection == Vector2.up || currentHeadDirection == Vector2.down) return;

                if (Input.GetAxis("Vertical") > 0)
                {   // Debug.Log("Up");
                    currentHeadDirection = Vector2.up;
                    headSegment.SetMoveDirection(currentHeadDirection);
                    canInput = false;
                }

                if (Input.GetAxis("Vertical") < 0)
                {   // Debug.Log("Down");
                    currentHeadDirection = Vector2.down;
                    headSegment.SetMoveDirection(currentHeadDirection);
                    canInput = false;
                }
            }
        }
    
        if (Input.GetKeyDown(KeyCode.E))
        {
            headSegment.EatFood();
        }
    }


    public void SetLastHeadPosition(Vector2 pos)
    {
        lastHeadPosition = pos;
    }

    public void MoveTailSegment()
    {
        BodyController tail = bodySegments[bodySegments.Count - 1];
        tail.SetPosition(lastHeadPosition);

        bodySegments.RemoveAt(bodySegments.Count - 1);
        bodySegments.Insert(0, tail);
    }

    public void AddBodySegment()
    {
        GameObject seg = Instantiate(bodySegmentPrefab, transform);
        seg.GetComponent<BodyController>().SetPosition(lastHeadPosition);
        bodySegments.Insert(0, seg.GetComponent<BodyController>());
    }

    public void EnableInput()
    {
        canInput = true;
    }


}
