using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float curFadeTime = 0;
    public Image fadeBG;
    GameManager gm;

    public float speed;

    void Start()
    {
        gm = GameManager.GetInstance();
        StartCoroutine(FadeOutCoroutine());
        speed = 1f;
    }

    public IEnumerator FadeOutCoroutine()
    {
        while (true)
        {
            yield return null;
            curFadeTime += Time.deltaTime * speed;
            fadeBG.GetComponent<Image>().color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), curFadeTime);
            if (curFadeTime >= 1)
            {
                fadeBG.gameObject.SetActive(false);
                if (LoadingSceneManager.currentStage != (int)LoadingSceneManager.STAGE.MAIN)
                {
                    gm.stage.gameObject.SetActive(true);
                }
                curFadeTime = 0;
                yield break;
            }
        }
    }
}
