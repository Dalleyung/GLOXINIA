using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextSpawn : MonoBehaviour
{
    //플레이어 데미지 텍스트
    public TextMeshProUGUI text;
    //몬스터 데미지 텍스트
    public TextMeshProUGUI text2;
    // 데미지 텍스트 생성할 위치 값
    public Vector2 playerDmgTextPos;
    public Vector2 monsterDmgTextPos;

    public void MakePlayerDmgText()
    {
        TextMeshProUGUI cpyText = Instantiate(text);
        // 위치 설정
        cpyText.transform.parent = GameObject.Find("GUI").transform;
        cpyText.transform.localPosition = playerDmgTextPos;
        cpyText.transform.localScale = Vector3.one;
        cpyText.gameObject.GetComponent<DamageText>().typeValue = 1;
        cpyText.gameObject.SetActive(true);
    }

    public void MakeMonsterDmgText()
    {
        TextMeshProUGUI cpyText = Instantiate(text2);
        // 위치 설정
        cpyText.transform.parent = GameObject.Find("GUI").transform;
        cpyText.transform.localPosition = monsterDmgTextPos;
        cpyText.transform.localScale = Vector3.one;
        cpyText.gameObject.GetComponent<DamageText>().typeValue = 2;
        cpyText.gameObject.SetActive(true);
    }
}
