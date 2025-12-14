using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option_UI : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    public float displayDuration;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.gameObject.SetActive(false);
    }

    public void Display()
    {
        textMeshProUGUI.gameObject.SetActive(true);

        StartCoroutine(DisplayTime());
    }

    private IEnumerator DisplayTime()
    {
        

        yield return new WaitForSeconds(displayDuration);

        textMeshProUGUI.gameObject.SetActive(false);
    }









}
