using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCheckDisplay : MonoBehaviour
{
    public SpriteRenderer attackRangeImage;

    //public Canvas Canvas;


    private void Awake()
    {
        attackRangeImage.enabled = false;
    }

    public void EnterDisplay()
    {
        attackRangeImage.enabled = true;




    }



    public void ExitDisplay()
    {
        attackRangeImage.enabled = false;




    }




}
