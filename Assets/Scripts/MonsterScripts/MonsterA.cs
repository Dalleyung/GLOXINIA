using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MonsterA : Monster
{
    //����A(����)
    void Start()
    {
        gm = GameManager.GetInstance();
        anim = GetComponent<Animator>();

        type = 1;
        israge = false;
        


        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPBar();
        ChangeImage();
        TestRageAttack();
    }
}
