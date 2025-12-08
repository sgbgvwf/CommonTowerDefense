using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRelativePosition : MonoBehaviour
{
    private static MouseRelativePosition instance;
    public static MouseRelativePosition Instance;

    [HideInInspector] public Vector2 mouseScreenPosition;

    [HideInInspector] public Vector2 mouseWorldPosition;

    [HideInInspector] public Vector2Int mouseGridPosition;

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
    }


    //获取鼠标的实时位置
    public static Vector2 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;

        screenPosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        return new Vector2(worldPosition.x, worldPosition.y);
    }



    //计算鼠标相对于目标物体的相对位置
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
        mouseScreenPosition = Input.mousePosition;
        mouseWorldPosition = GetMouseWorldPosition();
        mouseGridPosition = GetMouseGridPosition();
        //实时打印鼠标坐标（调试用）
        
        //Debug.Log($"鼠标屏幕位置：{mouseScreenPosition}");
        //Debug.Log($"鼠标世界位置：{mouseWorldPosition}");
        //Debug.Log($"鼠标网格位置：{mouseGridPosition}");
        
    }

    //鼠标判定区域
    private void OnDrawGizmosSelected()
    {
        
    }

}
