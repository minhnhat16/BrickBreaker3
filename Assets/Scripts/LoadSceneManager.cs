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
            await Task.Delay(100);
            progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
        progressBar.enabled = false; 

    }
}
