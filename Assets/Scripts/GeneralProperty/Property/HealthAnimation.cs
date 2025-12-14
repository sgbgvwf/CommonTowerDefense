using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Hurt()
    {
        anim.SetTrigger("Hurt");
    }

    public void Death()
    {
        anim.SetTrigger("Dead");
    }





}
