using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccomplishedLevel_UI : MonoBehaviour
{
    public LevelAccomplishDataSO levelAccomplishDataSO;

    public GameObject image;

    public Levels thislevel;

    private void Awake()
    {
        Debug.Log("1");
        image.SetActive(false);
        if (levelAccomplishDataSO == null)
        {
            return;
        }

        bool isAccomplished = levelAccomplishDataSO.GetLevelAccomplishState(thislevel);
        image.SetActive(isAccomplished);
        Debug.Log("2");
    }
    /*
    private void Start()
    {
        if (levelAccomplishDataSO.levelsAccomplishDict.ContainsKey(thislevel))
        {
            StateUpdate();

        }
    }


    public void StateUpdate()
    {
        if (levelAccomplishDataSO.GetLevelAccomplishState(thislevel))
        {
            image?.SetActive(true);
        }
    }
    */



}
