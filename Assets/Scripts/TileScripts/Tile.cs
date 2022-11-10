using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
//using static UnityEditor.PlayerSettings;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
using Text = UnityEngine.UI.Text;

public class Tile : MonoBehaviour
{
    public int tileValue = 0;
    public int blockCount = -1;

    public TextMeshProUGUI bombText;

    private GameManager gm;

    public Camera camera;

    public GameObject button;

    public TextMeshProUGUI setBombText;

    public GameObject shield;

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
    }

    private void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        TileUpdate();
    }

    void TileUpdate()
    {
        GetComponent<SpriteRenderer>().sprite = gm.setTile.SpriteList[tileValue];
    }

    //public void MakeButton()
    //{
    //    GameObject prefabButton = Resources.Load("Prefabs/" + "TileButton") as GameObject;
    //    button = Instantiate(prefabButton);
    //    button.transform.parent = GameObject.Find("GUI").transform;
    //    button.transform.parent = GameObject.Find("TileButtonLayer").transform;
    //    button.gameObject.name = gameObject.name + "tileButton";

    //    button.transform.position = transform.position;
    //    button.transform.localScale = new Vector3(2, 2, 0);

    //    button.GetComponent<Button>().onClick.AddListener(Touch);
    //}

    public void StartTouch()
    {
        if (!gm.player_move.isStart && !gm.player_move.freeze && tileValue == (int)SetTile.E_TileValue.Start_Tile)
        {
            Debug.Log("터치");
            GameObject startTile = gameObject;
            gm.player_move.transform.position = startTile.transform.position;
            gm.player_move.SelectTile();
        }
    }

    public void SwipeTouch(Vector2 p_pos)
    {
        Debug.Log("스와이프");
        gm.player_move.Move(p_pos);
    }

    public void FallingTileAnim()
    {
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        float rand = Random.Range(-randMax, randMax);
        float randPow = Random.Range(minPower, maxPower);
        rigid.AddForce(gameObject.transform.up * randPow + new Vector3(rand, 0, 0), ForceMode2D.Impulse);

        rigid.gravityScale = textGravity;
    }

    public void TileShield()
    {
        GameObject cpyobj = Instantiate(shield);
        cpyobj.transform.position = transform.position;
        Destroy(cpyobj, 1.3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Circle"))
        {
            tileValue = 6;
        }
    }
}