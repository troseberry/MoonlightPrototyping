using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public static SnakeController Instance;

    public Vector2 currentHeadDirection = Vector2.zero;
    private Vector2 currentTailDirection;

    public TileDirectionChanger currentHeadTile;

    public int totalSegmentCount;

    private bool hasStarted = false;
    public SegmentController[] startingSegments;

    public TileDirectionChanger[] allTiles;

    
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
                foreach(SegmentController seg in startingSegments)
                {
                    currentHeadDirection = Vector2.right;
                    seg.SetMoveDirection(Vector2.right);
                }
                hasStarted = true;
            }
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (currentHeadDirection == Vector2.right || currentHeadDirection == Vector2.left) return;

            if (Input.GetAxis("Horizontal") > 0)
            {
                currentHeadDirection = Vector2.right;
                // startingSegments[0].SetMoveDirection(currentHeadDirection);
                currentHeadTile.ActivateDirectionChanging(Vector2.right);
                Debug.Log("Right");
            }
            
            if (Input.GetAxis("Horizontal") < 0)
            {
                currentHeadDirection = Vector2.left;
                // startingSegments[0].SetMoveDirection(currentHeadDirection);
                currentHeadTile.ActivateDirectionChanging(Vector2.left);
                Debug.Log("Left");
            }
        }
        else if(Input.GetAxis("Vertical") != 0)
        {
            if (currentHeadDirection == Vector2.up || currentHeadDirection == Vector2.down) return;

            if (Input.GetAxis("Vertical") > 0)
            {
                currentHeadDirection = Vector2.up;
                // startingSegments[0].SetMoveDirection(currentHeadDirection);
                currentHeadTile.ActivateDirectionChanging(Vector2.up);
                Debug.Log("Up");
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                currentHeadDirection = Vector2.down;
                // startingSegments[0].SetMoveDirection(currentHeadDirection);
                currentHeadTile.ActivateDirectionChanging(Vector2.down);
                Debug.Log("Down");
            }
        }
    }

    public void SetSegmentPositions()
    {
        startingSegments[1].transform.position = startingSegments[0].previousTile.transform.position;
        startingSegments[2].transform.position = startingSegments[1].previousTile.transform.position;
    }

    public int GetTotalSegmentCount() { return totalSegmentCount; }

    public void SetCurrentHeadTile(TileDirectionChanger headTile)
    {
        currentHeadTile = headTile;
    }

    public SegmentController GetHeadSegment()
    {
        return startingSegments[0];
    }

    public TileDirectionChanger GetTopTile(int currTileNumber)
    {
        for (int i = 0; i < allTiles.Length; i++)
        {
            if (allTiles[i].tileNumber == currTileNumber - 9) return allTiles[i];
        }
        return null;
    }

    public TileDirectionChanger GetBottomTile(int currTileNumber)
    {
        for (int i = 0; i < allTiles.Length; i++)
        {
            if (allTiles[i].tileNumber == currTileNumber + 9) return allTiles[i];
        }
        return null;
    }

    public TileDirectionChanger GetLeftTile(int currTileNumber)
    {
        for (int i = 0; i < allTiles.Length; i++)
        {
            if (allTiles[i].tileNumber == currTileNumber - 1) return allTiles[i];
        }
        return null;
    }

    public TileDirectionChanger GetRightTile(int currTileNumber)
    {
        for (int i = 0; i < allTiles.Length; i++)
        {
            if (allTiles[i].tileNumber == currTileNumber + 1) return allTiles[i];
        }
        return null;
    }
}
