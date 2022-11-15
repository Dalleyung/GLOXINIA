using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    private MonsterController monster;
    public Sprite []Sprite;
    public Sprite []NameSprite;
    public TextMeshProUGUI text;
    public Image monsterName;
    public GameObject Icon;


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


                Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                Icon.GetComponent<Image>().sprite = Sprite[0];
                monsterName.sprite = NameSprite[0];
                monsterName.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 100);
                //text.text = "Minotaurs";

                break;
            case (int)LoadingSceneManager.STAGE.DEMON:
                monster.gameObject.transform.GetChild((int)LoadingSceneManager.STAGE.DEMON)
                    .gameObject.SetActive(true);
                GameManager.GetInstance().monster =
                    monster.gameObject.transform.GetChild((int)LoadingSceneManager.STAGE.DEMON)
                    .GetComponent<MonsterB>();

                Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 100);
                Icon.GetComponent<Image>().sprite = Sprite[1];
                monsterName.sprite = NameSprite[1];
                monsterName.GetComponent<RectTransform>().sizeDelta = new Vector2(210, 100);
                //text.text = "Demon Knight";


                break;
        }
    }
}
