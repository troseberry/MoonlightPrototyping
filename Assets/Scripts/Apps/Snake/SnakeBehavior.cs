using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehavior : MonoBehaviour
{
    public static SnakeBehavior Instance;

    private bool hasStarted = false;

    private Vector2 currentHeadDirection = Vector2.zero;

    public GameObject bodySegmentPrefab;

    public HeadController headSegment;
    private Vector2 lastHeadPosition;
    public List<BodyController> bodySegments;

    private bool canInput = true;

    public SwipeDetection swipeArea;
    
    void Start()
    {
        Instance = this; 
        swipeArea = GameObject.Find("SnakeSwipeArea").GetComponent<SwipeDetection>();  
    }

    
    void Update()
    {
        if (!hasStarted)
        {
            switch (swipeArea.GetCurrentSwipeDirection())
            {
                case SwipeDirection.RIGHT:
                    currentHeadDirection = Vector2.right;
                    headSegment.InitiateMove();
                    headSegment.SetMoveDirection(currentHeadDirection);
                    
                    hasStarted = true;
                    break;
                case SwipeDirection.UP:
                    currentHeadDirection = Vector2.up;
                    headSegment.InitiateMove();
                    headSegment.SetMoveDirection(currentHeadDirection);
                    
                    hasStarted = true;
                    break;
                case SwipeDirection.DOWN:
                    currentHeadDirection = Vector2.down;
                    headSegment.InitiateMove();
                    headSegment.SetMoveDirection(currentHeadDirection);
                    
                    hasStarted = true;
                    break;
            }
        }

        if (canInput)
        {
            switch (swipeArea.GetCurrentSwipeDirection())
            {
                case SwipeDirection.RIGHT:
                    if (currentHeadDirection == Vector2.right || currentHeadDirection == Vector2.left) break;
                    currentHeadDirection = Vector2.right;
                    headSegment.SetMoveDirection(currentHeadDirection);
                    canInput = false;
                    break;
                case SwipeDirection.LEFT:
                    if (currentHeadDirection == Vector2.right || currentHeadDirection == Vector2.left) break;
                    currentHeadDirection = Vector2.left;
                    headSegment.SetMoveDirection(currentHeadDirection);
                    canInput = false;
                    break;
                case SwipeDirection.UP:
                    if (currentHeadDirection == Vector2.up || currentHeadDirection == Vector2.down) break;
                    currentHeadDirection = Vector2.up;
                    headSegment.SetMoveDirection(currentHeadDirection);
                    canInput = false;
                    break;
                case SwipeDirection.DOWN:
                    if (currentHeadDirection == Vector2.up || currentHeadDirection == Vector2.down) break;
                    currentHeadDirection = Vector2.down;
                    headSegment.SetMoveDirection(currentHeadDirection);
                    canInput = false;
                    break;
            }
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
