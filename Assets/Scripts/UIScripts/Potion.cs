using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int hpPotion = 5;
    public int atkPotion = 5;
    public int maxHpBuff = 10;
    public int maxAtkBuff = 3;

    private GameManager gm;

    public CoolTime coolTime;

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    void Update()
    {

    }

    public void UseHpPotion()
    {
        if(coolTime.isEnded)
        {
            gm.player.HP += hpPotion;
        }
    }

    public void UseAtkPotion()
    {
        //������ ���� �� �ٽ� ���� �Ա� �� ���ݷ����� ���ƿ;���
        if (coolTime.isEnded)
        {
            Debug.Log("����!");
            gm.player.ATK += atkPotion;
        }
    }

    void UseHpBuff()
    {
        //���� ��ø���� ���� ü���� �÷�����
        gm.player.MaxHP += maxHpBuff;
    }

    void UseAtkBuff()
    {
        //���� ��ø���� ���� ���ݷ��� �÷�����
        gm.player.MaxATK += maxAtkBuff;
    }
}
