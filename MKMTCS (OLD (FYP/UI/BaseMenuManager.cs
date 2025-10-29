using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseMenuManager : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] protected GameObject LoadingMenu;
    [SerializeField] protected AudioSource bgm;
    [SerializeField] protected Slider volumnSlider;

    protected virtual void Start()
    {
        LoadingMenu.SetActive(false);
    }

    protected virtual void Update()
    {
        bgm.volume = volumnSlider.value;
    }

    public void OnLoadSceneBtnClicked(int sceneIndex) 
    {
        LoadingMenu.SetActive(true);
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnQuitBtnClick() 
    {
        Application.Quit();
    }
}
