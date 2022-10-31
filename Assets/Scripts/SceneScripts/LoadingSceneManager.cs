using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public enum STAGE { MAIN = -1, COW, DEMON, MAX }

    public static string nextScene;
    [SerializeField] Slider progressBar;

    public TextMeshProUGUI loadingText;

    private float alpha = -1f;
    private float value;
    private bool isActive = false;

    public static int currentStage = (int)STAGE.MAIN;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        if (isActive)
        {
            if (loadingText.color.a == 0)
            {
                alpha *= -1;
            }
            else if (loadingText.color.a == 1)
            {
                alpha *= -1;
            }
            value += alpha * Time.deltaTime;
            loadingText.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), value);
        }
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    public static void NowStage(int num)
    {
        switch(num)
        {
            case (int)STAGE.MAIN:
                currentStage = num;
                LoadScene("MainScene");
                break;
            case (int)STAGE.COW:
                currentStage = num;
                LoadScene("BossScene_Beta");
                break;
            case (int)STAGE.DEMON:
                currentStage = num;
                LoadScene("BossScene_Beta");
                break;
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value == 1.0f)
                {
                    isActive = true;
                    loadingText.gameObject.SetActive(isActive);
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            op.allowSceneActivation = true;
                            yield break;
                        }
                    }
                }
            }
        }
    }
}
