using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAsync : MonoBehaviour
{
    public static LoadAsync instance;
    [SerializeField] GameObject loadingUI;
    [SerializeField] GameObject pauseMenu;
    private void Awake()
    {
        instance = this;
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingUI.SetActive(true);

        do
        {
            await Task.Delay(4000);
        } while (scene.progress < 0.9f);
        pauseMenu.SetActive(false);
        scene.allowSceneActivation = true;
        loadingUI.SetActive(false);
    }

    public async void LoadSceneNumber(int sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingUI.SetActive(true);

        do
        {
            await Task.Delay(100);
        } while (scene.progress < 0.9f);
        pauseMenu.SetActive(false);
        scene.allowSceneActivation = true;
        loadingUI.SetActive(false);
    }

    //public void LoadScene(string sceneName)
    //{
    //    StartCoroutine(LoadAsynch(sceneName));
    //}

    //IEnumerator LoadAsynch(string sceneName)
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    //    loadingUI.SetActive(true);
    //    while (!operation.isDone)
    //    {
    //        float progress = Mathf.Clamp01(operation.progress / .9f);
    //        yield return null;
    //    }

    //    if(operation.isDone)
    //    {
    //        loadingUI.SetActive(false);
    //    }
    //}
}
