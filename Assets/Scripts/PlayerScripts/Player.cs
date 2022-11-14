using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public GameManager gm;
    public Monster monster;
    public Player_Move player_move;
    public Timer timer;
    // static���� �����ؼ� ����ȯ�� �ϴ��� �÷��̾� ������ �ʱ�ȭ�� �ȵǵ��� �ٲ�
    public static float S_HP = 100000;
    public static float S_MaxHP = 100000;
    public static float S_MaxATK = 8;
    public static float S_ATK = 8;
    public float MaxHP = 100000;
    public float MaxATK = 8;
    public float HP;
    public float ATK = 8;
    public bool isDie;
    public Scrollbar playerHPBar;
    public Scrollbar playerHPBar2;
    public GameObject HPDanger;

    public float RandDMG;
    public float TotalDMG;
    public float MoveATK = 0.47f;

    public bool P_Fail;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        HP = S_HP;
        gm = GameManager.GetInstance();
        monster = gm.monster;
        player_move = gm.player_move;
        player_move = gm.player_move;
        timer = gm.timer;
        if (playerHPBar != null)
        {
            playerHPBar.size = HP/MaxHP;
        }
        if (playerHPBar2 != null)
            playerHPBar2.size = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    // �ٸ������� ȣ���ϴ� ����� �Լ��� ������
    public void Player_Monster_Attack()
    {
        monster.HP -= ATK;
        Debug.Log("���ݼ���");
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
        if(playerHPBar2 != null && HP >= -1)
        {
            time += Time.deltaTime* 0.0007f;
                playerHPBar2.size  = Mathf.Lerp(playerHPBar2.size, playerHPBar.size, time);

                if(time >= 1)
                    time = 0;
        }
    }


}