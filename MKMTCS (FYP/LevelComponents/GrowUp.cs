using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowUp : MonoBehaviour
{
    [SerializeField] private GameObject[] objectAfterGrowUp;
    [SerializeField] private float growUpTime;
    
    private void Update()
    {
        Growing();
    }

    private void Growing() 
    {
        growUpTime -= Time.deltaTime;

        if (growUpTime <= 0)
        {
            growUpTime = 0;
            int randomNum = Random.Range(0,objectAfterGrowUp.Length);
            Instantiate(objectAfterGrowUp[randomNum], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
