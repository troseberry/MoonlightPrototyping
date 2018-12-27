using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentController : MonoBehaviour
{
    private Vector2 moveDirection;
    public TileDirectionChanger currentTile;
    public TileDirectionChanger previousTile;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(moveDirection * (1 / 30f));
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        previousTile = currentTile;
        currentTile = other.transform.GetComponent<TileDirectionChanger>();
    }
}
