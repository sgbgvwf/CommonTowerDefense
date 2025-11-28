using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("敌人预制体")]
    public GameObject enemyPrefab;

    [Header("敌人路径列表")]
    [SerializeField] private List<GameObject> planPathPointsList;
    [System.Serializable]
    public struct PlanPathList
    {
        public GameObject pathPoint;
    }

    [Header("敌人生成间隔")]
    public float spawnFrequency;



    //实例化敌人
    public void SpawnEnemy()
    {

    }




    //为每一个实例化的敌人填充路径列表
    public void FillPathList(GameObject enemy)
    {
        if (!enemy.GetComponent<EnemyPath>())
        {
            return;
        }

        foreach (GameObject pathPoint in planPathPointsList)
        {
            enemy.GetComponent<EnemyPath>().planPathPointsList.Add(pathPoint);
        }

    }






}
