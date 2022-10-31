using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossPattern : MonoBehaviour
{
    public GameManager gm;

    public Animator anim;

    public bool boss_pattern1 = false;

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    
    void Update()
    {
        
    }

    public void TileSlicePattern()
    {
        Debug.Log("타일 프리펩 로드");
        GameObject prefabTile;

        prefabTile = Resources.Load("Prefabs/" + "Tile") as GameObject;

        InstantiateTile(prefabTile);
    }

    public void InstantiateTile(GameObject prefabTile)
    {
        Debug.Log("타일 생성 중");
        int count = gm.setTile.TileList.Count;
        if (gm.setTile.TileList.Count == 20)
        {
            for(int i = gm.setTile.TileList.Count - 1; i >= 16; i--)
            {
                if (gm.setTile.TileList[i].GetComponent<Tile>().bombText != null)
                {
                    Destroy(gm.setTile.TileList[i].GetComponent<Tile>().bombText.gameObject);
                }
                Destroy(gm.setTile.TileList[i].gameObject);

                gm.setTile.TileList.RemoveAt(i);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 vector2 = prefabTile.transform.position;
                vector2.x = 0;
                vector2.y -= 3 * i;

                gm.setTile.TileList.Add(Instantiate(prefabTile.GetComponent<Tile>()));
                gm.setTile.TileList[gm.setTile.TileList.Count - 1].transform.parent = GameObject.Find("Tiles").transform;
                gm.setTile.TileList[gm.setTile.TileList.Count - 1].name = prefabTile.name + $"({count})";
                gm.setTile.TileList[gm.setTile.TileList.Count - 1].transform.position = vector2;
                gm.setTile.TileList[gm.setTile.TileList.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = 2;
                count++;
            }
        }
    }

    public void Pattern2Anim()
    {
        boss_pattern1 = !boss_pattern1;

        TileSlicePattern();
        for (int i = 0; i < gm.setTile.TileList.Count; i++)
        {
            if (gm.GetComponent<SetTile>().TileList[i].bombText != null)
            {
                gm.GetComponent<SetTile>().TileList[i].bombText.gameObject.SetActive(false);
            }
        }

        gm.player_move.gameObject.SetActive(false);
        for (int i = 0; i < gm.setTile.TileList.Count; i++)
        {
            gm.setTile.TileList[i].tileValue = 0;
        }
        anim.SetTrigger("BossPattern1");
    }
}
