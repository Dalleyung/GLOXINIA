using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExPlayer : MonoBehaviour
{
    //씬에서 보이는 플레이어 위치값
    public Vector2 explayer;
    //논리적 위치값
    public Vector2 explayerpos;
    // Start is called before the first frame update
    void Start()
    {
        explayer = new Vector2(440, 75);
        explayerpos = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
