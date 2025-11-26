using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionDisplay : MonoBehaviour
{
    private static MousePositionDisplay instance;
    public static MousePositionDisplay Instance;


    public bool positionStatic;


    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
    }


    private void Update()
    {
        if (positionStatic)
        {
            return;
        }

        gameObject.transform.position = (Vector3Int)MouseRelativePosition.GetMouseGridPosition();
    }

    public bool SamePosition()
    {
        if (new Vector2Int( (int)transform.position.x, (int)transform.position.y) == MouseRelativePosition.GetMouseGridPosition())
        {
            return true;
        }
        else
        {
            return false;
        }

    }



}
