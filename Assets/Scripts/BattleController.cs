using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class BattleController : MonoBehaviour
{
    GameManager gm;

    public Button backBtn;

    public bool feverOn = false;

    public bool shieldOn = false;

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    void Update()
    {
        
    }

    public void TimeOverHandler()
    {
        if (!gm.player.isDie && !gm.monster.isDie)
        {

            TileHandler();
            gm.timer.timeover = false;
            gm.player_move.freeze = false;
            //플레이어 애니메이션 생기면 attackhandler에도 넣어줘야함
            gm.timer.gameObject.SetActive(true);
        }
    }

    public void AttackHandler()
    {
        Debug.Log("어택 핸들러 호출");
        
        AttackSuccess();

        //gm.gameObject.GetComponent<AudioSource>().pitch = 1;

        if (!gm.skill.isSkillGaugeFull && !gm.monster.israge)
        {
            gm.soundManager.PlayEffectSound(gm.soundManager.normalAttack);
            gm.monster.HitAnimation();

            //gm.monster.AttackAnimation();
        }
        else if(gm.monster.israge)
        {
            if(gm.timer.limitTime <= gm.timer.maxRageTime * 0.5 && LoadingSceneManager.currentStage == (int)LoadingSceneManager.STAGE.COW)
            {
                gm.monster.AttackDelay();
                gm.gaugeTest.gameObject.SetActive(true);
            }
            else 
            {
                shieldOn = true;
                gm.monster.AttackAnimation();
            }
        }
        else if(gm.skill.isSkillGaugeFull)
        {
            gm.soundManager.PlayEffectSound(gm.soundManager.feverAttack);
            gm.monster.HitAnimation();
            if (feverOn)
            {
                Player.HP += 5;
                if (Player.HP >= Player.MaxHP)
                {
                    Player.HP = Player.MaxHP;
                }
                if (!gm.monster.isDie)
                {
                    TileHandler();
                }
            }
        }
    }

    public void ResetHandler()
    {

        //gm.gameObject.GetComponent<AudioSource>().pitch = 1f;
        if (!gm.player_move.freeze)
        {
            gm.player_move.gameObject.transform.position = new Vector3(1000, 0, 0);
        }

        if ((gm.skill.isSkillGaugeFull || gm.monster.israge) && !gm.player_move.freeze)
        {
            int count = 0;
            for (int i = 1; i < (gm.setTile.TileList.Count / 4) + 1; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    if (gm.setTile.m_stagepreset[i, j] == (int)SetTile.E_TileValue.Tile)
                    {
                        gm.setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Tile;
                    }
                    else if (gm.setTile.m_stagepreset[i, j] == (int)SetTile.E_TileValue.Disable_Tile)
                    {
                        gm.setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Fever_Disable_Tile;
                    }
                    else if (gm.setTile.m_stagepreset[i, j] == (int)SetTile.E_TileValue.Start_Tile)
                    {
                        gm.setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Start_Tile;
                    }
                    count++;
                }
            }
        }
        else
        {
            for (int i = 0; i < gm.setTile.TileList.Count; i++)
            {
                if (gm.setTile.TileList[i].tileValue == (int)SetTile.E_TileValue.Disable_Tile)
                {
                    gm.setTile.TileList[i].tileValue = (int)SetTile.E_TileValue.Tile;
                }
                else if (gm.setTile.TileList[i].tileValue == (int)SetTile.E_TileValue.Disable_Start_Tile)
                {
                    gm.setTile.TileList[i].tileValue = (int)SetTile.E_TileValue.Start_Tile;
                }
            }
        }

        // 기본
        gm.player_move.moveStack = -1;
        gm.player_move.isStart = false;
    }

    public void AttackSuccess()
    {
        if (!gm.monster.israge)
        {
            Debug.Log("공격 성공");
            gm.monster.HP -= Player.ATK + gm.player_move.moveStack;

            if (gm.player_move.moveStack > 10 || gm.skill.isSkillGaugeFull)
            {
                gm.playerAttAnimation.attanicorutin(0);
            }
            else if (gm.player_move.moveStack > 5)
            {
                gm.playerAttAnimation.attanicorutin(1);
            }
            else
            {
                gm.playerAttAnimation.attanicorutin(2);
            }
            // 사운드 재생

            // 데미지 표현용
            gm.damageTextSpawn.MakePlayerDmgText();

            if (gm.monster.HP <= 0)
            {
                gm.monster.HP = 0;
                gm.monster.isDie = true;
                //Victory();
            }
        }

        if (!gm.skill.isSkillGaugeFull)
        {
            gm.skill.RaiseSkillGauge((float)gm.player_move.moveStack);
        }
    }

    public void TileHandler()
    {
        Debug.Log("타일 초기화 시작");
        if (gm.monster.israge)
        {
            gm.timer.limitTime = gm.timer.maxRageTime;

        }
        else if (!gm.skill.isSkillGaugeFull)
        {
            gm.timer.limitTime = gm.timer.maxTime;
        }

        gm.player_move.gameObject.transform.position = new Vector3(1000, 0, 0);
        gm.turnText.transform.GetChild(0).GetComponent<TurnText>().MakeString();
        gm.setTile.Init();

        // 기본
        gm.player_move.moveStack = -1;
        gm.player_move.isStart = false;
    }

    public void Defeat()
    {
        backBtn.gameObject.SetActive(false);
        gm.resultBtn.gameObject.SetActive(true);
        gm.resultBtn.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Stage\nFail";
        gm.resultBtn.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        gm.resultBtn.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
        gm.monster.AttackDelay();
    }
}
