using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Skill : MonoBehaviour
{
    /// <summary>
    /// 스킬게이지가 전부 채워졌을때 true
    /// 제한시간이 종료되면 false로 변경
    /// </summary>
    public bool isSkillGaugeFull;
    /// <summary>
    /// 스킬게이지 현재값(
    /// </summary>
    public static float skillGauge= 0;
    /// <summary>
    /// 스킬게이지의 최대값
    /// </summary>
    const float maxSkillGauge = 60;
    public Scrollbar skillGaugeBar;
    //이거때문에 실시간으로 게이지가 변경이 안되어서 주석
    //private void Awake()
    //{
    //    GameManager.GetInstance().skill = this;
    //}
    GameManager gm;

    public float plusSkillGauge;

    /// <summary>
    /// 피버타임 이펙트 원본
    /// </summary>
    public GameObject skillEffect;

    /// <summary>
    /// 피버타임 이펙트의 복사본을 가질 변수
    /// </summary>
    public GameObject fe1;
    public GameObject fe2;

    public GameObject cutScene;

    void Start()
    {
        gm = GameManager.GetInstance();
        skillEffect = Resources.Load<GameObject>("Prefabs/FeverEffect");
        isSkillGaugeFull = false;
        fe1 = null;
        fe2 = null;
        //skillGauge = 0;
        if (skillGaugeBar != null)
        {
            skillGaugeBar.size = 0;
        }
            
    }

    void Update()
    {
        if(skillGaugeBar != null)
        {
            skillGaugeBar.size = skillGauge / maxSkillGauge;

            Color A = Color.yellow;
            //Color A = Color.Lerp(Color.cyan, Color.blue, skillGaugeBar.size);
            //Color B = Color.Lerp(Color.blue, new Color(0f, 0f, 0.54f, 1f), skillGaugeBar.size);
            //Color C = Color.Lerp(new Color(0f, 0f, 0.54f, 1f), Color.magenta, skillGaugeBar.size);
            //Color D = Color.Lerp(A, B, skillGaugeBar.size);
            //Color E = Color.Lerp(B, C, skillGaugeBar.size);
            //Color F = Color.Lerp(D, E, skillGaugeBar.size);

            skillGaugeBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = A;
        }
    }

    /// <summary>
    /// 스킬게이지값을 올려주는 함수 (공격처리시 함께 작동)
    /// </summary>
    /// <param name="value"> 스킬게이지에 더해질 값</param>
    public void RaiseSkillGauge(float value)
    {
        //스킬게이지에 매개변수 value값 * 추가값 더하기
        if(!gm.monster.israge)
        {
            skillGauge += value * plusSkillGauge;
        }
        
        //스킬게이지 전부 채워졌을때 처리
        if (skillGauge >= maxSkillGauge && !gm.player.isDie && Player.HP >= 0)
        {
            skillGauge = maxSkillGauge;

            if (gm.monster.israge == false)
            {
                isSkillGaugeFull = true;
            }

            if (!gm.monster.isDie && !gm.monster.israge && gm.Rage.ragecount == 0)
            {
                gm.soundManager.PlayBGMSound(gm.soundManager.feverBGM);
                gm.monster.AttackDelay();
                gm.timer.gameObject.SetActive(false);
                cutScene.gameObject.SetActive(true);
                gm.battleController.feverOn = true;
            }
            else
            {
                if (gm.Rage.rageclear >= 4)
                {
                    gm.battleController.shieldOn = true;
                    gm.monster.AttackAnimation();
                }
                else if(gm.Rage.ragecount == 0)
                {
                    gm.monster.AttackAnimation();
                }
                
            }
        }
    }

    public void SkillPreset()
    {
        SetTile setTile = GameManager.GetInstance().setTile;
        //중복 처리 작업
        int rn = Random.Range(1, 10);
        while (true)
        {
            rn = Random.Range(1, 10);
            if (rn != setTile.nowpreset)
            {
                break;
            }
        }
        //현재 프리셋 넘버에 random값 넣기
        setTile.nowpreset = rn;

        TextAsset StagePreset = null;
        StagePreset = Resources.Load("Data/SkillGaugePresetData", typeof(TextAsset)) as TextAsset;

        StringReader sr = new StringReader(StagePreset.text);

        string source = sr.ReadLine();

        while (source != null)
        {
            if (int.Parse(source) != setTile.nowpreset)
            {
                for (int i = 0; i < 7; i++)
                {
                    source = sr.ReadLine();
                }
            }
            else
            {
                source = sr.ReadLine();
                for (int x = 0; x < 6; x++)
                {
                    string[] values = source.Split(' ');  // 쉼표로 구분한다. 저장시에 공백으로 구분하여 저장하였다.
                    setTile.m_stagepreset[x, 0] = int.Parse(values[0]);
                    setTile.m_stagepreset[x, 1] = int.Parse(values[1]);
                    setTile.m_stagepreset[x, 2] = int.Parse(values[2]);
                    setTile.m_stagepreset[x, 3] = int.Parse(values[3]);
                    setTile.m_stagepreset[x, 4] = int.Parse(values[4]);
                    setTile.m_stagepreset[x, 5] = int.Parse(values[5]);
                    source = sr.ReadLine();
                }
                source = null;
            }
        }
        sr.Close();

        int count = 0;
        for (int i = 1; i < (setTile.TileList.Count / 4) + 1; i++)
        {
            for (int j = 1; j < 5; j++)
            {
                if (setTile.m_stagepreset[i, j] == (int)SetTile.E_TileValue.Tile)
                {
                    setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Tile;
                }
                else if (setTile.m_stagepreset[i, j] == (int)SetTile.E_TileValue.Disable_Tile)
                {
                    setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Fever_Disable_Tile;
                }
                else if (setTile.m_stagepreset[i, j] == (int)SetTile.E_TileValue.Start_Tile)
                {
                    setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Start_Tile;
                }
                count++;
            }
        }
    }
}
