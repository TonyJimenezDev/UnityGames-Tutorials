using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyMovement : MonoBehaviour
{
    [Header("Transition")]
    [SerializeField] float transition = 1f;
    [SerializeField] float waitTime = .1f;
    [SerializeField] ParticleSystem EnemyDamageExplosion;

    void Start()
    {
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));       
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            float speed = Random.Range(2f, 5f);
            while (Vector3.Distance(transform.position, waypoint.transform.position) > .02f)
            {
                transform.position = Vector3.Lerp(transform.position, waypoint.transform.position, transition * Time.deltaTime * speed);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);           
        }
        var enemyDamageExplosion = Instantiate(EnemyDamageExplosion, transform.position, Quaternion.identity);
        enemyDamageExplosion.Play();
        float destroyDelay = enemyDamageExplosion.main.duration;
        Destroy(enemyDamageExplosion.gameObject, destroyDelay);
        Destroy(gameObject);
    }   
}

  
