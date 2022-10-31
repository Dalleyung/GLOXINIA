using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager gm;
    private void Start()
    {
        gm = GameManager.GetInstance();
        gm.soundManager.PlayBGMSound(gm.soundManager.titleBGM);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Exit();
        }
    }



    public void Tutorial()
    {
        //tutorial Scene 이동
        //LoadingSceneManager.LoadScene("BossScene_Beta");
    }

    public void Credit()
    {
        //Credit Scene 이동
        //LoadingSceneManager.LoadScene("BossScene_Beta");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
