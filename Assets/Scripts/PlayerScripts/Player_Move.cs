using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public int moveStack = -1;  // 이동 횟수
    public bool isStart = false;   // 한붓 시작했는지
    public bool freeze = false; //플레이어 조작 중지
    public GameManager gm;

    public float RasePitch = 0.05f;

    [SerializeField]
    private LayerMask tileMask;     // 타일 마스크 받아오기

    //화산효과 변수
    // 위로 향하는 힘 값
    public float minPower;
    public float maxPower;
    // 중력값
    public float textGravity;
    // 랜덤 좌우 최대값
    public float randMax;

    void Start()
    {
        gm = GameManager.GetInstance();

        gm.soundManager.PlayBGMSound(gm.soundManager.battleBGM);
    }

    public void Move(float p_x, float p_y)   // 확인할 좌표 넣기
    {
        if(freeze)
        {
            return;
        }
        Vector2 temppos = transform.position;
        
        // 타일 체크
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(temppos.x + p_x, temppos.y + p_y), Vector2.left, 0.5f);
        Tile tile = null;
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Tile")
            {
                tile = hit.collider.GetComponent<Tile>();
            }
        }
        // 지나갈 타일 체크
        if (isStart)
        {
            if (hit.collider != null)
            {
                if (tile.tileValue == (int)SetTile.E_TileValue.Bomb_Tile
                    && tile.blockCount > 0)
                {
                    return;
                }
                else if (tile.tileValue == (int)SetTile.E_TileValue.Disable_Bomb_Tile
                || tile.tileValue == (int)SetTile.E_TileValue.Disable_Tile
                || tile.tileValue == (int)SetTile.E_TileValue.Disable_Start_Tile
                || tile.tileValue == (int)SetTile.E_TileValue.Fever_Disable_Tile
                || hit.collider.tag != "Tile")
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            if (hit.collider == null)
            {
                return;
            }
        }
        gm.soundManager.Tile_audioSource.pitch += RasePitch;
        temppos = tile.transform.position;
        transform.position = temppos;
        // 이동 사운드 재생
        gm.soundManager.PlayTileSound(gm.soundManager.tileMove);
    }

    // 지나간 타일 Fill 만들기
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tile tile = collision.GetComponent<Tile>();
        
        if (isStart)
        {
            if (tile.tileValue == (int)SetTile.E_TileValue.Bomb_Tile)
            {
                //gm.GetComponent<SetTile>().BlockCountDown();

                tile.tileValue = (int)SetTile.E_TileValue.Disable_Bomb_Tile;
                moveStack++;
            }
            else if(tile.tileValue == (int)SetTile.E_TileValue.Tile)
            {
                //gm.GetComponent<SetTile>().BlockCountDown();

                tile.tileValue = (int)SetTile.E_TileValue.Disable_Tile;
                moveStack++;
            }
            else if(tile.tileValue == (int)SetTile.E_TileValue.Start_Tile)
            {
                tile.tileValue = (int)SetTile.E_TileValue.Disable_Start_Tile;
                moveStack++;
                SelectTile();
            }
        }
    }

    public void SelectTile()
    {

        if (freeze)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 1, tileMask);
        Tile tile = null;
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Tile")
            {
                tile = hit.collider.GetComponent<Tile>();
            }
        }
        if (moveStack == -1)
        {
            gm.soundManager.Tile_audioSource.pitch = 1f;
            // 장애물이면 리턴
            if (tile.tileValue != (int)SetTile.E_TileValue.Start_Tile)
            {
                Debug.Log("리턴됨");
                return;
            }
            Debug.Log("통과~");

            isStart = true;
            moveStack = 0;

            tile.tileValue = (int)SetTile.E_TileValue.Disable_Start_Tile;
        }
        else if (tile.tileValue == (int)SetTile.E_TileValue.Disable_Start_Tile)
        {
            //분노스택 줄이기
            //gm.monster.HandleRageStack();
            // 초기화
            Debug.Log("어택 버튼 누름");
            gm.battleController.AttackHandler();
        }
    }

    public void FallingTileAnim()
    {
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        float rand = Random.Range(-randMax, randMax);
        float randPow = Random.Range(minPower, maxPower);
        rigid.AddForce(gameObject.transform.up * randPow + new Vector3(rand, 0, 0), ForceMode2D.Impulse);

        rigid.gravityScale = textGravity;
    }
}