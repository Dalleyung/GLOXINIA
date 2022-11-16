using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class Monster : MonoBehaviour
{
    public GameManager gm;
    public Player player;
    public float ATK;
    public float HP;
    public float MaxHP;
    public bool isDie;  // 몬스터 Die 추가 (종훈)
    public Scrollbar monsterHPBar;
    public Scrollbar monsterHPBar2;

    /// <summary>
    /// 분노스택 (유제상)
    /// </summary>
    public int rage;

    public const int MAX_RAGE = 3;
    public const int MIN_RAGE = 0;
    /// <summary>
    /// 분노상태 확인용 변수 (유제상)
    /// </summary>
    public bool israge;

    public Animator anim;

    public List<GameObject> swordList;
    public int maxSwordCnt = 5;
    public float spawnDelay;
    public float shootDelay;

    public GameObject attackEffect;

    public GameObject bossCoreEffect;
    public GameObject bossDieEffect_1;
    public GameObject bossDieEffect_2;

    public GameObject bigSword;

    public GameObject obj;

    public GameObject rageCutScene;

    public int spawnSwordCnt = 0;


    public float RandDMG;
    public float TotalDMG;
    public int RefFig = 15000;

    //HPbar 변수
    public float time;
    float beforesize;

    enum MonsterType
    {
        Boss = 0,
        MonsterA,
        MonsterB
    };
    public int type;
    void Start()
    {
        gm = GameManager.GetInstance();
        player = GameManager.GetInstance().player;
        if (monsterHPBar != null)
        {
            monsterHPBar.size = HP / MaxHP;
        }
        if (monsterHPBar2 != null)
            monsterHPBar2.size = 1;
        ATK = 10;
        rage = MIN_RAGE;
        israge = false;

    }

    public void UpdateHPBar()
    {
        if (monsterHPBar != null && HP >= -1)
        {
            monsterHPBar.size = HP / MaxHP;
            monsterHPBar.transform.GetChild(4).GetComponent<Image>().color
                = Color.Lerp(Color.white, new Color32(255, 64, 64, 255), monsterHPBar.size);
        }
        if (monsterHPBar2 != null && HP >= -1)
        {
            if (monsterHPBar2.size > monsterHPBar.size)
                time += Time.deltaTime;
            else
                beforesize = monsterHPBar2.size;

            monsterHPBar2.size = Mathf.Lerp(beforesize, monsterHPBar.size, time);

            if (time >= 1)
                time = 0;
        }
    }


    public void ChangeImage()
    {
        if (player == null)
        {
            return;
        }
        if (player.P_Fail == true)
        {
            //플레이어 피격모션 애니메이션 추가후 사용
        }
    }

    //플레이어 공격할때 동시처리 함수
    public int AttackPlayer()
    {
        if(gm.monster.israge)
        {
            RandDMG = Random.Range(1.3f, 1.5f);
            if (LoadingSceneManager.currentStage == 0)//현재 씬이 소일때
            {
                //몬스터 분노 퍼즐 실패 데미지 공식
                TotalDMG = 9567 * (4 - gm.Rage.rageclear) * RandDMG;
            }
            else
            {
                //보스일때 5번 공격하게됨으로 .2를 곱해줌
                TotalDMG = (9567 * (4 - gm.Rage.rageclear) * RandDMG) * 0.2f;
            }
        }
        else
        {
            RandDMG = Random.Range(0.96f, 1.04f);
            //몬스터 공격 공식
            TotalDMG = (RefFig * RandDMG) - gm.player.TotalDMG;
        }

        //음수값이 들어가면 dmg 0으로 처리
        if (TotalDMG < 0)
            TotalDMG = 0;

        return (int)TotalDMG;
    }

    public float power;
    public void BossNormalAttackEvent(int p_num)
    {
        switch (p_num)
        {
            case 0:
                Debug.Log("1");
                bigSword = Instantiate(Resources.Load("Prefabs/" + "BigSword") as GameObject);
                bigSword.transform.position = new Vector3(0, 13, 0);
                bigSword.SetActive(true);
                break;
            case 1:
                Debug.Log("2");

                gm.soundManager.PlayEffectSound(gm.soundManager.demonAttack);

                bigSword.GetComponent<Rigidbody2D>().AddForce(Vector3.down * power, ForceMode2D.Impulse);
                for (int i = 0; i < gm.setTile.TileList.Count; i++)
                {
                    gm.setTile.TileList[i].GetComponent<BoxCollider2D>().isTrigger = false;
                }
                GameObject copyObj4 = Instantiate(obj);
                copyObj4.transform.position = transform.position + Vector3.down * 0.1f;
                copyObj4.GetComponent<Rigidbody2D>().AddForce(Vector2.down * power, ForceMode2D.Impulse);
                Destroy(copyObj4, 0.2f);

                gm.player.HP -= AttackPlayer();
                gm.cameraShake.shakePower = 1;
                gm.cameraShake.Shake(true);
                gm.damageTextSpawn.MakeMonsterDmgText();

                gm.player_move.FallingTileAnim();

                // 타일 깨짐화
                for (int i = 0; i < gm.setTile.TileList.Count; i++)
                {
                    gm.setTile.TileList[i].tileValue = 6;
                }

                // 체력 검사
                HPCheck();

                if (gm.player.HP <= 0)
                {
                    gm.player.HP = 0;
                    gm.player.isDie = true;
                    gm.gameOver.StartGameOverAnim();
                }
                break;
            case 2:
                HandleRageStack();
                Debug.Log("3");
                if (!gm.monster.israge && gm.skill.isSkillGaugeFull)
                {
                    gm.soundManager.PlayBGMSound(gm.soundManager.feverBGM);
                    gm.skill.cutScene.gameObject.SetActive(true);
                    gm.battleController.feverOn = true;
                }
                StartCoroutine(FallingAnimDelay());
                Destroy(bigSword, 1f);
                break;
            case 3:
                GameObject cpyEffect = Instantiate(Resources.Load("Prefabs/" + "Normal_Attack_VFX") as GameObject);
                cpyEffect.transform.position = bigSword.transform.position;
                Destroy(cpyEffect, 1f);
                break;
        }
    }

    public void MonsterAttackEvent()
    {
        if (gm.battleController.shieldOn)
        {
            for (int i = 0; i < gm.setTile.TileList.Count; i++)
            {
                gm.setTile.TileList[i].TileShield();
            }
            HandleRageStack();
            gm.cameraShake.shakePower = 0.5f;
            gm.cameraShake.Shake(true);
            gm.battleController.shieldOn = false;
            gm.soundManager.PlayEffectSound(gm.soundManager.parrying);

            // 예외 처리였던 것..
            if (gm.skill.isSkillGaugeFull)
            {
                gm.soundManager.PlayBGMSound(gm.soundManager.feverBGM);
                gm.skill.cutScene.gameObject.SetActive(true);
                gm.battleController.feverOn = true;
            }
        }
        else
        {
            AllTileFallingAnim();
            Debug.Log("공격 맞음");
            
            if (rage < MAX_RAGE && gm.timer.timeover == true)
            {
                gm.player.HP -= AttackPlayer();
                if (gm.player.HP <= 0)
                {
                    gm.player.HP = 0;
                    gm.player.isDie = true;
                    gm.gameOver.StartGameOverAnim();
                }
                gm.cameraShake.shakePower = 1;
                gm.cameraShake.Shake(true);
                gm.damageTextSpawn.MakeMonsterDmgText();
                HandleRageStack();
                CowAttackEffectOn();
            }
            else if (rage >= MAX_RAGE && gm.timer.timeover == true)
            {
                if (gm.Rage.ragecount >= 3)
                {
                    gm.player.HP -= AttackPlayer();
                    if (gm.player.HP <= 0)
                    {
                        gm.player.HP = 0;
                        gm.player.isDie = true;
                        gm.gameOver.StartGameOverAnim();
                    }
                    gm.cameraShake.shakePower = 2;
                    gm.cameraShake.Shake(true);
                    gm.damageTextSpawn.MakeMonsterDmgText();
                    HandleRageStack();
                    CowAttackEffectOn();
                }
            }
            else if (rage >= MAX_RAGE && gm.timer.timeover == false)
            {
                if (gm.Rage.ragecount >= 3)
                {
                    gm.player.HP -= AttackPlayer();
                    if (gm.player.HP <= 0)
                    {
                        gm.player.HP = 0;
                        gm.player.isDie = true;
                        gm.gameOver.StartGameOverAnim();
                    }
                    gm.cameraShake.shakePower = 2;
                    gm.cameraShake.Shake(true);
                    gm.damageTextSpawn.MakeMonsterDmgText();
                    HandleRageStack();
                    CowAttackEffectOn();
                }
            }
            else if (rage < MAX_RAGE || gm.timer.timeover == true)
            {
                gm.player.HP -= AttackPlayer();
                if (gm.player.HP <= 0)
                {
                    gm.player.HP = 0;
                    gm.player.isDie = true;
                    gm.gameOver.StartGameOverAnim();
                }
                gm.cameraShake.shakePower = 1;
                gm.cameraShake.Shake(true);
                gm.damageTextSpawn.MakeMonsterDmgText();
                HandleRageStack();
                CowAttackEffectOn();
            }

            switch (LoadingSceneManager.currentStage)
            {
                case (int)LoadingSceneManager.STAGE.COW:
                    gm.soundManager.PlayEffectSound(gm.soundManager.cowAttack);
                    break;
                case (int)LoadingSceneManager.STAGE.DEMON:
                    gm.soundManager.PlayEffectSound(gm.soundManager.demonAttack);
                    break;
            }
        }

        gm.monster.swordList.Clear();
        //플레이어 피격 애니메이션이벤트 생기면 수정해야할 코드
        StartCoroutine(FallingAnimDelay());

        // 체력 검사
        HPCheck();
    }

    public void BossRageAttack()
    {
        if (gm.battleController.shieldOn)
        {
            for (int i = 0; i < gm.setTile.TileList.Count; i++)
            {
                gm.setTile.TileList[i].TileShield();
            }
            gm.cameraShake.shakePower = 0.2f;
            gm.cameraShake.Shake(true);
        }
        else
        {
            if (gm.Rage.ragecount >= 3)
            {
                //몬스터 분노 퍼즐 실패 데미지
                gm.player.HP -= AttackPlayer();
                gm.cameraShake.shakePower = 2;
                gm.cameraShake.Shake(true);
                gm.damageTextSpawn.MakeMonsterDmgText();
                gm.soundManager.PlayEffectSound(gm.soundManager.demonAttack);
                
            }
        }

        // 체력 검사
        HPCheck();
    }

    public void HPCheck()
    {
        if (gm.player.HP / gm.player.MaxHP > 0.3f)
        {
            gm.player.HPDanger.SetActive(false);
            Debug.Log("경고 미표시");
        }
        else
        {
            gm.player.HPDanger.SetActive(true);
            Debug.Log("경고 표시");
        }
    }

    public void BossRageAttackEvent(int p_num)
    {
        
        switch (p_num)
        {
            case 0:
                swordList[spawnSwordCnt++].gameObject.SetActive(true);
                gm.soundManager.PlayEffectSound(gm.soundManager.createSword);
                break;
            case 1:
                for (int i = 0; i < gm.setTile.TileList.Count; i++)
                {
                    gm.setTile.TileList[i].GetComponent<BoxCollider2D>().isTrigger = false;
                }
                StartCoroutine(ShootingSwordAnim());
                break;
            case 2:
                if (israge)
                {
                    HandleRageStack();
                }
                
                gm.battleController.shieldOn = false;
                if (Skill.skillGauge != 0)
                {
                    rage = MIN_RAGE;
                    israge = false;
                }
                if (gm.player.HP <= 0)
                {
                    gm.player.HP = 0;
                    gm.player.isDie = true;
                    gm.gameOver.StartGameOverAnim();
                }

                ////플레이어 피격 애니메이션이벤트 생기면 수정해야할 코드
                ///
                if (gm.skill.isSkillGaugeFull)
                {
                    gm.soundManager.PlayBGMSound(gm.soundManager.feverBGM);
                    gm.skill.cutScene.gameObject.SetActive(true);
                    gm.battleController.feverOn = true;
                }

                /*if (!gm.battleController.feverOn)
                {
                    StartCoroutine(FallingAnimDelay());
                }*/
                StartCoroutine(FallingAnimDelay());
                break;
        }

    }

    IEnumerator ShootingSwordAnim()
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(shootDelay);
            StartCoroutine(swordList[count].GetComponent<Sword>().BezierSword(count++));
            gm.soundManager.PlayEffectSound(gm.soundManager.shotSword);
            if (count >= maxSwordCnt)
            {
                yield break;
            }
        }
    }



    public IEnumerator FallingAnimDelay()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < gm.setTile.TileList.Count; i++)
        {
            Destroy(gm.setTile.TileList[i].gameObject);
        }
        gm.setTile.TileList.Clear();
        gm.setTile.InstantiateTile(Resources.Load<GameObject>("Prefabs/" + "Tile"));
        for (int i = 0; i < gm.setTile.TileList.Count; i++)
        {
            gm.setTile.TileList[i].gameObject.SetActive(true);
        }
        gm.player_move.GetComponent<Rigidbody2D>().gravityScale = 0;
        gm.player_move.gameObject.SetActive(false);
        if (!gm.skill.isSkillGaugeFull)
        {
            gm.battleController.TimeOverHandler();
        }
        gm.player_move.gameObject.SetActive(true);

        //무지성으로 건듦
        gm.setTile.Init();

        //분노 상태 돌입 시 컷신 연출동안 플레이어 freeze에 타일을 빈타일로 만들기 위함
        // 분노 후 피버로 들어갈 시에 위와 같은 이유로 피버도 추가
        if(israge || gm.battleController.feverOn)
        {
            //컷씬 연출이 끝나고 freeze 해제 및 TileHandler호출로 타일 재배치
            AttackDelay();
        }
        CowAttackEffectOff();
    }

    /// <summary>
    /// 분노스택 줄이는함수 (유제상)
    /// </summary>
    public void HandleRageStack()
    {
        if (israge == true && gm.Rage.ragecount >= 3)
        {
            //israge가 true일 때는 false가 되는 동시에 rage가 3이 되게 함
            israge = false;
            rage = MIN_RAGE;
            gm.Rage.ragecount = 0;
            gm.Rage.rageclear = 0;
            gm.soundManager.PlayBGMSound(gm.soundManager.battleBGM);
            gm.skill.RaiseSkillGauge(0);
        }
        else if (israge == true && gm.Rage.ragecount < 3)
        {
            gm.Rage.ragecount++;
        }
        else if (israge == false)
        {
            //israge가 false일 때는 rage를 감소시키고 0이 되는 동시에 israge가 true가 되게 함
            rage++;
            if (rage >= MAX_RAGE && !gm.player.isDie)
            {
                israge = true;
                gm.soundManager.PlayBGMSound(gm.soundManager.rageBGM);
                rageCutScene.gameObject.SetActive(true);
                gm.timer.gameObject.SetActive(false);
            }
        }
    }

    void AllTileFallingAnim()
    {
        for (int i = 0; i < gm.setTile.TileList.Count; i++)
        {
            gm.setTile.TileList[i].FallingTileAnim();
        }
        gm.player_move.FallingTileAnim();
    }

    public void AttackDelay()
    {
        gm.player_move.freeze = true;
        gm.timer.gameObject.SetActive(false);
        for (int i = 0; i < gm.setTile.TileList.Count; i++)
        {
            gm.setTile.TileList[i].tileValue = (int)SetTile.E_TileValue.Fever_Disable_Tile;
        }
    }

    public void AttackAnimation()
    {
        if (gm.Rage.ragecount >= 3 || israge == false)
        {
            if (gm.battleController.feverOn)
            {
                return;
            }
            if (!gm.monster.isDie && !gm.battleController.feverOn)
            {
                AttackDelay();
                if (israge && LoadingSceneManager.currentStage == (int)LoadingSceneManager.STAGE.DEMON)
                {
                    gm.monster.anim.SetTrigger("IsRageAttack");
                }
                else
                {
                    gm.monster.anim.SetTrigger("IsAttack");
                }
            }
        }
        else
        {
            HandleRageStack();
            gm.battleController.TileHandler();
        }
    }

    public void HitAnimation()
    {
        if (!gm.skill.isSkillGaugeFull)
            AttackDelay();
        gm.monster.anim.SetTrigger("IsHit");

    }

    public void DieAnimationEvent()
    {
        if (gm.monster.isDie)
        {
            AttackDelay();
            gm.monster.anim.SetTrigger("IsDie");
            // 타일 깨짐화
            for (int i = 0; i < gm.setTile.TileList.Count; i++)
            {
                gm.setTile.TileList[i].tileValue = 6;
            }
            // 죽었을 때만 피버 사라지게 하기
            if (gm.skill.fe1 != null)
            {
                Destroy(gm.skill.fe1);
                Destroy(gm.skill.fe2);
            }
        }
    }

    public void Victory()
    {
        if (LoadingSceneManager.currentStage == (int)LoadingSceneManager.STAGE.DEMON)
        {
            gm.battleController.backBtn.gameObject.SetActive(false);
            gm.resultBtn.gameObject.SetActive(true);
            gm.resultEffect.gameObject.SetActive(true);
            gm.resultBtn.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            gm.resultBtn.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            gm.resultBtn.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
            gm.resultBtn.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            gm.resultBtn.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            gm.battleController.backBtn.gameObject.SetActive(false);
            gm.resultBtn.gameObject.SetActive(true);
            gm.resultEffect.gameObject.SetActive(true);
            gm.resultBtn.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            gm.resultBtn.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            gm.resultBtn.transform.GetChild(3).gameObject.SetActive(false);
            gm.resultBtn.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        }
        gm.soundManager.PlayBGMSound(gm.soundManager.victoryBGM);
        gm.monster.AttackDelay();
        anim.speed = 0;
    }


    public void CowAttackEffectOn()
    {
        if (LoadingSceneManager.currentStage == (int)LoadingSceneManager.STAGE.COW)
        {
            attackEffect.gameObject.SetActive(true);
        }
    }

    public void CowAttackEffectOff()
    {
        if (LoadingSceneManager.currentStage == (int)LoadingSceneManager.STAGE.COW)
        {
            attackEffect.gameObject.SetActive(false);
        }
    }

    [System.Obsolete]
    public void BossDieEffectEvent(int p_num)
    {
        if (gm.skill.fe1 != null)
        {
            Destroy(gm.skill.fe1);
            Destroy(gm.skill.fe2);
        }

        if (LoadingSceneManager.currentStage == (int)LoadingSceneManager.STAGE.DEMON)
        {
            switch (p_num)
            {
                case 0:
                    bossDieEffect_1.gameObject.SetActive(true);
                    break;
                case 1:
                    bossCoreEffect.gameObject.GetComponent<ParticleSystem>().startColor = new Color32(255, 0, 0, 255);
                    break;
                case 2:
                    bossDieEffect_1.gameObject.SetActive(false);
                    bossDieEffect_2.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
