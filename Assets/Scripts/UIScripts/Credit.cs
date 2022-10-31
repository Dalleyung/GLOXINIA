using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credit : MonoBehaviour
{
    GameManager gm;
    public List<string> nameList;
    public string lastMsg;
    public int count = 0;
    public int scrollSpeed = 1;
    bool isend;
    private void Start()
    {
        gm = GameManager.GetInstance();
        lastMsg = "Thanks For Playing";
        isend = false;
    }

    public void NameTextAnim(int p_count)
    {
        if (p_count < nameList.Count)
        {
            Debug.Log(p_count);
            gameObject.transform.GetComponent<TextMeshProUGUI>().text = nameList[p_count];
            count = p_count;
        }
        else if(p_count == nameList.Count)
        {
            Debug.Log(p_count);
            gameObject.transform.GetComponent<TextMeshProUGUI>().text = lastMsg;
            count = p_count;
        }
    }
    private void OnDestroy()
    {
        if (count >= nameList.Count)
        {
            gm.nameSpawn.flag = true;
        }
    }

    public float currentTime = 0;
    public IEnumerator TextFadeOut()
    {
        if (count == nameList.Count && !isend)
        {
            yield return new WaitForSeconds(2f);
            isend = true;
        }
        
        yield return new WaitForSeconds(0.05f);
        if (count == nameList.Count)
        {
            currentTime += Time.deltaTime * 2;
        }
        else
        {
            currentTime += Time.deltaTime * 4;
        }
        gameObject.GetComponent<TextMeshProUGUI>().color = 
            Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 0), currentTime);
        if (currentTime >= 1)
        {
            //gameObject.SetActive(false);
            if (count == nameList.Count)
            {
                Destroy(gameObject,1f);
            }
            else 
            {
                Destroy(gameObject);
            }
            yield break;
        }
    }
    
    void ScrollUp()
    {
        if(count == nameList.Count)
        {
            if(gameObject.transform.localPosition.y <= 50)
            {
                gameObject.transform.position += Vector3.up * Time.deltaTime;
            }
        }
        else
        {
            gameObject.transform.position += Vector3.up * Time.deltaTime;
        }

        if (count == nameList.Count)
        {
            if (gameObject.transform.localPosition.y >= 50)
            {
                StartCoroutine(TextFadeOut());
            }
        }
        else
        {
            if (gameObject.transform.localPosition.y >= 240)
            {
                StartCoroutine(TextFadeOut());
            }
        }

        if (gameObject.transform.localPosition.y >= 280)
        {
            //Destroy(gameObject);
        }
    }

    private void Update()
    {
        ScrollUp();
    }
}
