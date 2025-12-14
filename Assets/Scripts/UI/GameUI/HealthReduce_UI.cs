using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Concorde.Timer;


public class HealthReduce_UI : MonoBehaviour
{
    public GameDataSO gameDataSO;

    public GameObject image;

    TimerManager timerManager;

    private void Awake()
    {
        timerManager = new TimerManager();
    }

    private void Start()
    {
        image.SetActive(false);
    }

    private void OnEnable()
    {
        gameDataSO.HealthReduction += HealthReduceOption;
    }

    private void OnDisable()
    {
        gameDataSO.HealthReduction -= HealthReduceOption;
    }


    public void HealthReduceOption()
    {
        StartCoroutine(Option());
        //Debug.Log("0");
    }


    private IEnumerator Option()
    {
        //timerManager.Start("", 0.05f)

        image.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        image.SetActive(false);

    }





}
