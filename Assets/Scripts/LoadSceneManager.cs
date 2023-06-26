using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UIElements;


public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private UnityEngine.UI.Image progressBar;
    [SerializeField] private GameObject _backGroundCanvas;
    [SerializeField] private GameObject _pauseGameBtn;
    public GameObject _GameOverUI;
    public GameObject _CompleteLeverUI;
    public GameObject _LevelPopUpUI;
    public GameObject _LevelScrollView;
    public GameObject _MenuUI;
    public GameObject _InGameUI;
    private void Awake()
    {
        Instance = this;
    }
    public async void LoadScene(string sceneName)
    {

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);
        do
        {
            await Task.Delay(300);
            progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }

    //SET BACKGROUND CANVAS ACTIVE
    public void SetActiveCanvas(GameObject _object, bool active)
    {
        if (active)
        {
            _object.SetActive(true);
        }
        else
        {
            _object.SetActive(false);
        }
    }
    public void ChangeScene(string sceneName)
    {
        LoadScene(sceneName);
        GameManager.Instance.LoadOnInGameController();

    }
    public void HidePopUpUI()
    {

    }

    public void HideMenuUI()
    {
        _MenuUI.gameObject.SetActive(false);
    }
    public void HideInGameUI()
    {
        _InGameUI.gameObject.SetActive(false);
    }
    public void ActiveIngameUI()
    {
        _InGameUI.gameObject.SetActive(true);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

}
