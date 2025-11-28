using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointManager : MonoBehaviour
{
    private static PathPointManager instance;
    public static PathPointManager Instance;

    /*
    [SerializeField] private List<GameObject> pathPointsList;

    [System.Serializable]
    public struct PathPointList
    {
        public GameObject pathPoint;

        //public Vector3 pathPointPosition; 
    }
    */
    public Dictionary<GameObject, Vector3> pathPointDict;

    

    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("单例不单一！");
        }


        PathPoint[] pathPointList = GetComponentsInChildren<PathPoint>();


        pathPointDict = new Dictionary<GameObject, Vector3>();



        foreach (var pathPoint in pathPointList)
        {
            if (pathPointDict.ContainsKey(pathPoint.gameObject))
            {
                return;
            }
            pathPointDict.Add(pathPoint.gameObject, pathPoint.transform.position);

        }
    }


    private void Start()
    {



    }











}
