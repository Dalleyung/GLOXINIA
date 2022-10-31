using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExTile : MonoBehaviour
{

    //enum extiletype(전투, 정예전투, 휴식, 이벤트, 보스)
    public enum exTileType
    {
        battleTile = 1, eliteTile, restTile, eventTile, bossTile
    }
    public exTileType extiletype = exTileType.battleTile;
    //배열 stl list tree구조(맵클래스에서 따로 생성)
    //타일본인위치 vector2(씬에서의 위치값)
    public Vector2 tilescenepos;
    //타일의 논리적위치()
    public Vector2 tilepos;
    //플레이어가 이미 지난곳인지 확인
    public bool ispathed;
    //플레이어가 지날수있는곳인지 확인
    public bool ispath;
    //앞으로 연결된 위치값 list<vector2>배열로
    public List<Vector2> linkedTile = new List<Vector2>();
    //타일 본인확인용
    public Button thistile;



    // Start is called before the first frame update
    void Start()
    {
        thistile=GetComponent<Button>();
        //타일 매니저에서 받은값을 토대로 타일위치 조정
        thistile.transform.position = new Vector3(tilescenepos.x, tilescenepos.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
