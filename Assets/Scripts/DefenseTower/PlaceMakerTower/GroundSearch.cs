using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSearch : MonoBehaviour
{

    private Vector2 detectionPosition;

    public float detectionRadius;



    public Dictionary<Vector2Int, GameObject> findResultDict = new Dictionary<Vector2Int, GameObject>();


    private void Awake()
    {
        detectionPosition = (Vector2)transform.position + new Vector2(0.1f, 0.1f);
    }

    public void FindGround()
    {
        Debug.Log("查找");
        //查找周围所有带有 Collider2D 的对象
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionPosition, detectionRadius);

        //遍历检测到的所有碰撞体
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("遍历");
            Vector2Int GridPos = Vector2Int.FloorToInt(hitCollider.transform.position);

            if (!findResultDict.ContainsKey(GridPos) && hitCollider.transform.position != transform.position)//不能把自己加进去
            {
                Debug.Log(GridPos);
                findResultDict.Add(GridPos, hitCollider.gameObject);
            }
            else
            {
                //比较tag并添加字典
                if (hitCollider.tag == "DefenseTower" && hitCollider.transform.position != transform.position)
                {
                    //Debug.Log(findResultDict[GridPos]);
                    findResultDict.Remove(GridPos);
                    findResultDict.Add(GridPos, hitCollider.gameObject);
                    //Debug.Log(findResultDict[GridPos]);
                }
                else
                {
                    Debug.Log("Nothing");
                    continue;
                }

            }
            

        }
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(detectionPosition, detectionRadius);
    }

}
