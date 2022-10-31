using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private MonsterController monster;

    void Start()
    {
        monster = GetComponent<MonsterController>();
    }

    void Update()
    {
        
    }

    public void SpawnMonster()
    {
        switch(LoadingSceneManager.currentStage)
        {
            case (int)LoadingSceneManager.STAGE.COW:
                monster.gameObject.transform.GetChild((int)LoadingSceneManager.STAGE.COW)
                    .gameObject.SetActive(true);
                GameManager.GetInstance().monster = 
                    monster.gameObject.transform.GetChild((int)LoadingSceneManager.STAGE.COW)
                    .GetComponent<MonsterA>();
                break;
            case (int)LoadingSceneManager.STAGE.DEMON:
                monster.gameObject.transform.GetChild((int)LoadingSceneManager.STAGE.DEMON)
                    .gameObject.SetActive(true);
                GameManager.GetInstance().monster =
                    monster.gameObject.transform.GetChild((int)LoadingSceneManager.STAGE.DEMON)
                    .GetComponent<MonsterB>();
                break;
        }
    }
}
