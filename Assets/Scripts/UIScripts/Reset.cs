using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    public static int monster = 3;
    public static int boss = 5;

    public int resetChance = boss;
    public Text resetText;

    private void Update()
    {
        resetText.text = resetChance.ToString();
    }

    public void TileResetButton()
    {
        if(resetChance > 0 && GameManager.GetInstance().player_move.isStart)
        {
            resetChance--;
            GameManager.GetInstance().battleController.ResetHandler();
        }
    }
}
