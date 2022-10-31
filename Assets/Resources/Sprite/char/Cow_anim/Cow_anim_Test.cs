using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow_anim_Test : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("AttackChk", true);
            Invoke("AnimStop", 4f);
        }
    }
    void AnimStop()
    {
        anim.SetBool("AttackChk", false);
    }
}
