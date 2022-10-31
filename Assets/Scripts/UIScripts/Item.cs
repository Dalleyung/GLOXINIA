using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameManager gm;

    // 혹시나 해서 만들었스빈다ㅎㅎ
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
