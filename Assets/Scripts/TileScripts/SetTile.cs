using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SetTile : MonoBehaviour
{
    //public StagePreset stagePreset;

    public List<Tile> TileList;
    public List<Sprite> SpriteList;
    public int row = 4; //��
    public int col = 4; //��

    //������ �������� ������ ����
    public int[,] m_stagepreset = new int[6, 6];
    public int nowpreset = 0;

    private GameManager gm;

    public enum E_TileValue { None = -1, Tile, Start_Tile, Bomb_Tile, Disable_Tile, Disable_Bomb_Tile, Disable_Start_Tile, Fever_Disable_Tile, MAX }

    void Start()
    {
        gm = GameManager.GetInstance();

        GameObject prefabTile;

        prefabTile = Resources.Load<GameObject>("Prefabs/" + "Tile");

        InstantiateTile(prefabTile);

        Init();
    }

    public void InstantiateTile(GameObject prefabTile)
    {
        int count = 0;
        for (int x = 0; x < row; x++)
        {
            Vector3 vector3 = prefabTile.transform.position ;
            for (int y = 0; y < col; y++)
            {
                vector3 = prefabTile.transform.position;
                vector3.x += 4.83f * x;
                vector3.y -= 4.85f * y;

                //Ÿ�� ���� ��ġ����
                vector3 += new Vector3(-0.6f, -1.05f, 0);

                TileList.Add(Instantiate(prefabTile.GetComponent<Tile>()));

                
                TileList[count].transform.parent = GameObject.Find("Tiles").transform;
                TileList[count].name = prefabTile.name + $"({count})";
                TileList[count].transform.position = vector3;
                TileList[count].transform.localScale = new Vector3(5, 5, 5);
                count++;
            }
        }
    }

    public void SetTileValue()
    {
        int count = 0;
        //TextAsset StagePreset = null;

        while (count < 2)
        {
            int random = Random.Range(0, TileList.Count - 1);

            if (TileList[random].tileValue != (int)E_TileValue.Start_Tile)
            {
                TileList[random].tileValue = (int)E_TileValue.Start_Tile;
                count++;
            }
        }
    }

    public void Init()
    {
        // ������� �� Ÿ�� �� ���� Ǯ�����ϱ�
        gm.player_move.freeze = false;

        int count = 0;
        if (gm.battleController.feverOn)
        {
            gm.skill.SkillPreset();
        }
        else if (gm.monster.israge)
        {
            Debug.Log("�г�Ÿ�ϱ�ü����");
            gm.Rage.RagePreset();
        }
        else
        {
            for (int i = 0; i < TileList.Count; i++)
            {
                TileList[i].tileValue = (int)E_TileValue.Tile;
            }
            SetTileValue();
        }
        for (int i = 0; i < TileList.Count; i++)
        {
            count += TileList[i].tileValue;
        }
        if (count <= 0)
        {
            Init();
        }
    }
}