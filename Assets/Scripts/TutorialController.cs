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
    public Button MainMenu;
    public int count;

    void Start()
    {
        gm = GameManager.GetInstance();
        gm.tutorialController = this;
        count = 0;
        Vector2 size = canvas.GetComponent<RectTransform>().sizeDelta;
        tutorialImage.GetComponent<RectTransform>().sizeDelta = size;
        for (int i = 0; i < 17; i++)
        {
            tutorialList.Add(Resources.Load<Sprite>($"Sprite/Tutorial/±×¸²{i + 1}"));
        }
        ButtonControll();
        tutorialImage.gameObject.GetComponent<Image>().sprite = tutorialList[count];
    }

    void Update()
    {
        ButtonAnimSync();
    }

    public void ButtonAnimSync()
    {
        if(leftButton.gameObject.activeSelf)
        {
            rightButton.GetComponent<Animator>().enabled = false;
            rightButton.image.color = leftButton.image.color;
        }
        else
        {
            rightButton.GetComponent<Animator>().enabled = true;
            leftButton.image.color = rightButton.image.color;
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
            MainMenu.gameObject.SetActive(true);
        }
        else
        {
            rightButton.gameObject.SetActive(true);
            MainMenu.gameObject.SetActive(false);
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

    public void onClickMeinMenu()
    {
        LoadingSceneManager.currentStage = (int)LoadingSceneManager.STAGE.MAIN;
        LoadingSceneManager.NowStage((int)LoadingSceneManager.STAGE.MAIN);
    }
}
