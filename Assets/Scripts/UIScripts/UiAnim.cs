using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnim : MonoBehaviour
{
    public List<GameObject> stagePoint;
    
    GameManager gm;
  
    void Start()
    {
        gm = GameManager.GetInstance();

        gm.monsterController.SpawnMonster();
    }

    
    void Update()
    {
        
    }

    private void StageStartEvent()
    {
        gm.monster.monsterHPBar.gameObject.SetActive(true);
        gm.timer.timerbar.gameObject.SetActive(true);
        gm.player.playerHPBar.gameObject.SetActive(true);
        gm.skill.skillGaugeBar.gameObject.SetActive(true);
        gm.Rage.rageUI.gameObject.SetActive(true);
        gm.Rage.rageBackground.gameObject.SetActive(true);
        
        for (int i = 0; i < gm.setTile.TileList.Count; i++)
        {
            gm.setTile.TileList[i].gameObject.SetActive(true);
        }

        gm.monster.HPCheck();
    }

    private void StagePointEvent(int p_num)
    {
        switch(p_num)
        {
            case 0:
                stagePoint[LoadingSceneManager.currentStage].SetActive(true);
                break;
            case 1:
                stagePoint[LoadingSceneManager.currentStage].SetActive(false);
                break;
        }
    }
}
