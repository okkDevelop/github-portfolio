using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance;

    public QuestEvents questEvents;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }

        questEvents = new QuestEvents();
    }
}
