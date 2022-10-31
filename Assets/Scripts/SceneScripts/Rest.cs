using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rest : MonoBehaviour
{
    public Text text;
    public GameObject adventure;
    public GameObject heal;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Heal()
    {
        //gm.player.HP = gm.player.MAX_HP;
        //Debug.Log("체력 만땅 회복 : " + gm.player.HP);
        //text.gameObject.SetActive(true);
        //heal.gameObject.SetActive(false);
        //adventure.gameObject.SetActive(true);
    }
    
    public void Adventure()
    {
        //LoadingSceneManager.LoadScene("ExplorationScene");
    }
}
