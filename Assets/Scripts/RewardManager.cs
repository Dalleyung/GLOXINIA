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
            Debug.Log("�Դ´� ȿ���ߵ�");

        }
        if (ran == 1)
        {
            Debug.Log("�������� ȿ���ߵ�");

        }
        if (ran == 2)
        {
            Debug.Log("�ο�� ȿ���ߵ�");

        }
        if (ran == 3)
        {
            Debug.Log("�����ش� ȿ���ߵ�");

        }

        //ž������� �̵�
        //LoadingSceneManager.LoadScene("ExplorationScene");
    }

    public void EventDownButton()
    {
        if (ran == 0)
        {
            Debug.Log("�����Ѵ� ȿ���ߵ�");

        }
        if (ran == 1)
        {
            Debug.Log("�����Ѵ� ȿ���ߵ�");

        }
        if (ran == 2)
        {
            Debug.Log("�������� ȿ���ߵ�");

        }
        if (ran == 3)
        {
            Debug.Log("�������� ȿ���ߵ�");

        }

        //Ž������� �̵�
        //LoadingSceneManager.LoadScene("ExplorationScene");


    }
}
