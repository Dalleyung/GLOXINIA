using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<GameObject> item;
    private bool open = false;

    public void ShowBag()
    {
        open = !open;

        if (open)
        {
            for(int i = 0 ; i < item.Count; i++)
            {
                item[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < item.Count; i++)
            {
                item[i].SetActive(false);
            }
        }
    }
}
