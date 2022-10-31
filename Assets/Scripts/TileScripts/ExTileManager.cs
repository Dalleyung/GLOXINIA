using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExTileManager : MonoBehaviour
{
    //ĵ���� ������Ʈ ������
    public Canvas exscenecanvas;
    //��� �̹��� ������
    public Image backbgroundimage;
    //ĵ���� �����Ϸ� ������
    public CanvasScaler scaler;

    //Ÿ�Ͼ������� ��ġ��[y,x]
    public  List<List<Vector2>> tilescenepos = new List<List<Vector2>>();
    
    //Ÿ���� ���� ��ġ��[y,x]
    public Vector2[,] tilepos = new Vector2[7,3];
    
    //Ÿ��Ÿ��
    public ExTile.exTileType[,] type =new ExTile.exTileType[7,3];
    
    //�ν��Ͻ��� ���� ������
    public ExTile BattleTile;
    public ExTile EliteTile;
    public ExTile RestTile;
    public ExTile EventTile;
    public ExTile BossTile;

    //���� ����Ʈ�� �迭���޸� [y][x]�������� �����ؾ��մϴ�
    public List<List<ExTile>> tilemap = new List<List<ExTile>>();
    //���� ������Ʈ������
    public GameObject exTileManagerobj;
    public ExPlayer explayer;


    //Ÿ�� �ӽ���ġ���� ���� �����µ����Ϳ��� �ҷ����°ɷ� �缳��
    void setExTile()
    {
        tilescenepos.Add(new List<Vector2>());
        tilescenepos[0].Add(new Vector2(130, 340));
        tilepos[0,0].x = 1;
        tilepos[0,0].y = 1;
        type[0, 0] = ExTile.exTileType.battleTile;

        tilescenepos[0].Add(new Vector2(440, 360));
        tilepos[0,1].x = 2;
        tilepos[0,1].y = 1;
        type[0, 1] = ExTile.exTileType.eventTile;

        tilescenepos[0].Add(new Vector2(980, 370));
        tilepos[0,2].x = 3;
        tilepos[0,2].y = 1;
        type[0, 2] = ExTile.exTileType.battleTile;

        tilescenepos.Add(new List<Vector2>());
        tilescenepos[1].Add(new Vector2(440, 620));
        tilepos[1,0].x = 1;
        tilepos[1,0].y = 2;
        type[1, 0] = ExTile.exTileType.eventTile;

        tilescenepos[1].Add(new Vector2(980, 620));
        tilepos[1,1].x = 2;
        tilepos[1,1].y = 2;
        type[1, 1] = ExTile.exTileType.battleTile;

        tilescenepos.Add(new List<Vector2>());
        tilescenepos[2].Add(new Vector2(130, 830));
        tilepos[2,0].x = 1;
        tilepos[2,0].y = 3;
        type[2, 0] = ExTile.exTileType.battleTile;

        tilescenepos[2].Add(new Vector2(440, 830));
        tilepos[2,1].x = 2;
        tilepos[2,1].y = 3;
        type[2, 1] = ExTile.exTileType.eliteTile;

        tilescenepos[2].Add(new Vector2(990, 910));
        tilepos[2,2].x = 3;
        tilepos[2,2].y = 3;
        type[2, 2] = ExTile.exTileType.restTile;

        tilescenepos.Add(new List<Vector2>());
        tilescenepos[3].Add(new Vector2(300, 1100));
        tilepos[3,0].x = 1;
        tilepos[3,0].y = 4;
        type[3, 0] = ExTile.exTileType.restTile;

        tilescenepos[3].Add(new Vector2(740, 1100));
        tilepos[3,1].x = 2;
        tilepos[3,1].y = 4;
        type[3, 1] = ExTile.exTileType.eventTile;

        tilescenepos.Add(new List<Vector2>());
        tilescenepos[4].Add(new Vector2(130, 1350));
        tilepos[4, 0].x = 1;
        tilepos[4, 0].y = 5;
        type[4, 0] = ExTile.exTileType.battleTile;

        tilescenepos[4].Add(new Vector2(560, 1420));
        tilepos[4,1].x = 2;
        tilepos[4,1].y = 5;
        type[4, 1] = ExTile.exTileType.eventTile;

        tilescenepos[4].Add(new Vector2(980, 1350));
        tilepos[4,2].x = 3;
        tilepos[4,2].y = 5;
        type[4, 2] = ExTile.exTileType.eliteTile;

        tilescenepos.Add(new List<Vector2>());
        tilescenepos[5].Add(new Vector2(130, 1660));
        tilepos[5,0].x = 1;
        tilepos[5,0].y = 6;
        type[5, 0] = ExTile.exTileType.battleTile;

        tilescenepos[5].Add(new Vector2(560, 1660));
        tilepos[5,1].x = 2;
        tilepos[5,1].y = 6;
        type[5, 1] = ExTile.exTileType.restTile;

        tilescenepos[5].Add(new Vector2(990, 1660));
        tilepos[5,2].x = 3;
        tilepos[5,2].y = 6;
        type[5, 2] = ExTile.exTileType.restTile;

        tilescenepos.Add(new List<Vector2>());
        tilescenepos[6].Add(new Vector2(550, 1980));
        tilepos[6,0].x = 1;
        tilepos[6,0].y = 7;
        type[6, 0] = ExTile.exTileType.bossTile;
    }


    void setLinkedTile()
    {
        tilemap[0][0].linkedTile.Add(new Vector2(1, 2));
        tilemap[0][0].linkedTile.Add(new Vector2(1, 3));
        tilemap[0][1].linkedTile.Add(new Vector2(1, 2));
        tilemap[0][2].linkedTile.Add(new Vector2(2, 2));
        tilemap[1][0].linkedTile.Add(new Vector2(2, 3));
        tilemap[1][1].linkedTile.Add(new Vector2(3, 3));
        tilemap[2][0].linkedTile.Add(new Vector2(1, 4));
        tilemap[2][1].linkedTile.Add(new Vector2(1, 4));
        tilemap[2][1].linkedTile.Add(new Vector2(2, 4));
        tilemap[2][2].linkedTile.Add(new Vector2(2, 4));
        tilemap[3][0].linkedTile.Add(new Vector2(1, 5));
        tilemap[3][0].linkedTile.Add(new Vector2(2, 5));
        tilemap[3][1].linkedTile.Add(new Vector2(2, 5));
        tilemap[3][1].linkedTile.Add(new Vector2(3, 5));
        tilemap[4][0].linkedTile.Add(new Vector2(1, 6));
        tilemap[4][0].linkedTile.Add(new Vector2(2, 6));
        tilemap[4][1].linkedTile.Add(new Vector2(2, 6));
        tilemap[4][2].linkedTile.Add(new Vector2(3, 6));
        tilemap[5][0].linkedTile.Add(new Vector2(1, 7));
        tilemap[5][1].linkedTile.Add(new Vector2(1, 7));
        tilemap[5][2].linkedTile.Add(new Vector2(1, 7));

    }

    void exTileActive()
    {
        if(SceneManager.GetActiveScene().name != "ExplorationScene")
        {
            exTileManagerobj.gameObject.SetActive(false);
        }
    }

    //������������(�÷��̾�� �����ؾ���)
    public void saveSceneData()
    {
        string key;
        key = "exPlayerposx";
        PlayerPrefs.SetInt(key, (int)explayer.explayerpos.x);
        key = "exPlayerposy";
        PlayerPrefs.SetInt(key, (int)explayer.explayerpos.y);

        key = "exPlayersposx";
        PlayerPrefs.SetInt(key, (int)explayer.explayer.x);
        key = "exPlayersposy";
        PlayerPrefs.SetInt(key, (int)explayer.explayer.y);

        key = "ispathed";
        for (int i = 0; i < tilemap.Count; i++)
        {
            for (int j = 0; j < tilemap[i].Count; j++)
            {
                PlayerPrefs.SetInt(key + i.ToString(), Convert.ToInt32(tilemap[i][j].ispathed));

            }
        }
    }

    public void loadSceneData()
    {
        string key;
        key = "exPlayerposx";
        //explayer.explayerpos.x = PlayerPrefs.GetInt(key);
        int p_x = PlayerPrefs.GetInt(key);
        key = "exPlayerposy";
        //explayer.explayerpos.y = PlayerPrefs.GetInt(key);
        int p_y = PlayerPrefs.GetInt(key);

        if (explayer.explayerpos.x != 0 && explayer.explayerpos.y != 0)
        {
            p_x = (int)explayer.explayerpos.x - 1;
            p_y = (int)explayer.explayerpos.y - 1;

            key = "exPlayersposx";
            explayer.explayer.x = PlayerPrefs.GetInt(key);
            key = "exPlayersposy";
            explayer.explayer.y = PlayerPrefs.GetInt(key);

            explayer.transform.position = new Vector3(explayer.explayer.x, explayer.explayer.y, 0);
            int l_x;
            int l_y;

            for (int i = 0; i < tilemap[p_y][p_x].linkedTile.Count; i++)
            {
                l_x = (int)tilemap[p_y][p_x].linkedTile[i].x - 1;
                l_y = (int)tilemap[p_y][p_x].linkedTile[i].y - 1;
                tilemap[l_x][l_y].ispath = true;
            }

            key = "ispathed";
            for (int i = 0; i < tilemap.Count; i++)
            {
                for (int j = 0; j < tilemap[i].Count; j++)
                {
                    tilemap[i][j].ispathed = Convert.ToBoolean(PlayerPrefs.GetInt(key + i.ToString()));
                }
            }
        }
        else if(explayer.explayerpos.x == 0 && explayer.explayerpos.y == 0)
        {
            tilemap[0][0].ispath = true;
            tilemap[0][1].ispath = true;
            tilemap[0][2].ispath = true;
            explayer.transform.position = new Vector3(explayer.explayer.x, explayer.explayer.y, 0);
        }
    }

    //���ӸŴ������� ĵ�������ο� �ִ� extilemanager�� �������ְ� ��� ����־����
    /*public static ExTileManager exTileManager;
    private void Awake()
    {
        DontDestroyOnLoad(exTileManagerobj);

        if(exTileManager == null)
        {
            exTileManager = GetComponent<ExTileManager>();
        }
        else if(exTileManager != this)
        {
            Destroy(this);
        }
    }*/

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //���ũ�⸦ ĵ������ �°� �ʱ�ȭ
        backbgroundimage.rectTransform.sizeDelta = new Vector2(scaler.referenceResolution.x,
            scaler.referenceResolution.y);
        //�����ġ�� ĵ������ �°� �ʱ�ȭ
        backbgroundimage.rectTransform.position = new Vector3(exscenecanvas.transform.position.x,
            exscenecanvas.transform.position.y, exscenecanvas.transform.position.z);

        setExTile();
        for (int i = 0; i < tilescenepos.Count; i++)
        {
            tilemap.Add(new List<ExTile>());

            for (int j=0; j < tilescenepos[i].Count; j++)
            {
                //Ÿ�ϻ���(ĵ���� ���ο��� ����)
                switch (type[i, j])
                {
                    case ExTile.exTileType.battleTile:
                        tilemap[i].Add(Instantiate(BattleTile, exTileManagerobj.transform.position,
                            Quaternion.identity, exTileManagerobj.transform));
                        break;
                    case ExTile.exTileType.eliteTile:
                        tilemap[i].Add(Instantiate(EliteTile, exTileManagerobj.transform.position,
                            Quaternion.identity, exTileManagerobj.transform));
                        break;
                    case ExTile.exTileType.eventTile:
                        tilemap[i].Add(Instantiate(EventTile, exTileManagerobj.transform.position,
                            Quaternion.identity, exTileManagerobj.transform));
                        break;
                    case ExTile.exTileType.restTile:
                        tilemap[i].Add(Instantiate(RestTile, exTileManagerobj.transform.position,
                            Quaternion.identity, exTileManagerobj.transform));
                        break;
                    case ExTile.exTileType.bossTile:
                        tilemap[i].Add(Instantiate(BossTile, exTileManagerobj.transform.position,
                            Quaternion.identity, exTileManagerobj.transform));
                        break;
                }
                //�������� ��ġ�� �Ҵ�
                tilemap[i][j].tilescenepos = this.tilescenepos[i][j];
                //���� ��ġ�� �Ҵ�
                tilemap[i][j].tilepos = new Vector2(tilepos[i, j].y, tilepos[i, j].x);
                tilemap[i][j].extiletype = type[i, j];

            }
        }
        setLinkedTile();
        //loadSceneData();

        tilemap[0][0].ispath = true;
        tilemap[0][1].ispath = true;
        tilemap[0][2].ispath = true;
    }

    void Update()
    {
        exTileActive();
        
    }
}
