using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolInitialization : MonoBehaviour
{

    [System.Serializable]
    public struct objectProperty
    {
        public GameObject prefab;

        public int initCount;

        public int maxCount;

    }

    [Header("∂‘œÛ≥ÿ Ù–‘")]
    [SerializeField] private List<objectProperty> objectProperties;


    private void Start()
    {
        foreach(var property in objectProperties)
        {
            ObjectPoolManager.Instance.InitPool(property.prefab, property.initCount, property.maxCount);
        }
    }





}
