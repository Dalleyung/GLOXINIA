using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterB : Monster
{
    //����B(��)
    void Start()
    {
        gm = GameManager.GetInstance();
        anim = GetComponent<Animator>();

        type = 2;
        israge = false;
        rage = MIN_RAGE;
    }

    void Update()
    {
        UpdateHPBar();
        ChangeImage();
    }
}