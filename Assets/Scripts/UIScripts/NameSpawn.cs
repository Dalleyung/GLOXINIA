using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class NameSpawn : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Image creditBG;
    public bool flag = false;
    private bool isCreditBGOn = false;
    GameManager gm;
    public int count = 0;
    private void Start()
    {
        gm = GameManager.GetInstance();
        
    }
    private void Update()
    {
        if(flag)
        {
            flag = false;
            StartCoroutine(FadeOutCoroutine());
        }
        SkipCredit();
    }
    public void MakeNameText()
    {
        if (count < gm.credit.nameList.Count)
        {
            TextMeshProUGUI cpyText = Instantiate(text);
            cpyText.transform.parent = GameObject.Find("Canvas").transform;
            cpyText.transform.localPosition = new Vector2(0, -340);
            cpyText.transform.localScale = Vector3.one;
            cpyText.gameObject.SetActive(true);
            if(count == 0 || count == 5 || count == 10 || count == 16)
            {
                cpyText.fontSize = 42;
                cpyText.fontStyle = FontStyles.Bold;
            }
            cpyText.gameObject.GetComponent<Credit>().NameTextAnim(count++);
        }
        else
        {
            TextMeshProUGUI cpyText = Instantiate(text);
            cpyText.transform.parent = GameObject.Find("Canvas").transform;
            cpyText.transform.localPosition = new Vector2(0, -340);
            cpyText.transform.localScale = Vector3.one;
            cpyText.fontSize = 48;
            cpyText.fontStyle = FontStyles.Bold;
            cpyText.gameObject.SetActive(true);

            cpyText.gameObject.GetComponent<Credit>().NameTextAnim(count++);
        }
    }

    IEnumerator NameCoroutine()
    {
        while (true)
        {
            if(count < gm.credit.nameList.Count)
            {
                if(count == 5 || count == 10 || count == 16)
                {
                    yield return new WaitForSeconds(2.5f);
                }
                else
                {
                    yield return new WaitForSeconds(1.5f);
                }
            }
            else
            {
                yield return new WaitForSeconds(4f);
            }
            MakeNameText();
            if (count > gm.credit.nameList.Count)
            {
                count = 0;
                yield break;
            }
        }
    }

    public float currentTime = 0;
    public void StartCredit()
    {
        gm.soundManager.PlayBGMSound(gm.soundManager.credit);
        isCreditBGOn = true;
        creditBG.gameObject.SetActive(isCreditBGOn);
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;
            creditBG.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), currentTime);
            if (currentTime >= 1)
            {
                StartCoroutine(NameCoroutine());
                yield break;
            }
        }
    }
    
    public IEnumerator FadeOutCoroutine()
    {
        while (true)
        {
            yield return null;
            currentTime -= Time.deltaTime;
            creditBG.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), currentTime);
            if (currentTime <= 0)
            {
                isCreditBGOn = false;
                creditBG.gameObject.SetActive(isCreditBGOn);
                gm.soundManager.PlayBGMSound(gm.soundManager.titleBGM);
                currentTime = 0;
                yield break;
            }
        }
    }

    public void SkipCredit()
    {
        if (isCreditBGOn)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Time.timeScale = 4;
                }

                //Detects swipe after finger is released from screen
                if (touch.phase == TouchPhase.Ended)
                {
                    Time.timeScale = 1;
                }
            }
        }
    }
}
