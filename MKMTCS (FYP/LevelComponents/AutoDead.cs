using EmeraldAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EmeraldAISystem))]
public class AutoDead : MonoBehaviour
{
    [SerializeField] private float countDown = 480f;
    private EmeraldAISystem emeraldAISystem;
    private EnemySpawner enemySpawner;

    private void Start()
    {
        emeraldAISystem = GetComponent<EmeraldAISystem>();

        if (emeraldAISystem) 
            StartCoroutine(CountDownToDie());
    }

    private IEnumerator CountDownToDie() 
    {
        yield return new WaitForSeconds(countDown);
        enemySpawner.EnemiesRemove(gameObject);
        Destroy(gameObject);
    }
}
