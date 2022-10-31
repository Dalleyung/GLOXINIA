using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextSpawn : MonoBehaviour
{
    //�÷��̾� ������ �ؽ�Ʈ
    public TextMeshProUGUI text;
    //���� ������ �ؽ�Ʈ
    public TextMeshProUGUI text2;
    // ������ �ؽ�Ʈ ������ ��ġ ��
    public Vector2 playerDmgTextPos;
    public Vector2 monsterDmgTextPos;

    public void MakePlayerDmgText()
    {
        TextMeshProUGUI cpyText = Instantiate(text);
        // ��ġ ����
        cpyText.transform.parent = GameObject.Find("GUI").transform;
        cpyText.transform.localPosition = playerDmgTextPos;
        cpyText.transform.localScale = Vector3.one;
        cpyText.gameObject.GetComponent<DamageText>().typeValue = 1;
        cpyText.gameObject.SetActive(true);
    }

    public void MakeMonsterDmgText()
    {
        TextMeshProUGUI cpyText = Instantiate(text2);
        // ��ġ ����
        cpyText.transform.parent = GameObject.Find("GUI").transform;
        cpyText.transform.localPosition = monsterDmgTextPos;
        cpyText.transform.localScale = Vector3.one;
        cpyText.gameObject.GetComponent<DamageText>().typeValue = 2;
        cpyText.gameObject.SetActive(true);
    }
}
