using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MagicalTurretAttackDisplay : MonoBehaviour
{
    public float rotationalSpeed;

    private void Update()
    {
        Quaternion localRotate = Quaternion.AngleAxis(rotationalSpeed * Time.deltaTime, new Vector3(0, 0, 1));
        transform.rotation *= localRotate;
    }
}