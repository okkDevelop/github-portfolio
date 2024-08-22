using UnityEngine;
using UnityEngine.UI;

public class MenuManager : BaseMenuManager
{
    [Header("MainMenuPanel")]
    [SerializeField] private GameObject mainMenuCollection;

    [Header("PlayMenuPanel")]
    [SerializeField] private GameObject playMenuCollection;
    [SerializeField] private GameObject saveFileHorizontalLayoutGrp;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject levelHorizontalLayoutGrp;
    [SerializeField] private Button levelButton;

    [Header("SettingMenuPanel")]
    [SerializeField] private GameObject settingMenuCollection;

    [Header("CreditMenuPanel")]
    [SerializeField] private GameObject creditMenuCollection;

    protected override void Start()
    {
        base.Start();

        ActivateMenuPanel(mainMenuCollection.name);
        saveFileHorizontalLayoutGrp.SetActive(false);
        levelHorizontalLayoutGrp.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }

    public void OnPlayGameBtnPressed() 
    {
        ActivateMenuPanel(playMenuCollection.name);
        saveFileHorizontalLayoutGrp.SetActive(true);
        playButton.interactable = false;
    }

    public void OnPlayBtnClicked() 
    {
        levelButton.interactable = true;
        playButton.interactable = false;
        saveFileHorizontalLayoutGrp.SetActive(true);
        levelHorizontalLayoutGrp.SetActive(false);
    }

    public void OnLevelBtnClicked()
    {
        levelButton.interactable = false;
        playButton.interactable = true;
        levelHorizontalLayoutGrp.SetActive(true);
        saveFileHorizontalLayoutGrp.SetActive(false);
    }

    public void OnSettingBtnPressed()
    {
        ActivateMenuPanel(settingMenuCollection.name);
    }

    public void OnCreditBtnPressed()
    {
        ActivateMenuPanel(creditMenuCollection.name);
    }

    public void OnCloseBtnClicked() 
    {
        ActivateMenuPanel(mainMenuCollection.name);
    }

    private void ActivateMenuPanel(string menuName)
    {
        mainMenuCollection.SetActive(menuName.Equals(mainMenuCollection.name));
        playMenuCollection.SetActive(menuName.Equals(playMenuCollection.name));
        settingMenuCollection.SetActive(menuName.Equals(settingMenuCollection.name));
        creditMenuCollection.SetActive(menuName.Equals(creditMenuCollection.name));
    }
}
