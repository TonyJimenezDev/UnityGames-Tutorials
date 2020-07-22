using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySpawner : MonoBehaviour
{
    [SerializeField] FriendlyMovement friendlyprefab;
    [SerializeField] GameObject putFriendliesInHere;

    void Start()
    {
        StartCoroutine(SpawnFriendly());
    }

    IEnumerator SpawnFriendly()
    {
        while (true)
        {
            float rn = Random.Range(5f, 10f);
            yield return new WaitForSeconds(rn);

            var newFriendly = Instantiate(friendlyprefab, friendlyprefab.transform.position, friendlyprefab.transform.rotation);
            newFriendly.transform.parent = putFriendliesInHere.transform;
        }
    }
}
