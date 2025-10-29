using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> selectedTool = new List<GameObject>();
    [SerializeField] private GameObject weaponParentObject;
    private List<GameObject> currentTools = new List<GameObject>();
    [SerializeField] private List<GameObject> selectedFrame = new List<GameObject>();

    //[HideInInspector] public bool GotAxe;
    //[HideInInspector] public bool GotPickaxe;
    //[HideInInspector] public bool GotSword;
    //[HideInInspector] public bool GotMagic;

    private bool isBareHand;
    private bool isAxe;
    private bool isPickaxe;
    private bool isSword;
    private bool isMagic;

    public bool IsBareHand => isBareHand;
    public bool IsAxe => isAxe;
    public bool IsPickaxe => isPickaxe;
    public bool IsSword => isSword;
    public bool IsMagic => isMagic;

    private int scrollIndex;

    private void Awake()
    {
        foreach (Transform weaponChild in weaponParentObject.transform)
        {
            currentTools.Add(weaponChild.gameObject);
        }
    }

    private void Start()
    {
        scrollIndex = 0;
    }

    private void Update() 
    {
        ScrollToSelect();
    }

    private void ScrollToSelect() 
    {
        float _scrollValue = Input.GetAxis("Mouse ScrollWheel");

        if (_scrollValue < 0f)
        {
            scrollIndex++;
            if (scrollIndex >= selectedTool.Count) 
                scrollIndex = 0;
        }
        else if (_scrollValue > 0f) 
        {
            scrollIndex--;
            if (scrollIndex < 0)
                scrollIndex = selectedTool.Count - 1;
        }

        SelectedTools();
    }

    private void SelectedTools() 
    {
        isBareHand = (scrollIndex == 0);
        isAxe = (scrollIndex == 1);
        isPickaxe = (scrollIndex == 2);
        isSword = (scrollIndex == 3);
        isMagic = (scrollIndex == 4);

        for (int i = 0; i < selectedTool.Count; i++)
        {
            selectedFrame[i].SetActive(i == scrollIndex);
            currentTools[i].SetActive(i == scrollIndex);
        }
    }
}
