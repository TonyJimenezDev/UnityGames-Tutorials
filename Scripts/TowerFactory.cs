using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Tower towerPrefab;
    [SerializeField] GameObject putTowerInHere;

    [Header("Limits")]
    [SerializeField] int towerLimit = 6;
    [SerializeField] float timer = 2f;
    [SerializeField] float maxTime = 2f;

    Queue<Tower> towerQueue = new Queue<Tower>();

    private void Update()
    {  
       timer -= Time.deltaTime;
    }

    public void AddTower(Waypoint baseWaypoint)
     {
        int numTowers = towerQueue.Count;
        if(numTowers < towerLimit)
        {
            PoppingNewTower(baseWaypoint);
        }       
         if (timer <= 0f && numTowers == towerLimit)
         {
            MoveExistingTower(baseWaypoint);
            timer = maxTime;
        }
     }

    private void PoppingNewTower(Waypoint baseWaypoint)
    {
        var minus = new Vector3(transform.position.x, -5f, transform.position.z);
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position + minus, Quaternion.identity);

        newTower.transform.parent = putTowerInHere.transform;
        baseWaypoint.isPlaceable = false; // Can't place anymore bc this changes to false
        newTower.baseWaypoint = baseWaypoint;
        towerQueue.Enqueue(newTower);
    }

     private void MoveExistingTower(Waypoint baseWaypoint)
    {
        var minus = new Vector3(transform.position.x, -5f, transform.position.z);
        var oldTower = towerQueue.Dequeue(); // Removes old tower off the queue list

        oldTower.baseWaypoint.isPlaceable = true; // lets you set stuff on the block
        baseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = baseWaypoint;
        oldTower.transform.position = baseWaypoint.transform.position + minus;
        towerQueue.Enqueue(oldTower);
    }
}
