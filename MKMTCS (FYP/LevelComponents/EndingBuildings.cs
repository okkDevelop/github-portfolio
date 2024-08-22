using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBuildings : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Win());
    }

    private IEnumerator Win() 
    {
        yield return new WaitForSeconds(5);
        GameManager.Instance.WinTrigger = true;
    }
}
