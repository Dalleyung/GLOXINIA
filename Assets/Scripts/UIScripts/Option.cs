using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameObject option;   // �ɼ�â

    private bool pause = false;   // ���� �Ͻ�����

    GameManager gm;

    public List<GameObject> obj;    // �ɼ� ������ ������͵�

    public void Start()
    {
        gm = GameManager.GetInstance();            
    }

    public void OpenOption()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        pause = !pause;

        gm.player_move?.gameObject.SetActive(!pause);

        option.SetActive(pause);

        // ��ư ����
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(!pause);
        }
    }

    public void YesBtn()
    {
        Time.timeScale = 1;
        LoadingSceneManager.currentStage = (int)LoadingSceneManager.STAGE.MAIN;
        LoadingSceneManager.NowStage(LoadingSceneManager.currentStage);
    }

    public void NoBtn()
    {
        OpenOption();
    }
}
