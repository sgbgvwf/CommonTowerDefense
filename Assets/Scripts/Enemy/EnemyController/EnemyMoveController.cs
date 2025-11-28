using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyMoveController : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool move;

    [Header("移动方向")]
    public Vector3 Dirrection;

    public EnemyPath enemyPath;

    private bool faceLeft;

    [Header("移动速度")]
    public float moveSpeed;


    //游戏开始前的初始化
    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();


    }

    //每个固定帧速率的帧调用的更新
    private void FixedUpdate()
    {
        if (move)
        Move();
    }


    //移动和朝向
    public void Move()
    {
        transform.position += Dirrection * moveSpeed;

        //方向
        Dirrection = (enemyPath.currentTargetPathPoint.transform.position - transform.position + new Vector3(0.5f, 0.5f, 0)).normalized;

        if ((faceLeft && Dirrection.x > 0) || (!faceLeft && Dirrection.x < 0))
        {
            TurnBack();
        }



    }

    //人物翻转
    private void TurnBack()
    {
        if (Dirrection.x < 0)
        {
            faceLeft = true;
        }
        else
        {
            faceLeft = false;
        }
        transform.localScale = new Vector3(-1 * transform.localScale.x, 1 * transform.localScale.y, 1 * transform.localScale.z);
    }













}
