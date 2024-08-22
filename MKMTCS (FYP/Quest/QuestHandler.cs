using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuestHandler : QuestStep
{
    [SerializeField] private Quests quest;

    protected override void SubscribeEvents()
    {
        switch (quest) 
        {
            case Quests.CutTree:
                GameEventsManager.Instance.questEvents.CutTree += NumberBasedQuest;
                break;
            case Quests.MineRock:
                GameEventsManager.Instance.questEvents.MineRock += NumberBasedQuest;
                break;
            case Quests.UpgradeHouse:
                GameEventsManager.Instance.questEvents.UpgradeHouse += FinishedQuestStep;
                break;
            case Quests.KillMonster:
                GameEventsManager.Instance.questEvents.KillMonster += NumberBasedQuest;
                break;
            case Quests.RegainHealth:
                GameEventsManager.Instance.questEvents.RegainHealth += NumberBasedQuest;
                break;
            case Quests.RegainHunger:
                GameEventsManager.Instance.questEvents.RegainHunger += NumberBasedQuest;
                break;
            case Quests.RegainMana:
                GameEventsManager.Instance.questEvents.RegainMana += NumberBasedQuest;
                break;
            case Quests.FixPortal:
                GameEventsManager.Instance.questEvents.FixPortal += FinishedQuestStep;
                break;
            case Quests.FindSnowy:
                GameEventsManager.Instance.questEvents.FindSnowy += DestinationBasedQuest;
                break;
            case Quests.FindVolcano:
                GameEventsManager.Instance.questEvents.FindVolcano += DestinationBasedQuest;
                break;
            case Quests.FindFloatingIslands:
                GameEventsManager.Instance.questEvents.FindFloatingIslands += DestinationBasedQuest;
                break;
        }

    }

    protected override void UnsubscribeEvents()
    {
        switch (quest)
        {
            case Quests.CutTree:
                GameEventsManager.Instance.questEvents.CutTree -= NumberBasedQuest;
                break;
            case Quests.MineRock:
                GameEventsManager.Instance.questEvents.MineRock -= NumberBasedQuest;
                break;
            case Quests.UpgradeHouse:
                GameEventsManager.Instance.questEvents.UpgradeHouse -= FinishedQuestStep;
                break;
            case Quests.KillMonster:
                GameEventsManager.Instance.questEvents.KillMonster -= NumberBasedQuest;
                break;
            case Quests.RegainHealth:
                GameEventsManager.Instance.questEvents.RegainHealth -= NumberBasedQuest;
                break;
            case Quests.RegainHunger:
                GameEventsManager.Instance.questEvents.RegainHunger -= NumberBasedQuest;
                break;
            case Quests.RegainMana:
                GameEventsManager.Instance.questEvents.RegainMana -= NumberBasedQuest;
                break;
            case Quests.FixPortal:
                GameEventsManager.Instance.questEvents.FixPortal -= FinishedQuestStep;
                break;
            case Quests.FindSnowy:
                GameEventsManager.Instance.questEvents.FindSnowy -= DestinationBasedQuest;
                break;
            case Quests.FindVolcano:
                GameEventsManager.Instance.questEvents.FindVolcano -= DestinationBasedQuest;
                break;
            case Quests.FindFloatingIslands:
                GameEventsManager.Instance.questEvents.FindFloatingIslands -= DestinationBasedQuest;
                break;
        }
        SoundsManager.Instance.PlaySound("QuestFinished");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            arrivedDestination = true;
            switch (quest)
            {
                case Quests.FindSnowy:
                    GameEventsManager.Instance.questEvents.FindingSnowy();
                    break;
                case Quests.FindVolcano:
                    GameEventsManager.Instance.questEvents.FindingVolcano();
                    break;
                case Quests.FindFloatingIslands:
                    GameEventsManager.Instance.questEvents.FindingFloatingIslands();
                    break;
            }
        }
    }
}

public enum Quests
{
    CutTree,
    MineRock,
    UpgradeHouse,
    KillMonster,
    RegainHealth,
    RegainHunger,
    RegainMana,
    FixPortal,
    FindSnowy,
    FindVolcano,
    FindFloatingIslands
}
