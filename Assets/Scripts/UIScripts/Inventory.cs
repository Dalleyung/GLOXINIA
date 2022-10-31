using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public enum ITEM_CODE
    {
        J_Mew = 0,
        Blood_Red_Wings,
    }

    // �κ� ���� ��ư
    public GameObject weaponBtn, accessoryBtn;
    // �κ��丮
    public GameObject weaponInv, accessoryInv;

    // ���� üũ��
    private bool weaponCheck = false;
    private bool accessoryCheck = false;

    // ������ �ִ� ������
    public List<Item> weaponItemList;
    public List<Item> accessoryItemList;

    public Button slot;

    // ������ �̹���
    // 0 : ���̹� 1: �ͺ�����
    public List<Sprite> itemSprite;

    void Start()
    {
        
    }

    void Update()
    {
        TestInv();
    }

    // �׽�Ʈ�� ������ ����
    private void TestInv()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Button cpyobj;
            cpyobj = Button.Instantiate(slot);
            cpyobj.transform.parent = GameObject.Find("WeaponContent").transform;
            cpyobj.image.sprite = itemSprite[0];
            cpyobj.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            cpyobj.gameObject.transform.localScale = Vector2.one;   // ������ ���� �� ����Ǵ��� �𸣰ھ ������ ��ȯ

            // ������ �ڵ� �ο�
            cpyobj.GetComponent<Item>().code = (int)ITEM_CODE.J_Mew;

            weaponItemList.Add(cpyobj.GetComponent<Item>());
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            Button cpyobj;
            cpyobj = Button.Instantiate(slot);
            cpyobj.transform.parent = GameObject.Find("AccessoryContent").transform;
            cpyobj.gameObject.GetComponent<Button>().image.sprite = itemSprite[1];
            cpyobj.gameObject.transform.localScale = Vector2.one;
            accessoryItemList.Add(cpyobj.GetComponent<Item>());
        }
    }

    public void ShowWeapon()
    {
        weaponCheck = !weaponCheck;
        if(weaponCheck)
        {
            weaponInv.transform.localPosition = Vector2.zero;
            accessoryInv.transform.localPosition = new Vector2(0, 1000);
            accessoryCheck = false;
        }
        else
        {
            weaponInv.transform.localPosition = new Vector2(0, 1000);
        }
    }

    public void ShowAccessory()
    {
        accessoryCheck = !accessoryCheck;
        if (accessoryCheck)
        {
            accessoryInv.transform.localPosition = Vector2.zero;
            weaponInv.transform.localPosition = new Vector2(0, 1000);
            weaponCheck = false;
        }
        else
        {
            accessoryInv.transform.localPosition = new Vector2(0, 1000);
        }
    }
}
