using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{

    public GameObject NPC;

    void Update()
    {
        if(Input.GetButtonDown("1 Key"))
        {
            NPC.GetComponent<Animator>().Play("walk");
        }
        if (Input.GetButtonDown("2 Key"))
        {
            NPC.GetComponent<Animator>().Play("left");
        }
        if (Input.GetButtonDown("3 Key"))
        {
            NPC.GetComponent<Animator>().Play("right");
        }
    }
}
