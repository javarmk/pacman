using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MaxScoreText : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text = GetComponent<Text>();

        text.text = "Ky luc: " + GameManager.instance.maxScore;
    }
}
