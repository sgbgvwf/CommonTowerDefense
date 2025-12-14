using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver_UI : MonoBehaviour
{
    public GameDataSO gameDataSO;

    public GameObject image;

    //public Button backButton;

    private bool dead = false;

    private bool back = false;

    private void Start()
    {
        image.SetActive(false);
    }


    private void OnEnable()
    {
        gameDataSO.Death += Dead;
    }

    private void OnDisable()
    {
        gameDataSO.Death -= Dead;
    }


    private void Update()
    {
        if (!dead)
        {
            return;
        }

        if (back)
        {
            image.SetActive(false);
        }
    }

    private void Dead()
    {
        Time.timeScale = 0;
        image.SetActive(true);
        dead = true;
    }

    public void BackButton()
    {
        back = true;
    }


}
