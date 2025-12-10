using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;

public class EnemySpawn : MonoBehaviour
{
    private TimerManager timerManager;

    private bool beginSpawn;

    [Header("敌人预制体")]
    public GameObject enemyPrefab;

    public Transform enemiesParent;

    [Header("敌人路径列表")]
    public List<GameObject> planPathPointsList;

    [Header("敌人生成数量")]
    public int planAmount;

    private int currentAmount = 1;

    private enum EnemySpawnTimeType
    {
        AbsoluteSpawnTime,
        RelativeSpawnTime
    }

    [Header("初始敌人生成时间")]
    [SerializeField] EnemySpawnTimeType enemySpawnTimeType;
    public float spwanInitialTime;

    public Component lastSpawn;

    [Header("敌人生成间隔时间")]
    public float spawnFrequency;


    private void Awake()
    {
        //Debug.Log("awake");
        timerManager = new TimerManager();
    }


    private void Start()
    {
        if(enemySpawnTimeType == EnemySpawnTimeType.AbsoluteSpawnTime)
        {
            timerManager.Start("", spwanInitialTime);

        }
        
    }

    private void Update()
    {
        if (enemySpawnTimeType == EnemySpawnTimeType.RelativeSpawnTime)
        {
            if (lastSpawn == null && !beginSpawn)
            {
                timerManager.Start("", spwanInitialTime);
                beginSpawn = true;
            }
        }
        SpawnEnemy();
        
    }


    //实例化敌人
    public void SpawnEnemy()
    {
        if (!timerManager.IsFinished(""))
        {
            return;
        }
        if(planPathPointsList.Count == 0)
        {
            Debug.LogError("No Setting Paths' List");
            return;
        }
        if (currentAmount <= planAmount)
        {
            timerManager.Remove("");
            GameObject newEnemy = ObjectPoolManager.Instance.GetObject(enemyPrefab, planPathPointsList[0].transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity, enemiesParent);
            FillPathList(newEnemy);
            newEnemy.GetComponent<EnemyMoveController>().move = true;

            //newEnemy.name = enemyPrefab.name + currentAmount;
            currentAmount++;
            timerManager.Start("", spawnFrequency);
        }
        else
        {
            Destroy(this);
        }
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
