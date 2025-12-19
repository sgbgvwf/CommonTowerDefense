using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyProperty : MonoBehaviour
{
    public EnemyPropertySO enemyPropertyData;

    [Header("对核心伤害")]
    public int coreDamage;

    [Header("移动速度")]
    public float moveSpeed;
    public float moveSpeedScale;

    private void Awake()
    {
        DataOperation.Instance.UpdateSingleData(ref coreDamage, enemyPropertyData.coreDamage);

        DataOperation.Instance.UpdateSingleData(ref moveSpeed, enemyPropertyData.moveSpeed);
        DataOperation.Instance.UpdateSingleData(ref moveSpeedScale, enemyPropertyData.moveSpeedScale);
    }


}
