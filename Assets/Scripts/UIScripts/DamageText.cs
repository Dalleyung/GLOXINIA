using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    GameManager gm;

    public float power;
    // ������� ����� ��
    public float alpha;

    public float currentTime = 0;
    public int typeValue = 0;

    

    // ȭ��ȿ�� ����
    //// ���� ���ϴ� �� ��
    //public float textPower;
    //// �߷°�
    //public float textGravity;
    //// ���� �¿� �ִ밪
    //public float randMax;


    private void Start()
    {
        gm = GameManager.GetInstance();
        DmgTextAnim();
    }

    public void DmgTextAnim()
    {
        // õõ�� �������
        StartCoroutine(AlphaDown(gameObject.transform.GetComponent<TextMeshProUGUI>()));

        // ������ �� ����
        switch(typeValue)
        {
            case 1:
                //��Ʈ ������ ����
                gameObject.transform.GetComponent<TextMeshProUGUI>().fontSize = 300;


                if (gm.player.TotalDMG >= 99999)
                    gameObject.transform.GetComponent<TextMeshProUGUI>().text = (99999).ToString();
                else
                    gameObject.transform.GetComponent<TextMeshProUGUI>().text = ((int)gm.player.TotalDMG).ToString();
                break;
            case 2:
                //�г������ �� �߰� �������� ����Ͽ� �־���� ��(���� ���� ����)

                //��Ʈ ������ ����
                gameObject.transform.GetComponent<TextMeshProUGUI>().fontSize = 300;

                gameObject.transform.GetComponent<TextMeshProUGUI>().text =
            ((int)gm.monster.TotalDMG).ToString();
                break;
        }
        

        // �߻�
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.up * power, ForceMode2D.Impulse);

        rigid.gravityScale = 0;

        // ȭ��ȿ��...RIP
        //TextMeshProUGUI cpyText = Instantiate(damageText);

        //// ��ġ ����
        //cpyText.transform.position = pos.position;
        //cpyText.transform.parent = GameObject.Find("GUI").transform;
        //cpyText.transform.localScale = Vector3.one;
        //cpyText.gameObject.SetActive(true);

        //// ������ �� ����
        //cpyText.text = (gm.player.ATK + gm.player_move.moveStack).ToString();

        //// �������
        //StartCoroutine(AlphaDown(cpyText));

        //// �߻�
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
