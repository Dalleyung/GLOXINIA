using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttAnimation : MonoBehaviour
{
    public List<Sprite> hattSpritelist = new List<Sprite>();
    public List<Sprite> mattSpritelist = new List<Sprite>();
    public List<Sprite> lattSpritelist = new List<Sprite>();

    GameManager gm;

    GameObject attObj;
    /// <summary>
    /// 숫자가 작을수록 빨라짐
    /// </summary>
    public float animSpeed = 0.15f;
    public Image attImage;
    RectTransform imgrt;
    

    
    public IEnumerator AttAnimation(int num)
    {
        int curruntNum = 0;
        attImage.color = new Color(1, 1, 1, 1);
        
        switch (num)
        {
            case 0:
                imgrt.sizeDelta = new Vector2(700, 700);
                imgrt.localPosition = new Vector3(0, 320, 0);
                //attImage.rectTransform.sizeDelta = new Vector2(700, 400);
                while (true)
                {
                    attImage.sprite = hattSpritelist[curruntNum];
                    curruntNum++;
                    if (curruntNum == 7)
                    {
                        attImage.sprite = null;
                        attImage.color = Color.clear;
                        yield break;
                    }
                    yield return new WaitForSeconds(animSpeed);
                }
            case 1:
                imgrt.localPosition = new Vector3(0, 380, 0);
                imgrt.sizeDelta = new Vector2(700, 400);
                //attImage.rectTransform.sizeDelta = new Vector2(700, 400);
                while (true)
                {
                    attImage.sprite = mattSpritelist[curruntNum];
                    curruntNum++;
                    if (curruntNum == 8)
                    {
                        attImage.sprite = null;
                        attImage.color = Color.clear;
                        yield break;
                    }
                    yield return new WaitForSeconds(animSpeed);
                }
            case 2:
                imgrt.localPosition = new Vector3(0, 380, 0);
                imgrt.sizeDelta = new Vector2(700, 400);
                //attImage.rectTransform.sizeDelta = new Vector2(700, 600);
                while (true)
                {
                    attImage.sprite = lattSpritelist[curruntNum];
                    curruntNum++;
                    if (curruntNum == 8)
                    {
                        attImage.sprite = null;
                        attImage.color = Color.clear;
                        yield break;
                    }
                    yield return new WaitForSeconds(animSpeed);
                }
        }
        yield break;
    }

    /// <summary>
    /// 플레이어 공격애니매이션 함수
    /// </summary>
    /// <param name="num">
    /// 0번 강공, 1번 중간, 2번 약공
    /// </param>
    /// <returns></returns>
    public void attanicorutin(int num)
    {
        StartCoroutine(AttAnimation(num));
    }

    void SetSpriteList()
    {
        for (int i = 1; i <= 7; i++)
        {
            hattSpritelist.Add(Resources.Load<Sprite>("Sprite/Attack/HighAttack/" + $"{i}"));
        }

        for (int j = 0; j < 2; j++)
        {
            for (int i = 1; i <= 8; i++)
            {
                if (j == 0)
                {
                    mattSpritelist.Add(Resources.Load<Sprite>("Sprite/Attack/MediumAttack/" + $"{i}"));
                }
                else
                {
                    lattSpritelist.Add(Resources.Load<Sprite>("Sprite/Attack/LowAttack/" + $"{i}"));
                }
            }
        }
    }
    private void Awake()
    {
        gm = GameManager.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetSpriteList();
        attImage.color = Color.clear;
        animSpeed = 0.1f;
        imgrt = attImage.rectTransform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
