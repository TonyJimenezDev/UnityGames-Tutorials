using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder2 : MonoBehaviour {

    [SerializeField] Waypoint startWaypoint;
    [SerializeField] Waypoint endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid2 = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue2 = new Queue<Waypoint>();

    Waypoint searchCenter; 
    public List<Waypoint> path2 = new List<Waypoint>(); // TODO make private
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
    public List<Waypoint> GetPath2()
    {
        if (path2.Count == 0)
        {
            CalculatePath2();
        }
        return path2;
    }


    private void CalculatePath2()
    {
        LoadCubes();
        BreadthFirstSearch();
        CreatePath2();
    }

    private void CreatePath2()
    {
        SetAsPath2(endWaypoint);
        Waypoint prev = endWaypoint.exploredFrom;

        while (prev != startWaypoint)
        {
            SetAsPath2(prev);
            prev = prev.exploredFrom;

        }
        SetAsPath2(startWaypoint);
        path2.Reverse();
    }

    private void SetAsPath2(Waypoint waypoint2)
    {
        path2.Add(waypoint2);
        waypoint2.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue2.Enqueue(startWaypoint);
        while (queue2.Count > 0 && isRunning)
        {
            searchCenter = queue2.Dequeue();
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
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoords = searchCenter.GetGridPos() + direction;
            if (grid2.ContainsKey(neighbourCoords))
            {
                QueueNewNeighbour(neighbourCoords);
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoords)
    {
        Waypoint neighbour = grid2[neighbourCoords];
        if (neighbour.isExplored || queue2.Contains(neighbour))
        {
            //Do Nothing
        }
        else
        {
            queue2.Enqueue(grid2[neighbourCoords]);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void LoadCubes()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint2 in waypoints)
        {
            var gridPos = waypoint2.GetGridPos();

            if (grid2.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping Block " + waypoint2);
            }
            else
            {
                grid2.Add(gridPos, waypoint2);
            }
        }
    }
}
