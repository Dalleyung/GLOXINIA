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
    public Image attImage;

    public IEnumerator AttAnimation(int num)
    {
        int curruntNum = 0;
        attImage.color = new Color(1, 1, 1, 1);

        switch (num)
        {
            case 0:
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
                    yield return new WaitForSeconds(0.1f);
                }
            case 1:
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
                    yield return new WaitForSeconds(0.1f);
                }
            case 2:
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
                    yield return new WaitForSeconds(0.1f);
                }
        }
        yield break;
    }

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
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
