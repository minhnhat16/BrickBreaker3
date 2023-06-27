using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public class BootLoader : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(LoadSceneAfterDelay(1f));
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // T?i scene có tên "Buffer"
        SceneManager.LoadScene("Buffer");
        ViewManager.Instance.SwitchView(ViewIndex.LoadingView, null, null);
    }
}
