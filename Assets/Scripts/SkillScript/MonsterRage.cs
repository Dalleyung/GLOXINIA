using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MonsterRage : MonoBehaviour
{
    public GameManager gm;
    public List<GameObject> monsterRageList;
    public GameObject prefabRage;
    public GameObject rageUI;
    public GameObject rageBackground;

    //�г����� �ݺ�Ƚ��
    public int ragecount;
    //�г����� ����Ƚ��
    public int rageclear;

    public void RagePreset()
    {
        SetTile setTile = GameManager.GetInstance().setTile;
        //�ߺ� ó�� �۾�

        int rn = Random.Range(0, 9);
        while (true)
        {
            rn = Random.Range(0, 9);
            if (rn != setTile.nowpreset)
            {
                break;
            }
        }
        Debug.Log("test" + rn);
        //���� ������ �ѹ��� random�� �ֱ�
        setTile.nowpreset = rn;

        TextAsset StagePreset = null;
        StagePreset = Resources.Load("Data/RageGaugePresetData", typeof(TextAsset)) as TextAsset;

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
                    setTile.m_ragestagepreset[ 0, x] = int.Parse(values[0]);
                    setTile.m_ragestagepreset[ 1, x] = int.Parse(values[1]);
                    setTile.m_ragestagepreset[ 2, x] = int.Parse(values[2]);
                    setTile.m_ragestagepreset[ 3, x] = int.Parse(values[3]);
                    setTile.m_ragestagepreset[ 4, x] = int.Parse(values[4]);
                    setTile.m_ragestagepreset[ 5,x] = int.Parse(values[5]);
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
                if (setTile.m_ragestagepreset[i, j] == (int)SetTile.E_TileValue.Tile)
                {
                    setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Tile;
                }
                else if (setTile.m_ragestagepreset[i, j] == (int)SetTile.E_TileValue.Disable_Tile)
                {
                    setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Fever_Disable_Tile;
                }
                else if (setTile.m_ragestagepreset[i, j] == (int)SetTile.E_TileValue.Start_Tile)
                {
                    setTile.TileList[count].GetComponent<Tile>().tileValue = (int)SetTile.E_TileValue.Start_Tile;
                }
                count++;
            }
        }
    }

    void Awake()
    {
        GameManager.GetInstance().Rage = this;

        
    }

    private void Start()
    {
        gm = GameManager.GetInstance();
        prefabRage = Resources.Load("Prefabs/" + "RageImage") as GameObject;

        InstantiateRage(prefabRage);
        ragecount = 0;
        rageclear = 0;
    }

    void Update()
    {
        UpdateRage();
    }

    public void InstantiateRage(GameObject prefabRage)
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            monsterRageList.Add(Instantiate(prefabRage));
            monsterRageList[count].transform.parent = rageUI.transform;
            monsterRageList[count].name = prefabRage.name + $"({count})";
            monsterRageList[count].transform.localPosition = Vector3.zero;
            monsterRageList[count].transform.localScale = new Vector3(1f, 0.2f, 1);
            count++;
        }
    }

    public void UpdateRage()
    {
        if (monsterRageList != null)
        {
            //���� �������� ����������Ʈ���� ũ�ٸ� Add�Ͽ� ������ ����
            if (monsterRageList.Count < gm.monster.rage)
            {
                int temp = gm.monster.rage - monsterRageList.Count;
                if (gm.monster.israge == false || gm.monster.rage == Monster.MAX_RAGE)
                {
                    
                    for (int i = 0; i < temp; i++)
                    {
                        monsterRageList.Add(Instantiate(prefabRage));
                        monsterRageList[monsterRageList.Count - 1].transform.parent = rageUI.transform;
                        monsterRageList[monsterRageList.Count - 1].name = prefabRage.name + $"({monsterRageList.Count - 1})";
                        monsterRageList[monsterRageList.Count - 1].transform.localPosition = Vector3.zero;
                        monsterRageList[monsterRageList.Count - 1].transform.localScale = new Vector3(0.65f, 0.2f, 1);
                    }
                }
            }

            //�ݴ�� ����������� ����Ʈ���� ���ٸ� remove�Ͽ� ������ ����
            else if (monsterRageList.Count > gm.monster.rage)
            {
                Destroy(monsterRageList[monsterRageList.Count - 1].gameObject);
                monsterRageList.RemoveAt(monsterRageList.Count - 1);

            }
        }
    }
}
