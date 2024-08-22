using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestInfo", menuName = "ScriptableObjects/QuestInfo_SO")]
public class QuestInfo_SO : ScriptableObject
{
    [Header("General")]
    public string DisplayName;
    [TextArea(10,10)] public string Description;
    public int NumberToFinished;

    private void OnValidate()
    {
#if UNITY_EDITOR
        DisplayName = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
