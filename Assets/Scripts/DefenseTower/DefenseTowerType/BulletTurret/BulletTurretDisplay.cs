using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurretDisplay : MonoBehaviour
{

    public SpriteRenderer image;

    public AttackLockStrategyManager _strategyManager;


    private void Update()
    {
        if (_strategyManager.blackboard.lockEnemy)
        {
            Vector3 direction = _strategyManager.blackboard.attackDirection;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            image.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }



}
