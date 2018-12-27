using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SwipeDirection { LEFT, RIGHT, UP, DOWN, NONE }

public class SwipeDetection : MonoBehaviour
{
    private SwipeDirection currentSwipeDirection = SwipeDirection.NONE; 

    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.EndDrag;
        entry.callback.AddListener((data) => { DetectSwipe((PointerEventData)data); });
        trigger.triggers.Add(entry); 
    }


    public void DetectSwipe(PointerEventData data)
    {
        Vector2 startPos = data.pressPosition;
        Vector2 endPos = data.position;

        Vector2 swipeDistance = (endPos - startPos).normalized;
        // Debug.Log("Dir: " + swipeDistance);

        currentSwipeDirection = DetermineSwipeDirection(swipeDistance);
    }

    SwipeDirection DetermineSwipeDirection(Vector2 swipe)
    {
        if (swipe.x > 0.8f)
        {
            return SwipeDirection.RIGHT;
        }
        else if (swipe.x < -0.8f)
        {
            return SwipeDirection.LEFT;
        }
        else if (swipe.y > 0.8f)
        {
            return SwipeDirection.UP;
        }
        else if (swipe.y < -0.8f)
        {
            return SwipeDirection.DOWN;
        }

        return SwipeDirection.NONE;
    }

    public SwipeDirection GetCurrentSwipeDirection()
    {
        SwipeDirection output = currentSwipeDirection;
        currentSwipeDirection = SwipeDirection.NONE;

        // Debug.Log("Swipe Dir: " + output);
        return output;
    }
}
