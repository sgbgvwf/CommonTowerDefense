using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProcessDisplay2 : MonoBehaviour
{
    public ResourceTowerController resourceTowerController;

    void Update()
    {
        transform.Rotate(0, 0, -20 * resourceTowerController.stoppingSpeed * Time.deltaTime);
    }
}
