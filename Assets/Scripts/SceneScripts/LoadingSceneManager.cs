using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public enum STAGE { MAIN = -1, COW, DEMON, MAX }

    public static string nextScene;
    [SerializeField] Slider progressBar;

    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI loadingText2;
    public Slider loadingSlider;

    private float alpha = -1f;
    private float value;
    private bool isTouchMsgActive = false;
    private bool isLoadingMsgActive = true;

    private List<string> loadingTextList = new List<string>();
    private int count = 0;
    public float curTime = 0;
    public float speed = 5f;

    public float curFadeTime = 0;
    public GameObject fadeBG;

    AsyncOperation op;

    public static int currentStage = (int)STAGE.MAIN;

    private void Start()
    {
        StartCoroutine(LoadScene());
        loadingTextList.Add("Loading.");
        loadingTextList.Add("Loading..");
        loadingTextList.Add("Loading...");
    }

    private void Update()
    {
        LoadingMsgAnim();
        TouchMsgAnim();
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
        op = SceneManager.LoadSceneAsync(nextScene);
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
                    isTouchMsgActive = true;
                    isLoadingMsgActive = false;
                    loadingText2.gameObject.SetActive(isLoadingMsgActive);
                    loadingSlider.gameObject.SetActive(false);
                    loadingText.gameObject.SetActive(isTouchMsgActive);
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            fadeBG.gameObject.SetActive(true);
                            StartCoroutine(FadeInCoroutine());
                            yield break;
                        }
                    }
                }
            }
        }
    }

    void TouchMsgAnim()
    {
        if (isTouchMsgActive)
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

    void LoadingMsgAnim()
    {
        if (isLoadingMsgActive)
        {
            curTime += Time.deltaTime * speed;
            if (curTime >= 1f)
            {
                Debug.Log(count);
                loadingText2.text = loadingTextList[count++];
                if (count >= loadingTextList.Count)
                {
                    count = 0;
                }
                curTime = 0;
            }
        }
    }

    IEnumerator FadeInCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            curFadeTime += Time.deltaTime * 10;
            fadeBG.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), curFadeTime);
            if (curFadeTime >= 1)
            {
                curFadeTime = 0;
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
