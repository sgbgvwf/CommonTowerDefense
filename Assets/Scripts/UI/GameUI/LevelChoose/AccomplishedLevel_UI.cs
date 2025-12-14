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
        image.SetActive(false);
    }

    private void Start()
    {
        if (levelAccomplishDataSO.levelsAccomplishDict.ContainsKey(thislevel))
        {
            StateUpdate();

        }
    }


    public void StateUpdate()
    {
        if (levelAccomplishDataSO.levelsAccomplishDict[thislevel])
        {
            image?.SetActive(true);
        }
    }




}
