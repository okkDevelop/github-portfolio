using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HungerBar;
    [SerializeField] private Image ManaBar;

    [Header("Menu")]
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private GameObject LostMenu;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        HealthBar.fillAmount = PlayerStatus.Instance.HealthValue / 100;
        HungerBar.fillAmount = PlayerStatus.Instance.HungerValue / 100;
        ManaBar.fillAmount = PlayerStatus.Instance.ManaValue / 100;

        IsWin();
        IsDeath();
    }

    private void IsWin() 
    {
        if (GameManager.Instance.WinTrigger) 
        {
            WinMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void IsDeath() 
    {
        if (PlayerStatus.Instance.HealthValue <= 0) 
        {
            LostMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnReviveBtnClicked()
    {
        LostMenu.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.Revive();
    }
}

