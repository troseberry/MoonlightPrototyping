using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirectionChanger : MonoBehaviour
{
    public int tileNumber;
    public bool changeSegmentDirection;
    public int segmentsPassedThru;

    public Vector2 tileDirection = Vector2.zero;
    public SegmentController currentPassingSegment;

    public TileDirectionChanger top;
    public TileDirectionChanger right;
    public TileDirectionChanger bottom;
    public TileDirectionChanger left;



    void Start()
    {
        // top = this;
        // right = this;
        // bottom = this;
        // left = this;

        Invoke("SetSurroundingTiles", 1f);
    }

    void SetSurroundingTiles()
    {
        top = (SnakeController.Instance.GetTopTile(tileNumber) != null) ? SnakeController.Instance.GetTopTile(tileNumber) : this;

        right = (SnakeController.Instance.GetRightTile(tileNumber) != null) ? SnakeController.Instance.GetRightTile(tileNumber) : this;

        bottom = (SnakeController.Instance.GetBottomTile(tileNumber) != null) ? SnakeController.Instance.GetBottomTile(tileNumber) : this;

        left = (SnakeController.Instance.GetLeftTile(tileNumber) != null)? SnakeController.Instance.GetLeftTile(tileNumber) : this;
    }
    
    void Update()
    {
        
    }

    public void ActivateDirectionChanging(Vector2 dir)
    {
        changeSegmentDirection = true;
        tileDirection = dir;

        SegmentController hedSeg = SnakeController.Instance.GetHeadSegment();

        currentPassingSegment = hedSeg;
        TriggerSegmentDirChange();

        hedSeg.transform.position = transform.position;

        // if (dir == Vector2.up)
        // {
        //     hedSeg.transform.position = top.transform.position;
        // }
        // else if ( dir == Vector2.right)
        // {
        //     hedSeg.transform.position = right.transform.position;
        // }
        // else if ( dir == Vector2.down)
        // {
        //     hedSeg.transform.position = bottom.transform.position;
        // }
        // else if ( dir == Vector2.left)
        // {
        //     hedSeg.transform.position = left.transform.position;
        // }


        
    }

    void TriggerSegmentDirChange()
    {
        currentPassingSegment.SetMoveDirection(tileDirection);
        segmentsPassedThru++;
    }

    void ResetAfterAllSegmentsPass()
    {
        //if snake controller's total segments count == segmentsPassedThru
        if (segmentsPassedThru == SnakeController.Instance.GetTotalSegmentCount())
        {
            changeSegmentDirection = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.gameObject.name.Equals("Head"))
        {
            // Debug.Log("Head Tile obj: " + name);
            SnakeController.Instance.SetCurrentHeadTile(this);
            
        }

        if (changeSegmentDirection)
        {
            Debug.Log("other seg obj: " + other.name);
            currentPassingSegment = other.gameObject.GetComponent<SegmentController>(); 
            TriggerSegmentDirChange();
            SnakeController.Instance.SetSegmentPositions();
            // other.transform.position = transform.position;
            Debug.Break();
        } 
    }
}
