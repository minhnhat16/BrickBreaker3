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
    public string currrentSence;
    private void Awake()
    {
        Instance = this;
    }
    public async void LoadScene(string sceneName)
    {
        currrentSence = sceneName;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        ViewManager.Instance.SwitchView(ViewIndex.LoadingView);
        do
        {
            await Task.Delay(0);
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
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
