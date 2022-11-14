using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnText : MonoBehaviour
{
    GameManager gm;
    public int maxTurn = 30;
    public int curTurn = 0;

    void Start()
    {
        gm = GameManager.GetInstance();
        MakeString();
    }
    
    public void MakeString()
    {
        curTurn++;
        if (curTurn >= maxTurn)
        {
            curTurn = maxTurn;
            gm.player.HP = 0;
            GameManager.GetInstance().player.isDie = true;
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = curTurn.ToString().PadLeft(2, '0') +
                "/" + maxTurn.ToString().PadLeft(2, '0');
    }
}
