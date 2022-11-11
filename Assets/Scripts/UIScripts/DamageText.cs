using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    GameManager gm;

    public float power;
    // 사라지게 만드는 값
    public float alpha;

    public float currentTime = 0;
    public int typeValue = 0;

    public float RandDMG;
    int TotalDMG;

    // 화산효과 변수
    //// 위로 향하는 힘 값
    //public float textPower;
    //// 중력값
    //public float textGravity;
    //// 랜덤 좌우 최대값
    //public float randMax;


    private void Start()
    {
        gm = GameManager.GetInstance();
        DmgTextAnim();
    }

    public void DmgTextAnim()
    {
        RandDMG = Random.Range(1.00f, 1.07f);
        // 천천히 사라지기
        StartCoroutine(AlphaDown(gameObject.transform.GetComponent<TextMeshProUGUI>()));

        // 데미지 값 설정
        switch(typeValue)
        {
            case 1:
                //폰트 사이즈 증가
                gameObject.transform.GetComponent<TextMeshProUGUI>().fontSize = 300;


                TotalDMG = ((int)(9500 * ((float)gm.player_move.m_MoveStackbuff / 15) * RandDMG));


                if (TotalDMG >= 9999)
                    gameObject.transform.GetComponent<TextMeshProUGUI>().text = (9999).ToString();
                else
                    gameObject.transform.GetComponent<TextMeshProUGUI>().text = TotalDMG.ToString();
                break;
            case 2:
                //분노상태일 때 추가 데미지도 계산하여 넣어줘야 함(현재 변수 없음)

                //폰트 사이즈 증가
                gameObject.transform.GetComponent<TextMeshProUGUI>().fontSize = 300;

                gameObject.transform.GetComponent<TextMeshProUGUI>().text =
            (gm.monster.ATK).ToString();
                break;
        }
        

        // 발사
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.up * power, ForceMode2D.Impulse);

        rigid.gravityScale = 0;

        // 화산효과...RIP
        //TextMeshProUGUI cpyText = Instantiate(damageText);

        //// 위치 설정
        //cpyText.transform.position = pos.position;
        //cpyText.transform.parent = GameObject.Find("GUI").transform;
        //cpyText.transform.localScale = Vector3.one;
        //cpyText.gameObject.SetActive(true);

        //// 데미지 값 설정
        //cpyText.text = (gm.player.ATK + gm.player_move.moveStack).ToString();

        //// 사라지기
        //StartCoroutine(AlphaDown(cpyText));

        //// 발사
        //Rigidbody2D rigid = cpyText.GetComponent<Rigidbody2D>();
        //float rand = Random.Range(-randMax, randMax);
        //rigid.AddForce(cpyText.transform.up * textPower + new Vector3(rand, 0, 0), ForceMode2D.Impulse);

        //rigid.gravityScale = textGravity;
    }

    IEnumerator AlphaDown(TextMeshProUGUI cpyText)
    {
        while (true)
        {

            cpyText.color -= new Color(0, 0, 0, easeInExpo());

            yield return new WaitForSeconds(0.3f);
            if (cpyText.color.a <= 0)
            {
                currentTime = 0;
                Destroy(cpyText.gameObject);
                break;
            }
        }
    }


    private void Update()
    {
        if (GetComponent<TextMeshProUGUI>().fontSize >= 100)
            GetComponent<TextMeshProUGUI>().fontSize -= 10;
    }

    float easeInExpo()
    {
        currentTime += Time.deltaTime * alpha;
        return Mathf.Pow(2, 10 * currentTime - 10);
    }
}
