using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public List<GameObject> planPathPointsList;

    public GameObject currentTargetPathPoint;

    private int count = 0;

    //private bool arriveCurrentPathPoint = false;

    private void Start()
    {
        currentTargetPathPoint = planPathPointsList[1];
    }

    private void Update()
    {
        PathPointPositionDetection();
    }
    

    private void PathPointPositionDetection()
    {
        if((transform.position - currentTargetPathPoint.transform.position - new Vector3(0.5f, 0.5f, 0)).magnitude < 0.05f)
        {
            if (currentTargetPathPoint.gameObject.GetComponent<PathPoint>())
            {
                PathPoint _pathPoint = currentTargetPathPoint.GetComponent<PathPoint>();

                if (this.tag == "PathPoint")
                {
                    _pathPoint.StartCoroutine(_pathPoint.Display());
                }
                else if (this.tag == "Enemy")
                {
                    _pathPoint.StartCoroutine(_pathPoint.Wait(this.gameObject));

                }
            }

            count++;
            if (count < planPathPointsList.Count)
            {
                currentTargetPathPoint = planPathPointsList[count];
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }



}
