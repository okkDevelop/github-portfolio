using System;
using UnityEngine;

public class QuestEvents
{
    public event Action CutTree;
    public event Action MineRock;
    public event Action UpgradeHouse;
    public event Action KillMonster;
    public event Action RegainHealth;
    public event Action RegainHunger;
    public event Action RegainMana;
    public event Action FixPortal;
    public event Action FindSnowy;
    public event Action FindVolcano;
    public event Action FindFloatingIslands;

    private void QuestTrigger(Action questEvent, string logMsg) 
    {
        questEvent?.Invoke();
        Debug.Log(logMsg);
    }

    #region Mission Listed
    public void CuttingTree()
    {
        QuestTrigger(CutTree, "cut tree mission trigger");
    }

    public void MiningRock()
    {
        QuestTrigger(MineRock, "mining mission trigger");
    }

    public void HouseUpgrade()
    {
        QuestTrigger(UpgradeHouse, "upgrade house mission trigger");
    }

    public void KillingMonster()
    {
        QuestTrigger(KillMonster, "monster mission trigger");
    }

    public void HealthRegain()
    {
        QuestTrigger(RegainHealth, "health regain mission trigger");
    }

    public void EatingFood() 
    {
        QuestTrigger(RegainHunger, "hunger mission trigger");
    }

    public void ManaRegain()
    {
        QuestTrigger(RegainMana, "mana regain mission trigger");
    }

    public void FixedPortal()
    {
        QuestTrigger(FixPortal, "fix portal mission trigger");
    }

    public void FindingSnowy()
    {
        QuestTrigger(FindSnowy, "snowy mission trigger");
    }

    public void FindingVolcano() 
    {
        QuestTrigger(FindVolcano, "volcano mission trigger");
    }

    public void FindingFloatingIslands()
    {
        QuestTrigger(FindFloatingIslands, "FI mission trigger");
    }

    #endregion
}
