using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesToSpawnS1;
    [SerializeField] private List<GameObject> enemiesToSpawnS2;
    [SerializeField] private GameObject witch;
    [SerializeField] private float SpawnRange = 2.0f;
    [SerializeField] private int spawnTimeS1 = 5;
    [SerializeField] private int spawnTimeS2 = 2;
    [SerializeField] private int totalEnemiesS1 = 30;
    [SerializeField] private int totalEnemiesS2 = 40;

    private GameObject[] enemies;
    private bool waveFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFirstStage());
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log(enemies.Length);
        if(witch != null) { witch.SetActive((totalEnemiesS2 == 0) && (enemies.Length == 0)); }
    }

    IEnumerator SpawnFirstStage()
    {
        yield return new WaitForSeconds(spawnTimeS1);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        for (int i = 0; i < enemiesToSpawnS1.Count; i++)
        {
            Instantiate(enemiesToSpawnS1[i], 
                    new Vector3(transform.position.x + Random.Range(SpawnRange, SpawnRange), 
                    transform.position.y +  Random.Range(-SpawnRange, SpawnRange), 0), 
                    Quaternion.identity);
            totalEnemiesS1--;
            if(totalEnemiesS1 == 0) { break; }
        }
        if(totalEnemiesS1 > 0) { StartCoroutine(SpawnFirstStage()); }
        else { StartCoroutine(SpawnSecondStage()); }
    }
    IEnumerator SpawnSecondStage()
    {
        yield return new WaitForSeconds(spawnTimeS2);

        if(enemies.Length == 0)
        {
            waveFinished = true;
        }
        if(waveFinished)
        {
            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            for (int i = 0; i < enemiesToSpawnS2.Count; i++)
            {
                Instantiate(enemiesToSpawnS2[i], 
                        new Vector3(transform.position.x + Random.Range(SpawnRange, SpawnRange), 
                        transform.position.y +  Random.Range(-SpawnRange, SpawnRange), 0), 
                        Quaternion.identity);
                totalEnemiesS2--;
                if(totalEnemiesS2 == 0) { break; }
            }
        }
        if(totalEnemiesS2 > 0) { StartCoroutine(SpawnSecondStage()); }
    }

    public bool SpawnFinished()
    {
        return ((totalEnemiesS2 == 0) && (enemies.Length == 0));
    }
}
