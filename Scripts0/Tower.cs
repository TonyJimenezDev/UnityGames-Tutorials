using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float towerAttackRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;

    Transform targetEnemy;
    Transform targetTower;
    public Waypoint baseWaypoint;

    void Update ()
    {
        SetTargetEnemy();
        SetTargetTower();
            
        if (targetEnemy != null)
        {
            ReadyAim();
        } else { return; } 
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; } // IF no enemies, just return

        Transform closestEnemy = sceneEnemies[0].transform;
        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
            
        }
        targetEnemy = closestEnemy;
    }

    private void SetTargetTower()
    {
        var sceneEnemyTowers = FindObjectsOfType<EnemyTower>();
        if(sceneEnemyTowers.Length == 0) { return; }

        Transform closestTower = sceneEnemyTowers[0].transform;
        foreach(EnemyTower testTower in sceneEnemyTowers)
        {
            closestTower = GetClosest(closestTower, testTower.transform);
        }
        targetTower = closestTower;
    }

    private Transform GetClosest(Transform transformOne, Transform transformTwo)
    {
        float distanceTransformOne = Vector3.Distance(transform.position, transformOne.position);
        float distanceTransforTwo = Vector3.Distance(transform.position, transformTwo.position);

        if(distanceTransformOne <= distanceTransforTwo)
        {
            return transformOne;
        }
        return transformTwo;
    }

    private void ReadyAim()
    {
        float distanceBetweenEnemies = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        float distanceBetweenTower = Vector3.Distance(targetTower.transform.position, gameObject.transform.position);

        if (distanceBetweenEnemies <= distanceBetweenTower && distanceBetweenEnemies < towerAttackRange)
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemyAtEnemies();
            // Sound Effects for the firing even visionals. Could pull it as a  function
        }
        else
        {
            objectToPan.LookAt(targetTower);
            FireAtEnemyAtEnemies();
            // Sound Effects for the firing even visionals. Could pull it as a  function
        }
    }

    private void FireAtEnemyAtEnemies()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        float distanceToTower = Vector3.Distance(targetTower.transform.position, gameObject.transform.position);

        if (distanceToEnemy <= towerAttackRange || distanceToTower <= towerAttackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        ParticleSystem.EmissionModule eModule = projectileParticle.emission;
        eModule.enabled = isActive;
    }
}
