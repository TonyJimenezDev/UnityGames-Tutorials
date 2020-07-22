using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyMovement : MonoBehaviour
{
    [SerializeField] float waitTime = .1f;
    [SerializeField] ParticleSystem EnemyDamageExplosion;
    private float transition = 1f;

    void Start()
    {     
         PathFinder2 pathfinder2 = FindObjectOfType<PathFinder2>();
         var path2 = pathfinder2.GetPath2(); 
         StartCoroutine(FollowPath2(path2));
    }

     IEnumerator FollowPath2(List<Waypoint> path2)
       {
           foreach (Waypoint waypoint2 in path2)
           { 
               float speed = Random.Range(2f, 5f);
               while (Vector3.Distance(transform.position, waypoint2.transform.position) > .02f)
               {
                transform.position = Vector3.Lerp(transform.position, waypoint2.transform.position, transition * Time.deltaTime * speed);
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
