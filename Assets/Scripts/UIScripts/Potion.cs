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
        //공격이 끝난 후 다시 포션 먹기 전 공격력으로 돌아와야함
        if (coolTime.isEnded)
        {
            Debug.Log("공증!");
            gm.player.ATK += atkPotion;
        }
    }

    void UseHpBuff()
    {
        //버프 중첩수에 따라 체력을 늘려야함
        gm.player.MaxHP += maxHpBuff;
    }

    void UseAtkBuff()
    {
        //버프 중첩수에 따라 공격력을 늘려야함
        gm.player.MaxATK += maxAtkBuff;
    }
}
