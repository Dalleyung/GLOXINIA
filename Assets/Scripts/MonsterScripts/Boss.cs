using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : Monster
{
    //º¸½º
    void Start()
    {
        type = 0;
        HP = 50;
        ATK = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //Monster_Player_Attack();
        ChangeImage();
    }
}