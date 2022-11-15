using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    GameManager gm;
    public float speed = 0;
    public GameObject obj;
    public float power;
    public float accelerator;
    void Start()
    {
        gm = GameManager.GetInstance();
        CreateEffect();
    }

    void Update()
    {

    }

    public void CreateEffect()
    {
        GameObject cpyEffect2 = Instantiate(Resources.Load("Prefabs/" + "Normal_Attack_VFX") as GameObject);
        cpyEffect2.transform.position = transform.GetChild(0).position;
        Destroy(cpyEffect2, 1f);
    }

    public IEnumerator BezierSword(int p_num)
    {
        while (true)
        {
            yield return null;
            speed += accelerator * Time.deltaTime;
            switch (p_num)
            {
                case 0:
                    {
                        transform.position = gm.bezierCurve.Bezier(gm.bezierCurve.P1_1, gm.bezierCurve.P1_2, gm.bezierCurve.P1_3, speed);
                        Vector3 dir = gm.bezierCurve.Bezier(gm.bezierCurve.P1_1, gm.bezierCurve.P1_2, gm.bezierCurve.P1_3, speed * 1.01f) - transform.position;
                        //거리를 normalized하면 방향이 됨
                        if (speed < 0.9f)
                        {
                            transform.up = -dir.normalized;
                        }
                        break;
                    }
                case 1:
                    {
                        transform.position = gm.bezierCurve.Bezier(gm.bezierCurve.P2_1, gm.bezierCurve.P2_2, gm.bezierCurve.P2_3, speed);
                        Vector3 dir = gm.bezierCurve.Bezier(gm.bezierCurve.P2_1, gm.bezierCurve.P2_2, gm.bezierCurve.P2_3, speed * 1.01f) - transform.position;
                        //거리를 normalized하면 방향이 됨
                        if (speed < 0.9f)
                        {
                            transform.up = -dir.normalized;
                        }
                        break;
                    }
                case 2:
                    {
                        transform.position = gm.bezierCurve.Bezier(gm.bezierCurve.P3_1, gm.bezierCurve.P3_2, gm.bezierCurve.P3_3, speed);
                        Vector3 dir = gm.bezierCurve.Bezier(gm.bezierCurve.P3_1, gm.bezierCurve.P3_2, gm.bezierCurve.P3_3, speed * 1.01f) - transform.position;
                        //거리를 normalized하면 방향이 됨
                        if (speed < 0.9f)
                        {
                            transform.up = -dir.normalized;
                        }
                        break;
                    }
                case 3:
                    {
                        transform.position = gm.bezierCurve.Bezier(gm.bezierCurve.P4_1, gm.bezierCurve.P4_2, gm.bezierCurve.P4_3, speed);
                        Vector3 dir = gm.bezierCurve.Bezier(gm.bezierCurve.P4_1, gm.bezierCurve.P4_2, gm.bezierCurve.P4_3, speed * 1.01f) - transform.position;
                        //거리를 normalized하면 방향이 됨
                        if (speed < 0.9f)
                        {
                            transform.up = -dir.normalized;
                        }
                        break;
                    }
                case 4:
                    {
                        transform.position = gm.bezierCurve.Bezier(gm.bezierCurve.P5_1, gm.bezierCurve.P5_2, gm.bezierCurve.P5_3, speed);
                        Vector3 dir = gm.bezierCurve.Bezier(gm.bezierCurve.P5_1, gm.bezierCurve.P5_2, gm.bezierCurve.P5_3, speed * 1.01f) - transform.position;
                        //거리를 normalized하면 방향이 됨
                        if (speed < 0.9f)
                        {
                            transform.up = -dir.normalized;
                        }
                        break;
                    }
            }
            // 잔상 이펙트
            GameObject cpyEffect = Instantiate(Resources.Load("Prefabs/" + "Boss_Rage_Sword_VFX") as GameObject);
            cpyEffect.transform.position = transform.position;
            Destroy(cpyEffect, 0.5f);

            if (speed >= 1)
            {
                Destroy(gameObject, 1.0f);
                gm.soundManager.PlayEffectSound(gm.soundManager.stuck);
                // 진동
                Handheld.Vibrate();
                // 사운드
                GameObject cpyEffect2 = Instantiate(Resources.Load("Prefabs/" + "Rage_Small_VFX") as GameObject);
                cpyEffect2.transform.position = transform.position;
                Destroy(cpyEffect2, 1f);

                yield break;
            }
        }
    }

    private void OnDestroy()
    {
        //GameObject copyObj = Instantiate(obj);
        //copyObj.transform.position = transform.position + Vector3.up * 0.1f;
        //copyObj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * power, ForceMode2D.Impulse);
        //Destroy(copyObj, 0.2f);
        //shieldOn = false 일때 타일 폭팔 애니매이션
        if (!gm.battleController.shieldOn)
        {
            GameObject copyObj2 = Instantiate(obj);
            copyObj2.transform.position = transform.position + Vector3.right * 0.1f;
            copyObj2.GetComponent<Rigidbody2D>().AddForce(Vector2.right * power, ForceMode2D.Impulse);
            Destroy(copyObj2, 0.2f);
            GameObject copyObj3 = Instantiate(obj);
            copyObj3.transform.position = transform.position + Vector3.left * 0.1f;
            copyObj3.GetComponent<Rigidbody2D>().AddForce(Vector2.left * power, ForceMode2D.Impulse);
            Destroy(copyObj3, 0.2f);
            GameObject copyObj4 = Instantiate(obj);
            copyObj4.transform.position = transform.position + Vector3.down * 0.1f;
            copyObj4.GetComponent<Rigidbody2D>().AddForce(Vector2.down * power, ForceMode2D.Impulse);
            Destroy(copyObj4, 0.2f);
        }
        gm.monster.BossRageAttack();
        GameObject cpyEffect = Instantiate(Resources.Load("Prefabs/" + "Rage_Attack_VFX") as GameObject);
        cpyEffect.transform.position = transform.position;
        Destroy(cpyEffect, 1f);

        if (gm.battleController.shieldOn)
        {
            gm.soundManager.PlayEffectSound(gm.soundManager.parrying);
        }
        else
        {
            gm.soundManager.PlayEffectSound(gm.soundManager.demonAttack);
            if (gm.monster.swordList[0].gameObject == gameObject)
            {
                gm.player_move.FallingTileAnim();
            }
            if (gm.monster.swordList[4].gameObject == gameObject)
            {
                gm.monster.swordList.Clear();
            }
        }
    }
}
