using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Image;

    public Sprite[] image = new Sprite[4];

    int ran;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
           // Reward[i] = GameObject.Find("event"+(i+1).ToString());
        }
            ran = Random.Range(0, 4);

        Image.GetComponent<Image>().sprite = image[ran];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EventUpButton()
    {


        if(ran == 0)
        {
            Debug.Log("먹는다 효과발동");

        }
        if (ran == 1)
        {
            Debug.Log("가져간다 효과발동");

        }
        if (ran == 2)
        {
            Debug.Log("싸운다 효과발동");

        }
        if (ran == 3)
        {
            Debug.Log("도와준다 효과발동");

        }

        //탑험맵으로 이동
        //LoadingSceneManager.LoadScene("ExplorationScene");
    }

    public void EventDownButton()
    {
        if (ran == 0)
        {
            Debug.Log("무시한다 효과발동");

        }
        if (ran == 1)
        {
            Debug.Log("무시한다 효과발동");

        }
        if (ran == 2)
        {
            Debug.Log("도망간다 효과발동");

        }
        if (ran == 3)
        {
            Debug.Log("지나간다 효과발동");

        }

        //탐험맵으로 이동
        //LoadingSceneManager.LoadScene("ExplorationScene");


    }
}
