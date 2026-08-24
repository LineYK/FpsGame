using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingNextScene : MonoBehaviour
{
    [SerializeField]
    private int sceneNum = 2;

    [SerializeField]
    private Slider loadingBar;

    [SerializeField]
    private TMP_Text loadingText;


    private void Start()
    {
        StartCoroutine(TransitionNextScene(sceneNum));
    }

    // 비동기 씬 로드
    private IEnumerator TransitionNextScene(int num)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            loadingBar.value = ao.progress;
            loadingText.text = (ao.progress * 100f).ToString() + "%";

            if (ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
