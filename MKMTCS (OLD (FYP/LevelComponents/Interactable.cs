using EmeraldAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable Item Settings")]
    [SerializeField] private GameObject[] ItemsToDrop;
    [SerializeField] private int interactableIndex;
    [SerializeField] private Transform itemDropTransform;
    [SerializeField] private int minNumberItemDrop = 0;
    [SerializeField] private int maxNumberItemDrop;
    private float waitTime;

    private void Awake()
    {
        waitTime = gameObject.GetComponent<EmeraldAISystem>() ? 5 : 0;
    }

    public void Interacting(int damage) 
    {
        interactableIndex -= damage;

        if (interactableIndex <= 0)
            DropItem();
    }

    public void DropItem()
    {
        if(gameObject.CompareTag("Wooden"))
            GameEventsManager.Instance.questEvents.CuttingTree();
        else if(gameObject.CompareTag("stone"))
            GameEventsManager.Instance.questEvents.MiningRock();
        else if(gameObject.CompareTag("Enemy"))
            GameEventsManager.Instance.questEvents.KillingMonster();

        int randomNum = Random.Range(minNumberItemDrop, maxNumberItemDrop);

        foreach (GameObject itemToDrop in ItemsToDrop) 
        {
            for (int i = 0; i < randomNum; i++)
            {
                Transform spawnTransform = itemDropTransform != null ? itemDropTransform : transform;
                Instantiate(itemToDrop, spawnTransform.position, Quaternion.identity);
            }
        }
        StartCoroutine(DestroyWhenDead(waitTime));
    }

    private IEnumerator DestroyWhenDead(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);

        //if (gameObject.CompareTag("Enemy"))
        //    gameObject.SetActive(false);
        //else
        //    Destroy(gameObject);
    }
}
