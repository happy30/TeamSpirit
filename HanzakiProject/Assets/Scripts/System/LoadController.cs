//LoadController by Jordi

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadController : MonoBehaviour
{
    //Load this interface before loading...
    public GameObject loadingInterface;

    //Sprite and rotatingspeed
    public RectTransform loadSprite;
    public float rotateSpeed;

    //Slider for progress
    public Slider progressBar;

    //async
    AsyncOperation async;

    //Load the scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(StartASync(sceneName));
        loadingInterface.SetActive(true);
    }

    IEnumerator StartASync(string level)
    {
        async = SceneManager.LoadSceneAsync(level);
        if (loadingInterface != null)
        {
            loadingInterface.SetActive(false);
        }
        yield return async;
    }

    void Update()
    {
        if (async != null)
        {
            //Loading bar (Slider)
            progressBar.value = (float)async.progress;

            //Sprite to move alongside the progress of the slider with rotation
            if (loadSprite != null)
            {
                loadSprite.anchoredPosition = new Vector2(((float)async.progress * 1750) - 870, loadSprite.anchoredPosition.y);
                loadSprite.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
            }
        }
    }
}
