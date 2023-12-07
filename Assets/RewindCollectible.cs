using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewindCollectible : MonoBehaviour
{
    private TextMeshProUGUI rewind;
    public RewindPlayer rp;
    // Start is called before the first frame update
    void Start()
    {
        rewind=GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        rewind.text = rp.rewindCount.ToString();
    }
}

