using EmeraldAI;
using EmeraldAI.Example;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectToSpawn = new List<GameObject>();
    [SerializeField] private int spawnLimitation;
    [SerializeField] private float spawnDuration;
    private List<GameObject> objectAlreadySpawn = new List<GameObject>();
    private MeshCollider spawningArea;

    private void Start()
    {
        spawningArea = GetComponent<MeshCollider>();

        InvokeRepeating("SpawnEnemies", 0f, spawnDuration);
    }

    private void SpawnEnemies() 
    {
        if (objectAlreadySpawn.Count <= spawnLimitation) 
        {
            int randomPick = Random.Range(0, objectToSpawn.Count);
            if (objectAlreadySpawn.Count < spawnLimitation) 
            {
                GameObject newEnemies = Instantiate(objectToSpawn[randomPick], RandomPostion2(spawningArea), Quaternion.identity);
                objectAlreadySpawn.Add(newEnemies);

                newEnemies.GetComponent<EmeraldAIBehaviors>().OnDeath += EnemiesRemove;
            }
        }
    }

    private Vector3 RandomPostion2(MeshCollider spawningArea2)
    {
        Bounds bounds = spawningArea2.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }

    public void EnemiesRemove(GameObject deadEnemy) 
    {
        objectAlreadySpawn.Remove(deadEnemy);
    }
}
