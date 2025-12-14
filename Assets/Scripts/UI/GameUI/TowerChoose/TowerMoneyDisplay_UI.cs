using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerMoneyDisplay_UI : MonoBehaviour
{
    public TextMeshProUGUI number;

    private DefenseTowerChoose_UI defenseTowerChoose_UI;

    private void Awake()
    {
        defenseTowerChoose_UI = GetComponent<DefenseTowerChoose_UI>();
    }

    private void Start()
    {
        number.text = BuildManager.Instance.towerPrefabDictionary[defenseTowerChoose_UI.defenseTowerType].GetComponent<TowerPlaceMoney>().placementCost.ToString();
    }

}
