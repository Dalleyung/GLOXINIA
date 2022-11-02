using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GaugeTest : MonoBehaviour
{
    private float currentTime = 0;
    public float delay = 0.3f;
    public float mobValue = 0.05f;
    public float playerValue = 0.1f;
    private float randomValue;
    public float minVal;
    public float maxVal;
    public bool isGaugeFull = false;

    void Start()
    {
        gameObject.GetComponent<Scrollbar>().size = 0.5f;
    }

    void Update()
    {
        MonsterTouch();
        PlayerTouch();
    }

    void MonsterTouch()
    {
        currentTime += Time.deltaTime;
        mobValue = 0.02f * (gameObject.GetComponent<Scrollbar>().size / 1);
        if(mobValue <= 0.01f)
        {
            mobValue = 0.01f;
        }
        if (currentTime > delay)
        {
            gameObject.GetComponent<Scrollbar>().size -= mobValue;
            currentTime = 0;
        }
    }

    void PlayerTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                randomValue = Random.Range(minVal, maxVal);

                gameObject.GetComponent<Scrollbar>().size += playerValue + randomValue;

                if(gameObject.GetComponent<Scrollbar>().size >= 1)
                {
                    isGaugeFull = true;
                }
            }
        }
    }
}
