using EmeraldAI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject resources;
    [SerializeField] private float spawnTime;
    [SerializeField] private int limitation;
    private BoxCollider spawner;
    private int existingResources;
    private List<GameObject?> spawnedObjects = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        spawner = GetComponent<BoxCollider>();

        InvokeRepeating("SpawnResources", 0f, spawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stone"))
        {
            spawnedObjects.Add(other.gameObject);
            existingResources++;
        }
    }

    private void SpawnResources() 
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] == null)
            {
                spawnedObjects.RemoveAt(i);
                existingResources--;
            }
        }

        if (existingResources <= limitation)
        {
            Instantiate(resources, RandomPostionInSpawner(spawner), Quaternion.identity);
        }
    }

    private Vector3 RandomPostionInSpawner(BoxCollider spawningArea)
    {
        Bounds bounds = spawningArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}
