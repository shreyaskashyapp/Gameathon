using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
      public TextMeshProUGUI t1;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("showText",2f);
        t1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (t1.enabled && Input.GetKeyDown(KeyCode.S))
    {
      t1.enabled = false;
      Time.timeScale = 1f;
    }
    }

    public void showText(){
        Time.timeScale = 0.2f;
        t1.enabled= true;
    }
}
