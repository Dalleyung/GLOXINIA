using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Image fadeBG;
    public Image gameoverBG;
    public Image light;

    GameManager gm;
    private void Start()
    {
        gm = GameManager.GetInstance();
    }
    private void Update()
    {
  
    }

    public float currentTime = 0;
    public void StartGameOverAnim()
    {
        fadeBG.gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime * 0.5f;
            fadeBG.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), currentTime);
            if (currentTime >= 1)
            {
                break;
            }
        }
        gameoverBG.gameObject.SetActive(true);
        StartCoroutine(FadeOutCoroutine());
    }

    public IEnumerator FadeOutCoroutine()
    {
        gm.soundManager.PlayBGMSound(gm.soundManager.defeatBGM);
        while (true)
        {
            yield return null;
            currentTime -= Time.deltaTime * 0.5f;
            fadeBG.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), currentTime);
            if (currentTime <= 0)
            {
                break;
            }
        }
        currentTime = 0;
        fadeBG.gameObject.SetActive(false);
        light.gameObject.SetActive(true);
        StartCoroutine(LightFadeInCoroutine());
    }

    public IEnumerator LightFadeInCoroutine()
    {
        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime * 0.5f;
            light.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), currentTime);
            if (currentTime >= 1)
            {
                break;
            }
        }
        currentTime = 0;
        gm.battleController.Defeat();
    }
}
