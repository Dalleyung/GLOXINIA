using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnim : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SwordAttackReady()
    {
        if (GameManager.GetInstance().monster.spawnSwordCnt == GameManager.GetInstance().monster.maxSwordCnt)
        {
            for (int i = 0; i < GameManager.GetInstance().monster.swordList.Count; i++)
            {
                GameManager.GetInstance().monster.swordList[i].transform.GetChild(0).GetComponent<Animator>().speed = 0;
            }
            GameManager.GetInstance().monster.spawnSwordCnt = 0;
        }
    }
}
