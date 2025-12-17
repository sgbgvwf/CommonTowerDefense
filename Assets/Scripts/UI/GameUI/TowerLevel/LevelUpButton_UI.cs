using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpButton_UI : MonoBehaviour
{
    private GameObject _currentTower;
    private LevelManager _levelManager;

    public TowerLevelDataDisplay_UI DataDisplay;
    /*
    private void Awake()
    {
        _levelManager.GetComponent<LevelManager>();//等级管理器
    }
    */
    /// <summary>
    /// 点击升级按钮：尝试执行升级
    /// </summary>
    public void ClickButton()
    {
        _currentTower = MousePointStateManager.Instance.blackboard.currentTower;//塔
        _levelManager = _currentTower.GetComponent<LevelManager>();

        if (_levelManager.LevelUp())
        {
            //_levelManager.LevelUp();
            //更新展示组件：1.更新塔的状态；2.检查等级；3.更新数据

            DataDisplay.CurrentTowerUpdate();
            //return true;
        }
        else
        {
            //return false;//接option：金钱不足
        }

    }

}
