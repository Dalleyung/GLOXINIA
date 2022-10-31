using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public int moveStack = -1;  // �̵� Ƚ��
    public bool isStart = false;   // �Ѻ� �����ߴ���
    public bool freeze = false; //�÷��̾� ���� ����
    public GameManager gm;

    public float RasePitch = 0.05f;

    [SerializeField]
    private LayerMask tileMask;     // Ÿ�� ����ũ �޾ƿ���

    //ȭ��ȿ�� ����
    // ���� ���ϴ� �� ��
    public float minPower;
    public float maxPower;
    // �߷°�
    public float textGravity;
    // ���� �¿� �ִ밪
    public float randMax;

    void Start()
    {
        gm = GameManager.GetInstance();

        gm.soundManager.PlayBGMSound(gm.soundManager.battleBGM);
    }

    public void Move(float p_x, float p_y)   // Ȯ���� ��ǥ �ֱ�
    {
        if(freeze)
        {
            return;
        }
        Vector2 temppos = transform.position;
        
        // Ÿ�� üũ
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(temppos.x + p_x, temppos.y + p_y), Vector2.left, 0.5f);
        Tile tile = null;
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Tile")
            {
                tile = hit.collider.GetComponent<Tile>();
            }
        }
        // ������ Ÿ�� üũ
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
        // �̵� ���� ���
        gm.soundManager.PlayTileSound(gm.soundManager.tileMove);
    }

    // ������ Ÿ�� Fill �����
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
            // ��ֹ��̸� ����
            if (tile.tileValue != (int)SetTile.E_TileValue.Start_Tile)
            {
                Debug.Log("���ϵ�");
                return;
            }
            Debug.Log("���~");

            isStart = true;
            moveStack = 0;

            tile.tileValue = (int)SetTile.E_TileValue.Disable_Start_Tile;
        }
        else if (tile.tileValue == (int)SetTile.E_TileValue.Disable_Start_Tile)
        {
            //�г뽺�� ���̱�
            //gm.monster.HandleRageStack();
            // �ʱ�ȭ
            Debug.Log("���� ��ư ����");
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