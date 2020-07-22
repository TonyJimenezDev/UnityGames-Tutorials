using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] EnemyMovement enemyPrefab2;
    [SerializeField] GameObject putEnemiesInHere;

    [Header("Transition")]
    [SerializeField] float waitTimeforBigEnemy = 7;
    [SerializeField] float waitTimeforSmallEnemy = 4;

    [Header("Audio")]
    [SerializeField] AudioClip SpawnEnemyFX;
    [SerializeField] AudioClip SpawnEnemyFX2;

    public Text scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnEnemy2());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {                   
            yield return new WaitForSeconds(waitTimeforSmallEnemy);
            GetComponent<AudioSource>().PlayOneShot(SpawnEnemyFX);
            var newEnemy = Instantiate(enemyPrefab, enemyPrefab.transform.position, enemyPrefab.transform.rotation);
            newEnemy.transform.parent = putEnemiesInHere.transform;                
        }
    }

    IEnumerator SpawnEnemy2()
    {
        while (true)
        {                    
            yield return new WaitForSeconds(waitTimeforBigEnemy);
            GetComponent<AudioSource>().PlayOneShot(SpawnEnemyFX2);
            var newEnemy = Instantiate(enemyPrefab2, enemyPrefab2.transform.position, enemyPrefab2.transform.rotation);
            newEnemy.transform.parent = putEnemiesInHere.transform;
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "" + score;
        
    }
}
