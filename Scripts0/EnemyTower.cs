using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    [Header("Lookat & Fire")]
    [SerializeField] Transform objectToPanFriendly;    
    [SerializeField] ParticleSystem projectileParticleEnemy;
    [SerializeField] float towerAttackRangeEnemy = 10f;

    Transform targetFriendly;
    Transform targetFriendlyTower;

    private void Start()
    {
        EnemyShoots(false);
    }

    void Update ()
    {
        SetTargetFriendly(); 
        SetTargetFriendlyTower();

        if (targetFriendlyTower)
        {
            ReadyAim();
        }  else { return; }
    }

    private void SetTargetFriendly()
    {
        var sceneFriendly = FindObjectsOfType<FriendlyDamage>();
        if (sceneFriendly.Length == 0) { return; } // IF no enemies, just return

        Transform closestFriendly = sceneFriendly[0].transform;
        foreach (FriendlyDamage testFriendly in sceneFriendly)
        {
            closestFriendly = GetClosest(closestFriendly, testFriendly.transform);
        }
        targetFriendly = closestFriendly;
    }
    // Used a script to Aim the cannon at the corret spot for collision
    private void SetTargetFriendlyTower()
    {
        var sceneFriendlyTower = FindObjectsOfType<PlaceHolderFriendlyDeath>();
        if(sceneFriendlyTower.Length == 0) { return; } // If no Tower friendly, return

        Transform ClosestFriendlyTower = sceneFriendlyTower[0].transform;
        foreach (PlaceHolderFriendlyDeath testTower in sceneFriendlyTower)
        {
            ClosestFriendlyTower = GetClosest(ClosestFriendlyTower, testTower.transform );
        }
        targetFriendlyTower = ClosestFriendlyTower;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distanceTransformA = Vector3.Distance(transform.position, transformA.position);
        float distanceTransformB = Vector3.Distance(transform.position, transformB.position);

        if (distanceTransformA < distanceTransformB)
        {
            return transformA;
        }
        return transformB;
    }

    private void ReadyAim()
    {
        float distanceBetweenFriendly = Vector3.Distance(targetFriendly.transform.position, gameObject.transform.position);
        float distanceBetweenFriendlyTower = Vector3.Distance(targetFriendlyTower.transform.position, gameObject.transform.position);

        if (distanceBetweenFriendly <= distanceBetweenFriendlyTower && distanceBetweenFriendly < towerAttackRangeEnemy)
        {
                objectToPanFriendly.LookAt(targetFriendly);
                FireAtFriendly();                     
            // Sound Effects for the firing even visionals. Could pull it as a  function
        }
        else
        {
                objectToPanFriendly.LookAt(targetFriendlyTower);
                FireAtFriendly();
                // Sound Effects for the firing even visionals. Could pull it as a  function    
        }   
    }

    private void FireAtFriendly()
    {
        float distanceToFriendly = Vector3.Distance(targetFriendly.transform.position, gameObject.transform.position);
        float distancetoFriendlyTower = Vector3.Distance(targetFriendlyTower.transform.position, gameObject.transform.position);

        if (distanceToFriendly <= towerAttackRangeEnemy || distancetoFriendlyTower <= towerAttackRangeEnemy)
        {   
            EnemyShoots(true);
        }
        else
        {  
            EnemyShoots(false);
        }
    }

    private void EnemyShoots(bool IsFriendlyActive)
    {
        ParticleSystem.EmissionModule eModule = projectileParticleEnemy.emission;
        eModule.enabled = IsFriendlyActive;
    }
}
