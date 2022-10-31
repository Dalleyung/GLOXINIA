using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    GameManager gm;

    public const float Min_power = 5;
    public const float Max_power = 15;
    public float shakePower;

    Vector3 originPos;

    private void Awake()
    {
        gm = GameManager.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void Shake(bool isshake)
    {
        Handheld.Vibrate();
        if (isshake)
        {
            StartCoroutine(StartCameramove());
        }
    }
    public float currentTime = 0;
    public IEnumerator StartCameramove()
    {
        while (true)
        {

            //if (currentTime <= 0)
            //{
            //    yrange = //Random.Range(Min_power, Max_power);
            //    zrange = //Random.Range(Min_power, Max_power);
            //}
            if (currentTime % 2 == 0 && currentTime <=10)
            {
                Debug.Log("Starrrr");
                transform.position = Vector3.Lerp(transform.position, new Vector3(shakePower, 0,-10),1);
            }
            else if(currentTime % 2 == 1 && currentTime <= 10)
            {
                StartCoroutine(EndCameramove(shakePower));
            }
            shakePower -= 0.2f;
            if(shakePower <= 0)
            {
                shakePower = 0;
            }
            currentTime += 1;

            if (currentTime >= 10)
            {
                transform.position = originPos;
                currentTime = 0f;
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    public IEnumerator EndCameramove(float shakePower)
    {

        Debug.Log("Enddd");
        if (currentTime >= 10)
        {
            yield break;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(-shakePower, 0, -10), 1);
        yield return new WaitForSeconds(0.05f);

    }






}
