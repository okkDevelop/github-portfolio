using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    [Header("General Quest Settings")]
    private bool isFinished = false;
    private bool haveMissionAfter;
    [SerializeField] protected GameObject? nextQuestPrefab;
    [SerializeField] protected QuestInfo_SO questInfo;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;

    [Header("Number Based Quest Settings")]
    private int currentNumber = 0;
    //[SerializeField] protected int numberToFinished;

    [Header("Destination Quest Settings")]
    protected bool arrivedDestination;

    protected abstract void SubscribeEvents();
    protected abstract void UnsubscribeEvents();

    private void Start() 
    {
        questTitle.text = questInfo.DisplayName;
        questDescription.text = questInfo.Description;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            FinishedQuestStep();
        }
    }

    protected virtual void OnEnable()
    {
        SubscribeEvents();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeEvents();
    }

    protected void NumberBasedQuest()
    {
        if (currentNumber < questInfo.NumberToFinished)
        {
            currentNumber++;
            Debug.Log("current number" + questInfo.NumberToFinished);
        }

        if (currentNumber >= questInfo.NumberToFinished)
        {
            Debug.Log("this quest is finished");
            FinishedQuestStep();
        }
    }

    protected void DestinationBasedQuest()
    {
        if (arrivedDestination)
            FinishedQuestStep();
    }

    protected void ConditionBasedQuest() 
    {
        FinishedQuestStep();
    }

    protected void FinishedQuestStep() 
    {
        if (!isFinished)
        {
            isFinished = true;

            if (nextQuestPrefab != null)
            {
                var nextQuest = Instantiate(nextQuestPrefab);
                var nextQuestStep = nextQuest.GetComponent<QuestStep>();
                if (nextQuestStep != null)
                {
                    nextQuestStep.questTitle = questTitle;
                    nextQuestStep.questDescription = questDescription;
                }
            }
            else 
            {
                questTitle.text = "12.Escape!";
                questDescription.text = "now find a way to escape from this islands, HINT('upgrade the special object in each area')";
                Debug.Log("this is last mission");
            }

            Destroy(gameObject);
        }
    }
}