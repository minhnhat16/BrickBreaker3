using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject _backGroundCanvas;
    public GameObject _GameOverUI;

    private void Awake()
    {
        Instance = this;
    }
    public async void LoadScene(string sceneName)
    {

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _backGroundCanvas.SetActive(false);
        _loaderCanvas.SetActive(true);
        do
        {
            await Task.Delay(300);
            progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
        

    }
}
