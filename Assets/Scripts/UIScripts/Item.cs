using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameManager gm;

    // Ȥ�ó� �ؼ� ���������٤���
    public int PlusHP;
    public int PlusATK;
    public int code;

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    void Update()
    {
        
    }
}
