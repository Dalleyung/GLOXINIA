using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject go;
    public float StartTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TestStart", StartTime);
    }

    void TestStart()
    {
        go.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
