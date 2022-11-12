using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Skill : MonoBehaviour
{
    /// <summary>
    /// ��ų�������� ���� ä�������� true
    /// ���ѽð��� ����Ǹ� false�� ����
    /// </summary>
    public bool isSkillGaugeFull;
    /// <summary>
    /// ��ų������ ���簪(
    /// </summary>
    public static float skillGauge= 0;
    /// <summary>
    /// ��ų�������� �ִ밪
    /// </summary>
    const float maxSkillGauge = 60;
    public Scrollbar skillGaugeBar;
    //�̰Ŷ����� �ǽð����� �������� ������ �ȵǾ �ּ�
    //private void Awake()
    //{
    //    GameManager.GetInstance().skill = this;
    //}
    GameManager gm;

    public float plusSkillGauge;

    /// <summary>
    /// �ǹ�Ÿ�� ����Ʈ ����
    /// </summary>
    public GameObject skillEffect;

    /// <summary>
    /// �ǹ�Ÿ�� ����Ʈ�� ���纻�� ���� ����
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
    /// ��ų���������� �÷��ִ� �Լ� (����ó���� �Բ� �۵�)
    /// </summary>
    /// <param name="value"> ��ų�������� ������ ��</param>
    public void RaiseSkillGauge(float value)
    {
        //��ų�������� �Ű����� value�� * �߰��� ���ϱ�
        if(!gm.monster.israge)
        {
            skillGauge += value * plusSkillGauge;
        }
        
        //��ų������ ���� ä�������� ó��
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
        //�ߺ� ó�� �۾�
        int rn = Random.Range(1, 10);
        while (true)
        {
            rn = Random.Range(1, 10);
            if (rn != setTile.nowpreset)
            {
                break;
            }
        }
        //���� ������ �ѹ��� random�� �ֱ�
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
                    string[] values = source.Split(' ');  // ��ǥ�� �����Ѵ�. ����ÿ� �������� �����Ͽ� �����Ͽ���.
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
