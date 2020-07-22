using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 100;
    [SerializeField] float hits = 100;

    ScoreBoard scoreBoard;

    void Start()
    {
        AddsNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        // todo hit FX
        hits = hits - 1;
        if (hits <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;

        //gets name of object print ("Partical crashes with " + gameObject.name);
        Destroy(gameObject);
    }

    void AddsNonTriggerBoxCollider()
    {
        
        Collider boxNontrig = gameObject.AddComponent<BoxCollider>();
        boxNontrig.isTrigger = false;
    }
}
