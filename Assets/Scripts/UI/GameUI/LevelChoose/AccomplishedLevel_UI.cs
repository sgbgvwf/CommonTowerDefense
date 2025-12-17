using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccomplishedLevel_UI : MonoBehaviour
{
    public LevelAccomplishDataSO levelAccomplishDataSO;

    public GameObject image;

    private Button button;

    public Levels thislevel;

    private void Awake()
    {
        //Debug.Log("1");
        image.SetActive(false);
        if (levelAccomplishDataSO == null)
        {
            return;
        }

        bool isAccomplished = levelAccomplishDataSO.GetLevelAccomplishState(thislevel);
        image.SetActive(isAccomplished);
        //Debug.Log("2");

        button = GetComponent<Button>();
    }

    private void Start()
    {
        var levelPair = levelAccomplishDataSO._serializedLevelData.Find(p => p.level == thislevel);

        if (!levelPair.isUnLocked)
        {
            button.enabled = false;
        }
        else
        {
            button.enabled = true;
        }
    }


}
