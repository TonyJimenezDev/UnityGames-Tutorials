using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint; 
    [SerializeField] Waypoint endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    Waypoint searchCenter;
    public List<Waypoint> path = new List<Waypoint>(); // TODO make private
    bool isRunning = true;

    Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left,
            new Vector2Int(1,1), //Up Right
            new Vector2Int(1,-1), // Up Left
            new Vector2Int(-1, 1), //Down Right
            new Vector2Int(-1, -1) // Down Left
        };
    // Process this in this order
    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;  
    }

    private void CalculatePath()
    {
        LoadCubes();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);
        Waypoint prev = endWaypoint.exploredFrom;

        while (prev != startWaypoint)
        {
            SetAsPath(prev);
            prev = prev.exploredFrom;
        }
            SetAsPath(startWaypoint);
            path.Reverse();                  
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);       
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            StopIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void StopIfEndFound()
    {
        if (searchCenter == endWaypoint) 
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if(!isRunning) { return;  } 

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoords = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoords))
            {
                QueueNewNeighbour(neighbourCoords);
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoords)
    {
        Waypoint neighbour = grid[neighbourCoords];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //Do Nothing
        }
        else
        {
            queue.Enqueue(grid[neighbourCoords]);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void LoadCubes()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping Block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);               
            }
        }
    }
}
