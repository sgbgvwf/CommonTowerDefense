using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Property/EnemyPropertySO")]

public class EnemyPropertySO : ScriptableObject
{
    [Header("对核心伤害")]
    public int coreDamage;

    [Header("移动速度")]
    public float moveSpeed;
    public float moveSpeedScale;










}
