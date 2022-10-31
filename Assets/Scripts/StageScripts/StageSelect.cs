using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public ExTile thisbutton;
    public ExTileManager em;
    //스테이지 선택
    public void onClickStageSelect()
    {
        if (thisbutton.ispath == true)
        {
            thisbutton.ispathed = true;
            int p_x = 0;
            int p_y = 0;
            int x = 0;
            int y = 0;
            em.explayer.explayerpos.x = thisbutton.tilepos.x;
            em.explayer.explayerpos.y = thisbutton.tilepos.y;
            em.explayer.explayer.x = thisbutton.tilescenepos.x;
            em.explayer.explayer.y = thisbutton.tilescenepos.y;
            p_x = (int)(em.explayer.explayerpos.x-1);
            p_y = (int)(em.explayer.explayerpos.y-1);
       
            for (int i = 0; i < em.tilemap[p_y][p_x].linkedTile.Count; i++)
            {
                x = (int)(em.tilemap[p_y][p_x].linkedTile[i].x-1);
                y = (int)(em.tilemap[p_y][p_x].linkedTile[i].y-1);
                em.tilemap[y][x].ispath = true;
                Debug.Log(em.tilemap[y][x].ispath.ToString());
                Debug.Log(x.ToString());
                Debug.Log(y.ToString());
            }

            em.saveSceneData();

            //switch (thisbutton.extiletype)
            //{
            //    case ExTile.exTileType.battleTile:
            //        LoadingSceneManager.LoadScene("BossScene");
            //        break;
            //    case ExTile.exTileType.eliteTile:
            //        LoadingSceneManager.LoadScene("BossScene");
            //        break;
            //    case ExTile.exTileType.eventTile:
            //        LoadingSceneManager.LoadScene("EventScene");
            //        break;
            //    case ExTile.exTileType.restTile:
            //        LoadingSceneManager.LoadScene("RestScene");
            //        break;
            //    case ExTile.exTileType.bossTile:
            //        LoadingSceneManager.LoadScene("BossScene");
            //        break;
            //}
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        thisbutton=GetComponent<ExTile>();
        em = GameObject.Find("EXTileManager").GetComponent<ExTileManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
