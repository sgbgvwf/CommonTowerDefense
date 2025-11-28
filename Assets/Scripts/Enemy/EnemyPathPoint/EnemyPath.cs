using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] public List<GameObject> planPathPointsList;



    public GameObject currentTargetPathPoint;

    private int count = 0;


    private void Awake()
    {
        //planPathPointsList = new List<GameObject>();
        currentTargetPathPoint = planPathPointsList[0];
    }


    private void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PathPoint" && collision.gameObject == currentTargetPathPoint)
        {
            count++;
            Debug.Log(count);
            if(count < planPathPointsList.Count)
            {
                currentTargetPathPoint = planPathPointsList[count];
            }
            else
            {
                Destroy(gameObject);
            }
                Debug.Log(currentTargetPathPoint);
        }


    }





}
