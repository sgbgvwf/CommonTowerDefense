using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRelativePosition : MonoBehaviour
{
    //获取鼠标的实时位置
    public static Vector2 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;

        screenPosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        return new Vector2(worldPosition.x, worldPosition.y);
    }



    // 计算鼠标相对于目标物体的相对位置
    public static Vector2 GetRelativeToObject(Transform targetObject)
    {
        return GetMouseWorldPosition() - new Vector2(targetObject.position.x, targetObject.position.y);
    }



    //坐标格点化
    public static Vector2Int GetMouseGridPosition()
    {
        Vector2 mouseWorldPosition = GetMouseWorldPosition();
        int mouseGridPositionX = Mathf.FloorToInt(mouseWorldPosition.x);
        int mouseGridPositionY = Mathf.FloorToInt(mouseWorldPosition.y);

        return new Vector2Int(mouseGridPositionX, mouseGridPositionY);
    }



    private void Update()
    {
        Vector2 mouseWorldPosition = GetMouseWorldPosition();
        Vector2Int mouseGridPosition = GetMouseGridPosition();
        //实时打印鼠标坐标（调试用）
        //Debug.Log($"鼠标世界位置：{mouseWorldPosition}");
        //Debug.Log($"鼠标网格位置：{mouseGridPosition}");
    }





}
