using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPooling : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject projectileObject;
    [SerializeField] private int poolSize;
    private List<GameObject> projectilePool = new List<GameObject>();

    public static ObjectPooling Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }

        Initialized();
    }

    private void Initialized()
    {
        for (int i = 0; i < poolSize; i++) 
        {
            GameObject _projectileToUse = Instantiate(projectileObject);
            _projectileToUse.SetActive(false);
            projectilePool.Add(_projectileToUse);
        }
    }

    public GameObject GetProjectile(Vector3 position, Quaternion rotation) 
    {
        if (projectilePool.Count > 0)
        {
            GameObject projectile = projectilePool[0];
            projectilePool.RemoveAt(0);
            projectile.transform.position = position; 
            projectile.transform.rotation = rotation; 
            projectile.SetActive(true);
            return projectile;
        }
        else
        { 
            Debug.Log("projectile pool is currently empty");
            return null;
        }
    }

    public void StoreProjectile(GameObject projectile)
    {
        if (!projectilePool.Contains(projectile)) 
        {
            projectile.SetActive(false); 
            projectilePool.Add(projectile);
        }
    }
}
