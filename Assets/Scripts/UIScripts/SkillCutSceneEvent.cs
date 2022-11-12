using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillCutSceneEvent : MonoBehaviour
{
    GameManager gm;
    public Vector3 originPos;
    public Vector3 originPos2;
    public Vector3 originTextPos;
    public Vector3 originTextPos2;
    public GameObject gameChar;
    public GameObject gameText;

    // 사라지게 만드는 값
    public float alpha;

    public float currentTime = 0;

    private void Awake()
    {
        originPos = gameChar.transform.position;
        originPos2 = new Vector3(0, originPos.y, 0);
        originTextPos = gameText.transform.position;
    }

    private void Start()
    {
        gm = GameManager.GetInstance();
    }

    public void ActiveEvent(int p_num)
    {
        switch (p_num)
        {
            case 0:
                gm.soundManager.PlayEffectSound(gm.soundManager.feverStart);
                break;
            case 1:
                gm.timer.limitTime = gm.timer.maxTime;
                gameObject.SetActive(false);
                gm.timer.gameObject.SetActive(true);
                gm.battleController.TileHandler();
                gm.player_move.freeze = false;
                gm.skill.fe1 = Instantiate(gm.skill.skillEffect);
                gm.skill.fe1.transform.position = new Vector3(0, 21, -5);
                gm.skill.fe2 = Instantiate(gm.skill.skillEffect);
                gm.skill.fe2.transform.position = new Vector3(0, -21.5f, -5);
                break;
            case 2:
                StartCoroutine(CoroutineCutScene());
                break;
        }
    }

    public void MoveCutScene()
    {
        gameChar.transform.position = Vector3.Lerp(originPos, new Vector3(0, originPos.y, 0), easeInOutCirc(0));
        gameText.transform.position = Vector3.Lerp(originTextPos, new Vector3(1, 10, 0), easeInOutCirc(0));
    }

    public void MoveCutSceneEnd()
    {

        if (easeInOutCirc(1) >= 0)
        {
            gameChar.transform.position = Vector3.Lerp(new Vector3(0, originPos.y, 0), new Vector3(900, originPos.y, 0), easeInOutCirc(1));
            gameText.transform.position = Vector3.Lerp(new Vector3(1, 10, 0), new Vector3(-200, 45, 0), easeInOutCirc(1));
        }
    }

    public IEnumerator CoroutineCutScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            currentTime += Time.deltaTime * (1 - currentTime);
            MoveCutScene();
            if(currentTime >= 0.9f)
            {
                currentTime = 0;
                //originPos2 = new Vector3(0, originPos.y, 0);
                StartCoroutine(CoroutineCutSceneEnd());
                yield break;
            }
        }
    }

    public IEnumerator CoroutineCutSceneEnd()
    {
        //yield return new WaitForSeconds(1.0f);
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            currentTime += Time.deltaTime * 3;
            MoveCutSceneEnd();
            if (currentTime >= 1.0f)
            {
                currentTime = 0;
                gameChar.gameObject.transform.position = originPos;
                gameText.gameObject.transform.position = originTextPos;
                yield break;
            }
        }
    }

    float easeInOutCirc(int p_num)
    {
        switch(p_num)
        {
            case 0:
                return Mathf.Sqrt(1 - Mathf.Pow(currentTime - 1, 2));
            case 1:
                return 1 - Mathf.Sqrt(1 - Mathf.Pow(currentTime, 2));
        }
        return -1;
    }
}

