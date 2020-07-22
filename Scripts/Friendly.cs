using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : MonoBehaviour
{
    // place holders
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] float hits = 5f;


    private void OnParticleCollision(GameObject other)
    {
        hits = hits - 1;
        if (hits <= 5)
        {
            FriendlyDeath();
        }
    }

    void FriendlyDeath()
    {
        GameObject fx = Instantiate(deathFX,transform.position,Quaternion.identity );
        fx.transform.parent = parent;

        Destroy(gameObject);
    }


}
