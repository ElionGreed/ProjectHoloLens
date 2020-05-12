using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawnPoints;
    [SerializeField]
    GameObject[] enemyPrefabs;
    bool isDone = true;
    // Start is called before the first frame update
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isDone == true)
        {
            Spawn();
            isDone = false;
        }
    }
    // Update is called once per frame
    void Spawn()
    {
       int numOfSpawns = Random.Range(1, 4);
       int enemyPrefab;
       for(int i = 0; i < numOfSpawns; i++)
        {
            enemyPrefab = Random.Range(0, 4);
            try
            {
                spawnPoints[i] = Instantiate((enemyPrefabs[enemyPrefab]));
            }
            catch { }
           
        }
        gameObject.SetActive(false);
    }
}
