using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class TutorialController : MonoBehaviour
{
    public GameManager gm;
    public Canvas canvas;
    public GameObject tutorialImage;
    public List<Sprite> tutorialList;
    public Button rightButton;
    public Button leftButton;
    public int count;

    void Start()
    {
        gm = GameManager.GetInstance();
        gm.tutorialController = this;
        count = 0;
        for (int i = 0; i < 17; i++)
        {
            tutorialList.Add(Resources.Load<Sprite>($"Sprite/Tutorial/±×¸²{i + 1}"));
        }
        ButtonControll();
        tutorialImage.gameObject.GetComponent<Image>().sprite = tutorialList[count];
    }

    void Update()
    {
        
    }

    public void buttonAnim()
    {
        if (rightButton.gameObject.activeSelf)
        {
            rightButton.image.color = new Color(0, 0, 0, 0);
        }
    }

    public void ButtonControll()
    {
        if(count <= 0)
        {
            leftButton.gameObject.SetActive(false);
        }
        else
        {
            leftButton.gameObject.SetActive(true);
        }

        if(count >= 16)
        {
            rightButton.gameObject.SetActive(false);
        }
        else
        {
            rightButton.gameObject.SetActive(true);
        }

    }

    public void onClickRightButton()
    {
        if (rightButton == null)
        {
            return;
        }
        if (count >= 16)
        {
            return;
        }
        count++;
        tutorialImage.gameObject.GetComponent<Image>().sprite = tutorialList[count];

        ButtonControll();
    }

    public void onClickLeftButton()
    {
        if(leftButton == null)
        {
            return;
        }
        if(count <= 0)
        {
            return;
        }
        count--;
        tutorialImage.gameObject.GetComponent<Image>().sprite = tutorialList[count];

        ButtonControll();
    }
}
