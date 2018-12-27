﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    private Vector2 moveDirection;
    public bool isHeadSegment;

    public float moveDistance;

    private bool initiateMove = false;
    private bool startedMove = false;
    public bool stopMoving = false;

    private bool justAte = false;

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
            SnakeController.Instance.SetLastHeadPosition(transform.position);
            transform.Translate(moveDirection * moveDistance);

            if (!justAte)
            {
                SnakeController.Instance.MoveTailSegment();
            }
            else
            {
                SnakeController.Instance.AddBodySegment();
                justAte = false;
            }

            SnakeController.Instance.EnableInput();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("SnakeFood"))
        {
            Debug.Log("Hit Food");
            justAte = true;
            Destroy(other.gameObject);
            FoodSpawner.Instance.SpawnFood();
        }
    }
}
