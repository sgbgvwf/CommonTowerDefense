using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class AttackTimeTypes : MonoBehaviour
{
    /// <summary>
    /// 瞬时发射
    /// </summary>
    /// <param name="bullet">子弹</param>
    /// <param name="position">发射位置</param>
    /// <param name="direction">发射方向</param>
    /// <param name="parent">父物体</param>
    /// <returns></returns>
    public GameObject ImmediatelyAttack(GameObject bullet, Vector3 position, EnemyDetection enemyDetection, Transform parent)
    {
        GameObject _Bullet = Instantiate(bullet, position, Quaternion.identity, parent);
        _Bullet.name = "Bullet";

        Attack(_Bullet, enemyDetection.direction);

        return _Bullet;
    }


    /// <summary>
    /// 延迟发射
    /// </summary>
    /// <param name="bullet">子弹</param>
    /// <param name="position">发射位置</param>
    /// <param name="direction">发射方向</param>
    /// <param name="parent">父物体</param>
    /// <param name="delayTime">延迟时间</param>
    /// <returns></returns>
    public GameObject DelayAttack(GameObject bullet, Vector3 position, EnemyDetection enemyDetection, Transform parent, float delayTime)
    {
        GameObject _Bullet = Instantiate(bullet, position, Quaternion.identity, parent);
        _Bullet.name = "Bullet";

        StartCoroutine(Delay(bullet, position, enemyDetection, parent, delayTime));

        return _Bullet;
    }


    private IEnumerator Delay(GameObject bullet, Vector3 position, EnemyDetection enemyDetection, Transform parent, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        DelayAttackDataUpdate();

        Attack(bullet, enemyDetection.direction);
    }


    public void DelayAttackDataUpdate()
    {
        
    }


    private void Attack(GameObject bullet, Vector3 direction)
    {
        bullet.GetComponent<FlyerStraightController>().direction = direction;
    }



}