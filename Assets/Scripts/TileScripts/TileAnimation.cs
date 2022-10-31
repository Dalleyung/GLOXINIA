using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileAnimation : MonoBehaviour
{
    GameManager gm;
    
    public Player_Move player_move;

    private void Start()
    {
        gm = GameManager.GetInstance();
    }

    public void BossPatternEvent(int n)
    {
        switch (n)
        {
            case 1:
                player_move.gameObject.SetActive(true);
                for(int i = 0; i < gm.GetComponent<SetTile>().TileList.Count; i++)
                {
                    Debug.Log("i = " + i + "count = " + gm.GetComponent<SetTile>().TileList.Count);
                    if(gm.GetComponent<SetTile>().TileList[i].blockCount != -1)
                    {
                        gm.GetComponent<SetTile>().TileList[i].bombText.gameObject.SetActive(true);
                    }
                }
                gm.GetComponent<SetTile>().Init();
                break;
        }
    }
}
