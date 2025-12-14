using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameData_UI : MonoBehaviour
{
    public GameDataSO gameDataSO;

    public TextMeshProUGUI _coreHealth;

    public TextMeshProUGUI _money;

    public TextMeshProUGUI _moneyMax;

    public TextMeshProUGUI _level;

    private int initCoreHealth;

    //private bool isLoad = false;

    private void Start()
    {
        /*
        if (!gameDataSO.maxMoney)
        {
            _money.gameObject.SetActive(true);
            _moneyMax.gameObject.SetActive(false);
        }
        else
        {
            _money.gameObject.SetActive(false);
            _moneyMax.gameObject.SetActive(true);
        }
        */
        //DelayLoad();
    }


    private void Update()
    {
        initCoreHealth = CoreHealth.Instance.initialHealth;

        _coreHealth.text = gameDataSO.coreHealth.ToString() + "/" + initCoreHealth.ToString();

        //if(!isLoad)

        _level.text = gameDataSO.thisLevel.ToString();

        if (!gameDataSO.maxMoney)
        {
            if (!(_money.gameObject && !_moneyMax.gameObject))
            {
                _money.gameObject.SetActive(true);
                _moneyMax.gameObject.SetActive(false);
            }
            _money.text = gameDataSO.money.ToString();
        }
        else
        {
            if (!(!_money.gameObject && _moneyMax.gameObject))
            {
                _money.gameObject.SetActive(false);
                _moneyMax.gameObject.SetActive(true);
            }
            _moneyMax.text = gameDataSO.money.ToString() + "/" + gameDataSO.maxMoneyCount.ToString();
        }
    }

    /*
    private IEnumerator DelayLoad()
    {

        yield return new WaitForSeconds(0.1f);

        if (!gameDataSO.maxMoney)
        {
            _money.gameObject.SetActive(true);
            _moneyMax.gameObject.SetActive(false);
        }
        else
        {
            _money.gameObject.SetActive(false);
            _moneyMax.gameObject.SetActive(true);
        }

        isLoad = true;
    }
    */
}
