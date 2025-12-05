using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDefenseTower : MonoBehaviour
{
    //TowerCheckDisplay towerCheckDisplay;

    public SpriteRenderer mouseDisplay;

    public bool checkOperation;



    public void Check(GameObject currentTower)
    {
        if (!checkOperation)
        {
            checkOperation = true;

            mouseDisplay.color = new Color(0, 0, 255/255f, 100/255f);

            MousePositionDisplay.Instance.positionStatic = true;


            currentTower?.GetComponent<TowerCheckDisplay>().EnterDisplay();

        }
        else
        {
            currentTower?.GetComponent<TowerCheckDisplay>().ExitDisplay();

            mouseDisplay.color = MousePointStateManager.Instance.blackboard.originalColor;

            checkOperation = false;

            MousePositionDisplay.Instance.positionStatic = false;

        }










    }









}
