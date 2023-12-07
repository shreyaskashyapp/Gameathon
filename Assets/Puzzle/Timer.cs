using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    // Start is called before the first frame update
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = seconds.ToString();
        }
        else if (remainingTime <= 0)
            remainingTime = 3;
            //Re start scene

    }
}
