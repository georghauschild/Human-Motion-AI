using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{

    public GameObject NPC;
    public float rotationSpeed = 100.0f; // Drehgeschwindigkeit, anpassbar im Inspector
    private Animator animator;

    void Start()
    {
        animator = NPC.GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetButtonDown("w"))
        {
            animator.SetBool("isStarting", true);
            
        }
        if (Input.GetButton("a"))
        {
            // Sanfte Drehung nach links
            NPC.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetButton("d"))
        {
            // Sanfte Drehung nach rechts
            NPC.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetButtonDown("s"))
        {
            animator.SetBool("isStarting", false);
            animator.SetBool("isStopping", true);
        }
    }
}
