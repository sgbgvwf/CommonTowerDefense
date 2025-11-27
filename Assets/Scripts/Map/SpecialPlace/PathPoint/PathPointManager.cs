using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointManager : MonoBehaviour
{
    [SerializeField] private List<PathPointList> pathPointLists;


    [System.Serializable]
    public struct PathPointList
    {
        public GameObject pathPoint;

        public Vector3 pathPointPosition; 
    }



}
