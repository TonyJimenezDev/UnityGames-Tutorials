using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    public bool isPlaceable = true;
    public Waypoint exploredFrom;

    const int grideSize = 10;
    Vector2Int gridPos;

    public int GetGridSize()
    {
        return grideSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / grideSize),            
        Mathf.RoundToInt(transform.position.z / grideSize));
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                print("Can't place a here");
                // Might want to add a popup warning in the future.
            }
        }
    }
}
