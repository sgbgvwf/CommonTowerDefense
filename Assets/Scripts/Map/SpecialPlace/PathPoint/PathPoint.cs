using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{

    public SpriteRenderer point;


    [Header("等待一段时间")]
    public bool wait;

    public float waitDuration;



    private void Start()
    {
        point.color = new Color(240/255f, 50/255f, 50/255f, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy" || collision.tag != "BossEnemy")
        {

        }
    }












}
