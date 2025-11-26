using Concorde.Timer;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class PlaceMakerTowerController : MonoBehaviour
{
    public GroundSearch search;

    public GameObject placePrefab;
    private GameObject pointGround;

    public float changeDuration;

    public float destroyDuration;


    private void Start()
    {
        search.FindGround();
        ChooseGround();
    }

    public void ChooseGround()
    {
        //遍历字典中是ground的地块并添加进列表以便于随机
        List<Vector2Int> position = new List<Vector2Int>();
        foreach (Vector2Int pos in search.findResultDict.Keys)
        {
            Debug.Log("Key:" + pos);
            position.Add(pos);
        }

        int count = position.Count;

        //有ground
        if(count > 0)
        {

            System.Random rand = new System.Random();
            int randomIndex = rand.Next(0, count);
            Vector2Int choicePos = position[randomIndex];

            search.findResultDict.TryGetValue(choicePos, out GameObject ground);


            StartCoroutine(WorkDuration(choicePos, ground));
        }

        //无ground，采取3秒后自动销毁和补偿机制
        else
        {
            StartCoroutine(DestroyTime());

        }

    }


    public void MakePlace(Vector3 changePosition, GameObject pointGround)
    {
        GameObject.Destroy(pointGround);
        Instantiate(placePrefab, changePosition, quaternion.identity);
    }


    public IEnumerator WorkDuration(Vector2Int choicePos, GameObject ground)
    {
        Vector3 choiceTowerPos = new Vector3(choicePos.x, choicePos.y, 0);

        yield return new WaitForSeconds(changeDuration);

        search.FindGround();

        if (search.findResultDict[choicePos].tag != "DefenseTower")
        {
            MakePlace(choiceTowerPos, ground);
        }

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    public IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(destroyDuration);

        float moneyBack = 0.8f * gameObject.GetComponent<TowerMoney>().placementCost;

        Money.Instance.ChangeMoney(moneyBack);

        GameObject.Destroy(gameObject);
    }

}
