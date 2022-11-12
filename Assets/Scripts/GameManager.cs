using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public BossPattern bossPattern;
    //public Reset resetButton;
    //public Inventory inventory;
    //public TextMeshProUGUI bombText;
    public Camera camera;
    public Player_Move player_move;
    public Monster monster;
    public Player player;
    public Timer timer;
    public SetTile setTile;
    public BattleController battleController;
    public SoundManager soundManager;
    public Skill skill;
    public DamageTextSpawn damageTextSpawn;
    public NameSpawn nameSpawn;
    public Credit credit;
    public GameObject resultBtn;
    public MonsterRage Rage;
    public GameOver gameOver;
    public MonsterController monsterController;
    public BezierCurve bezierCurve;
    public CameraShake cameraShake;
    public PlayerAttAnimation playerAttAnimation;
    
    public GameObject stage;

    static GameManager instance;
    public static GameManager GetInstance() { Init(); return instance; }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        camera = Camera.main;
    }

    void Update()
    {
        InputBackKey();
    }

    void InputBackKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && 
            LoadingSceneManager.currentStage != (int)LoadingSceneManager.STAGE.MAIN &&
            !player_move.freeze)
        {
            GetComponent<Option>().OpenOption();
        }
    }

    //public void Adventure()
    //{
    //    LoadingSceneManager.LoadScene("ExplorationScene");
    //}

    public void MainMenu()
    {
        LoadingSceneManager.currentStage = (int)LoadingSceneManager.STAGE.MAIN;
        LoadingSceneManager.NowStage(LoadingSceneManager.currentStage);
    }

    public void GoNextStage()
    {
        if (!battleController.feverOn)
        {
            Skill.skillGauge *= 0.7f;
            skill.isSkillGaugeFull = false;
        }
        else if (battleController.feverOn)
        {
            Skill.skillGauge = 0;
            skill.isSkillGaugeFull = false;
            battleController.feverOn = false;
            
        }
        
        LoadingSceneManager.NowStage(++LoadingSceneManager.currentStage);
    }

    public void GameStart()
    {
        Player.HP = Player.MaxHP;
        Skill.skillGauge = 0;
        //스테이지 변수 관련 생성되면 넣어줘야함
        LoadingSceneManager.NowStage(++LoadingSceneManager.currentStage);
    }

    public void BossStart()
    {
        Player.HP = Player.MaxHP;
        Skill.skillGauge = 0;
        //스테이지 변수 관련 생성되면 넣어줘야함
        LoadingSceneManager.currentStage = (int)LoadingSceneManager.STAGE.DEMON;
        LoadingSceneManager.NowStage((int)LoadingSceneManager.STAGE.DEMON);
    }

    static void Init()
    {
        if (instance == null)
        {
            //@Managers 가 존재하는지 확인
            GameObject go = GameObject.Find("GameManager");
            //없으면 생성
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
            }
            if (go.GetComponent<GameManager>() == null)
            {
                go.AddComponent<GameManager>();
            }
            //없어지지 않도록 해줌

            // 이게 없어야 씬 전환이 원활해짐
            // (이 코드를 활성화 시 전투씬 입장 할 때 초기화가 안되는 오류가 있었음)
            //DontDestroyOnLoad(go);

            //instance 할당
            instance = go.GetComponent<GameManager>();
        }
    }
}

    
