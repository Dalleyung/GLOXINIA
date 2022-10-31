using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExTile : MonoBehaviour
{

    //enum extiletype(����, ��������, �޽�, �̺�Ʈ, ����)
    public enum exTileType
    {
        battleTile = 1, eliteTile, restTile, eventTile, bossTile
    }
    public exTileType extiletype = exTileType.battleTile;
    //�迭 stl list tree����(��Ŭ�������� ���� ����)
    //Ÿ�Ϻ�����ġ vector2(�������� ��ġ��)
    public Vector2 tilescenepos;
    //Ÿ���� ������ġ()
    public Vector2 tilepos;
    //�÷��̾ �̹� ���������� Ȯ��
    public bool ispathed;
    //�÷��̾ �������ִ°����� Ȯ��
    public bool ispath;
    //������ ����� ��ġ�� list<vector2>�迭��
    public List<Vector2> linkedTile = new List<Vector2>();
    //Ÿ�� ����Ȯ�ο�
    public Button thistile;



    // Start is called before the first frame update
    void Start()
    {
        thistile=GetComponent<Button>();
        //Ÿ�� �Ŵ������� �������� ���� Ÿ����ġ ����
        thistile.transform.position = new Vector3(tilescenepos.x, tilescenepos.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
