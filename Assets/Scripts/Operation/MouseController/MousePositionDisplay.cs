using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionDisplay : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.position = (Vector3Int)MouseRelativePosition.GetMouseGridPosition();
    }
}
