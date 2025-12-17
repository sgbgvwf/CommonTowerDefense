using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDefenseTower : MonoBehaviour
{
    //TowerCheckDisplay towerCheckDisplay;

    public SpriteRenderer mouseDisplay;

    public bool checkOperation;

    public TowerLevelDataDisplay_UI towerLevelDataDisplay_UI;

    public TowerLevelWindowDisplayButton_UI towerLevelWindowDisplayButton_UI;

    public void Check(GameObject currentTower)
    {
        if (!checkOperation)
        {
            checkOperation = true;

            mouseDisplay.color = new Color(0, 0, 255/255f, 100/255f);

            MousePositionDisplay.Instance.positionStatic = true;

            if(currentTower.GetComponent<TowerCheckDisplay>())
            {
                currentTower.GetComponent<TowerCheckDisplay>().EnterDisplay();
            }

            towerLevelDataDisplay_UI.OpenAllDisplay();
            towerLevelDataDisplay_UI.CurrentTowerUpdate();

            if (!towerLevelWindowDisplayButton_UI.isDisplay)
            {
                towerLevelWindowDisplayButton_UI.Display();
            }

        }
        else
        {
            if (currentTower.GetComponent<TowerCheckDisplay>())
            {
                currentTower.GetComponent<TowerCheckDisplay>().ExitDisplay();
            }

            mouseDisplay.color = MousePointStateManager.Instance.blackboard.originalColor;

            checkOperation = false;

            MousePositionDisplay.Instance.positionStatic = false;

            towerLevelDataDisplay_UI.CloseAllDisplay();
        }

    }

    public void CheckReset()
    {
        Check(MousePointStateManager.Instance.blackboard.currentTower);
        checkOperation = false;
        towerLevelDataDisplay_UI.CloseAllDisplay();
    }




}
