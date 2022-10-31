using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;



public enum PatternType
{
    NoPattern,
    Pattern1,
    Pattern2,
    UltimatePattern,


    MAX
}

public class PatternTimeLine : MonoBehaviour
{
    public PatternType[] pattern = new PatternType[6];
    int[,] PatternPos = new int[6,2];
    public GameObject NoPattern;
    public GameObject Pattern1;
    public GameObject Pattern2;
    public GameObject UltimatePattern;
    public GameObject[] PatternImage = new GameObject[6];
    RectTransform rtf;
    Transform tf;
    System.Random randomObj = new System.Random();
    int randnum;
    bool isbeulti = true;
    GameManager gm;

    public bool pattern1_On = false;
    public bool pattern2_On = false;

    void SetPatternObject()
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            if (PatternImage[i] != null)
            {
                Destroy(PatternImage[i]);
            }
        }

        for (int i = 0; i < 6; i++)
        {
            switch ((int)pattern[i])
            {
                case 0:
                    PatternImage[i] = Instantiate(NoPattern);
                    break;
                case 1:
                    PatternImage[i] = Instantiate(Pattern1);
                    break;
                case 2:
                    PatternImage[i] = Instantiate(Pattern2);
                    break;
                case 3:
                    PatternImage[i] = Instantiate(UltimatePattern);
                    break;
                default:
                    PatternImage[i] = Instantiate(NoPattern);
                    break;
            }
            PatternImage[i].transform.parent = GameObject.Find("PatternTimeLine").transform;
            PatternImage[i].transform.position = new Vector3( -PatternPos[i, 1] - 5, PatternPos[i, 0] + 7f, 0);
        }
    }

    public void PatternPull()
    {
        Debug.Log("PULL!");
        PatternType temp = pattern[0];
        for (int i = 1; i < pattern.Length; i++)
        {
            pattern[i - 1] = pattern[i];
        }
        pattern[pattern.Length - 1] = temp;

        for (int i = 0; i < 6; i++)
        {
            if (pattern[i] != PatternType.UltimatePattern)
            {
                isbeulti = false;
            }
            else
            {
                isbeulti = true;
                break;
            }
        }

        if (isbeulti == false)
        {
            pattern[5] = PatternType.UltimatePattern;
        }

        SetPatternObject();
    }


    void Start()
    {
        gm = GameManager.GetInstance();
        pattern[0] = PatternType.NoPattern;
        pattern[1] = PatternType.Pattern1;
        pattern[2] = PatternType.NoPattern;
        pattern[3] = PatternType.Pattern2;
        pattern[4] = PatternType.NoPattern;
        pattern[5] = PatternType.UltimatePattern;
        PatternPos[0, 0] = -8;
        PatternPos[0, 1] = 0;
        PatternPos[1, 0] = -8;
        PatternPos[1, 1] = -2;
        PatternPos[2, 0] = -8;
        PatternPos[2, 1] = -4;
        PatternPos[3, 0] = -8;
        PatternPos[3, 1] = -6;
        PatternPos[4, 0] = -8;
        PatternPos[4, 1] = -8;
        PatternPos[5, 0] = -8;
        PatternPos[5, 1] = -10;
        SetPatternObject();
    }

    void Update()
    {
        
    }
}
