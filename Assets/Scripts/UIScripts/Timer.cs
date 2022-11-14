using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameManager gm;
    public Player player;
    DateTime nowtime;
    float comparetime;
    public float limitTime;
    public float maxTime = 10f;
    public float maxRageTime = 2f;
    public bool timeover = false;
    public Scrollbar timerbar;

    void Start()
    {
        gm = GameManager.GetInstance();
        player = GameManager.GetInstance().player;
        timerbar = GetComponent<Scrollbar>();
        comparetime = DateTime.Now.Second;
        limitTime = maxTime;
    }

    void Update()
    {
        if (!gm.monster.isDie)
        {
            timecheck();
        }
    }

    void timecheck()
    {
        if(limitTime <= 0 && !timeover)
        {
            timeover = true;
            if(gm.battleController.feverOn)
            {
                gm.soundManager.PlayBGMSound(gm.soundManager.battleBGM);
                gm.battleController.feverOn = false;
                Skill.skillGauge = 0;
                gm.skill.isSkillGaugeFull = false;
                if (gm.skill.fe1 != null)
                {
                    Destroy(gm.skill.fe1);
                    Destroy(gm.skill.fe2);
                }
            }

            //if (gm.monster.israge == true)
            //{
            //    gm.monster.israge = false;
            //}

            gm.monster.AttackAnimation();
        }
      
        limitTime -= Time.deltaTime;

        if (gm.monster.israge)
        {
            timerbar.size = limitTime / maxRageTime;
        }
        else if (!gm.monster.israge)
        {
            timerbar.size = limitTime / maxTime;
        }

        //Color A = Color.Lerp(Color.red, new Color(1f, 0.5f, 0f, 1f), timerbar.size);
        //Color B = Color.Lerp(new Color(1f, 0.5f, 0f, 1f), new Color(1f, 1f, 0.6f, 1f), timerbar.size);
        //Color C = Color.Lerp(new Color(1f, 1f, 0.6f, 1f), Color.green, timerbar.size);
        //Color D = Color.Lerp(A, B, timerbar.size);
        //Color E = Color.Lerp(B, C, timerbar.size);
        //Color F = Color.Lerp(D, E, timerbar.size);

        timerbar.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
    }
}