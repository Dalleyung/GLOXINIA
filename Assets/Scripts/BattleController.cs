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
            //�÷��̾� �ִϸ��̼� ����� attackhandler���� �־������
            gm.timer.gameObject.SetActive(true);
        }
    }

    public void AttackHandler()
    {
        Debug.Log("���� �ڵ鷯 ȣ��");
        
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
            if(gm.Rage.ragecount >= 4)
            {
                if (gm.Rage.rageclear >= 4)
                {
                    shieldOn = true;
                }
                gm.monster.AttackAnimation();
            }
            else
            {
                TileHandler();
            }
        }
        else if(feverOn)
        {
            gm.soundManager.PlayEffectSound(gm.soundManager.feverAttack);
            gm.monster.HitAnimation();
            
            //if (feverOn)
            //{
            //    //�ǹ� Ÿ�ݴ� ü��ȸ�� ��� ����
            //    Player.HP += (int)((Player.MaxHP - Player.HP) * 0.07 + 1000);
            //    if (Player.HP >= Player.MaxHP)
            //    {
            //        Player.HP = Player.MaxHP;
            //    }
            //    if (!gm.monster.isDie)
            //    {
            //        TileHandler();
            //    }
            //}
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

        // �⺻
        gm.player_move.moveStack = -1;
        gm.player_move.isStart = false;
    }

    public void AttackSuccess()
    {

        if (!gm.monster.israge)
        {
            Debug.Log("���� ����");
            


            //�ǹ��϶��� �ƴҶ� ����� ����
            if(!gm.skill.isSkillGaugeFull)
            {
                gm.player.RandDMG = Random.Range(1.00f, 1.07f);
                gm.player.TotalDMG = ((9500 * (Player.ATK + gm.player_move.moveStack * gm.player.MoveATK) / 15) * gm.player.RandDMG);
                
            }
            else
            {
                //�ǹ��϶� DMG����
                gm.player.RandDMG = Random.Range(0.9f, 1.1f);
                gm.player.TotalDMG = (5735 * gm.player.RandDMG);
            }

            gm.monster.HP -= (int)gm.player.TotalDMG;

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
            // ���� ���

            // ������ ǥ����
            gm.damageTextSpawn.MakePlayerDmgText();

            if (gm.monster.HP <= 0)
            {
                gm.monster.HP = 0;
                gm.monster.isDie = true;
            }
        }
        else
        {
            gm.Rage.rageclear++;
            gm.Rage.ragecount++;
        }

        //�ǹ�Ÿ�ӿ� ���� ����(�ƾ� ���� ����)���� ü���� ȸ���ϱ⵵ �ϰ�,
        //�ƾ� ���� �� �� Ÿ�Ͽ� AttackDelay�� �ȸ�����(TileHandler�� �۵��ع���) �ǹ�Ÿ�� �������� ����Ǳ⵵ �ؼ�
        //AttackHandler���� AttackSuccess(RaiseSkillGauge ������)��ġ�� �ű�
        //���� ������ �ִٸ� ���� �ڵ带 ����� AttackHandler�� �Ȱ��� �ڵ带 �ּ� �����Ͻø� �˴ϴ�.
        if (feverOn)
        {
            //�ǹ� Ÿ�ݴ� ü��ȸ�� ��� ����
            Player.HP += (int)((Player.MaxHP - Player.HP) * 0.07 + 1000);
            if (Player.HP >= Player.MaxHP)
            {
                Player.HP = Player.MaxHP;
            }
            if (!gm.monster.isDie)
            {
                TileHandler();
            }
        }

        if (!gm.skill.isSkillGaugeFull)
        {
            gm.skill.RaiseSkillGauge((float)gm.player_move.moveStack);
        }
    }

    public void TileHandler()
    {
        Debug.Log("Ÿ�� �ʱ�ȭ ����");
        if (gm.monster.israge)
        {
            gm.timer.limitTime = gm.timer.maxRageTime;
            if(gm.timer.timeover == true)
            {
                gm.timer.timeover = false;
            }

            if (gm.monster.swordList.Count >= 1)
            {
                if (gm.monster.spawnSwordCnt <= gm.monster.maxSwordCnt)
                {
                    gm.monster.swordList?[gm.monster.spawnSwordCnt++].gameObject.SetActive(true);
                    gm.soundManager.PlayEffectSound(gm.soundManager.createSword);
                }
            }
        }
        else if (!gm.skill.isSkillGaugeFull)
        {
            gm.timer.limitTime = gm.timer.maxTime;
        }

        gm.player_move.gameObject.transform.position = new Vector3(1000, 0, 0);
        gm.setTile.Init();

        // �⺻
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
