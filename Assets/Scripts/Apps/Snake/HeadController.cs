using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    private Vector2 moveDirection;

    public float moveDistance;

    private bool initiateMove = false;
    private bool startedMove = false;
    public bool stopMoving = false;

    private bool justAte = false;

    public Transform headDeco;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (initiateMove && !startedMove) StartCoroutine(MoveHead());
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;

        if (moveDirection ==  Vector2.right)
        {
            headDeco.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (moveDirection ==  Vector2.up)
        {
            headDeco.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (moveDirection ==  Vector2.left)
        {
            headDeco.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else if (moveDirection ==  Vector2.down)
        {
            headDeco.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
    }

    public void InitiateMove()
    {
        initiateMove = true;
    }

    public void EndMove()
    {
        stopMoving = true;
    }

    public void EatFood()
    {
        justAte = true;
    }

    IEnumerator MoveHead()
    {
        if (!startedMove) startedMove = true;

        while (!stopMoving)
        {
            SnakeBehavior.Instance.SetLastHeadPosition(transform.position);
            transform.Translate(moveDirection * moveDistance);

            if (!justAte)
            {
                SnakeBehavior.Instance.MoveTailSegment();
            }
            else
            {
                SnakeBehavior.Instance.AddBodySegment();
                justAte = false;
            }

            SnakeBehavior.Instance.EnableInput();
            yield return new WaitForSeconds(0.4f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("SnakeFood"))
        {
            // Debug.Log("Hit Food");
            justAte = true;
            Destroy(other.gameObject);
            FoodSpawner.Instance.SpawnFood();
        }
        else if (other.tag.Equals("SnakeBoundary"))
        {
            // Debug.Log("Hit Grid Boundary");
            stopMoving = true;
            SnakeAppController.Instance.ToggleGameOverMenu();
        }
        else if (other.tag.Equals("SnakeBody"))
        {
            // Debug.Log("Hit snake body");
            stopMoving = true;
            SnakeAppController.Instance.ToggleGameOverMenu();
        }
    }
}
