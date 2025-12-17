using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAccomplish_UI : MonoBehaviour
{
    public GameDataSO gameDataSO;

    public GameObject image;

    private void Awake()
    {
        image.SetActive(false);
    }

    private void OnEnable()
    {
        gameDataSO.accomplish += Display;
    }

    private void OnDisable()
    {
        gameDataSO.accomplish -= Display;
    }

    public void Display()
    {
        image.SetActive(true);
    }



}
