using UnityEngine;
using DG.Tweening;

public class UIController : BaseMenuManager
{
    [Header("In-gameUI controller")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject[] allMenu;

    [Header("InventoryMenu Settings")]
    [SerializeField] private GameObject inventoryBG;
    [SerializeField] private float inventoryBGEaseInPosition;
    [SerializeField] private float inventoryBGEaseOutPosition;
    private bool hasPressedInventoryBG;
    private bool inventoryBGTransitioning;

    [Header("ControlMenu Settings")]
    [SerializeField] private GameObject craftingBG;
    [SerializeField] private float craftingBGEaseInPosition;
    [SerializeField] private float craftingBGEaseOutPosition;
    private bool hasPressedCraftingBG;
    private bool craftingBGTransitioning;

    [Header("MiniMapMenu Settings")]
    [SerializeField] private GameObject miniMapMenu;
    [SerializeField] private float miniMapMenuEaseInPosition;
    [SerializeField] private float miniMapMenuEaseOutPosition;
    private bool hasPressedMiniMap;
    private bool miniMapTransitioning;

    [SerializeField] private float movingSpeed;

    public bool HasPressedInventoryBG => hasPressedInventoryBG;
    public bool HasPressedCraftingBG => hasPressedCraftingBG;

    protected override void Start()
    {
        base.Start();

        pauseMenu.SetActive(false);
        controlMenu.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape)) 
            pauseMenu.SetActive(!pauseMenu.active ? true : false);

        if (inventoryBG != null && craftingBG != null && miniMapMenu != null)
            PanelTrigger();        
    }

    public void OnResumeBtnClicked() 
    {
        pauseMenu.SetActive(false);
    }

    public void OnControlBtnClicked()
    {
        controlMenu.SetActive(!controlMenu.active ? true : false);
    }

    public bool CheckMenu() 
    {
        foreach (GameObject menu in allMenu) 
        {
            if (menu.active)
                return false;
        }
        return true;
    }

    private void PanelTrigger()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryBGTransitioning)
                InventoryShow();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!craftingBGTransitioning)
                CraftingShow();
        }

        if (Input.GetKeyDown(KeyCode.M)) 
        {
            if (!miniMapTransitioning) 
                MiniMapShow();
        }
    }

    private void InventoryShow()
    {
        hasPressedInventoryBG = !hasPressedInventoryBG;
        inventoryBGTransitioning = true;
        inventoryBG.transform.DOLocalMoveX(hasPressedInventoryBG ? inventoryBGEaseInPosition : inventoryBGEaseOutPosition, movingSpeed)
                .SetEase(ease: Ease.InOutCubic) // Use Ease.Linear for a straight line movement (optional)
                .OnComplete(() => inventoryBGTransitioning = false);
        SoundsManager.Instance.PlaySound("Open");
    }

    private void CraftingShow()
    {
        hasPressedCraftingBG = !hasPressedCraftingBG;
        craftingBGTransitioning = true;
        craftingBG.transform.DOLocalMoveX(hasPressedCraftingBG ? craftingBGEaseInPosition : craftingBGEaseOutPosition, movingSpeed)
                .SetEase(ease: Ease.InOutCubic) // Use Ease.Linear for a straight line movement (optional)
                .OnComplete(() => craftingBGTransitioning = false);
        SoundsManager.Instance.PlaySound("Open");
    }

    private void MiniMapShow()
    {
        hasPressedMiniMap = !hasPressedMiniMap;
        miniMapTransitioning = true;
        miniMapMenu.transform.DOLocalMoveY(hasPressedMiniMap ? miniMapMenuEaseInPosition : miniMapMenuEaseOutPosition, movingSpeed)
            .SetEase(ease: Ease.InOutCubic) // Use Ease.Linear for a straight line movement (optional)
            .OnComplete(() => miniMapTransitioning = false);
        SoundsManager.Instance.PlaySound("Open");
    }
}
