using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gm;
    public Monster monster;
    public Player_Move player_move;
    public Timer timer;
    // static으로 변경해서 씬전환을 하더라도 플레이어 스탯이 초기화가 안되도록 바꿈
    public static float MaxHP = 100000;
    public static float MaxATK = 8;
    public static float HP = 100000;
    public static float ATK = 8;
    public bool isDie;
    public Scrollbar playerHPBar;
    public GameObject HPDanger;

    public float RandDMG;
    public float TotalDMG;
    public float MoveATK = 0.47f;

    public bool P_Fail;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        monster = gm.monster;
        player_move = gm.player_move;
        player_move = gm.player_move;
        timer = gm.timer;
        if (playerHPBar != null)
        {
            playerHPBar.size = HP/MaxHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    // 다른곳에서 호출하는 방식의 함수로 수정함
    public void Player_Monster_Attack()
    {
        monster.HP -= ATK;
        Debug.Log("공격성공");
        Debug.Log(monster.HP.ToString());
        if (monster.HP <= 0)
        {
            monster.HP = 0;
            monster.isDie = true;
        }
    }
    public void UpdateText()
    {
        if (playerHPBar != null && HP >= -1)
        {
            playerHPBar.size = HP / MaxHP;
        }
    }


}