using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewindCollectible : MonoBehaviour
{
    private TextMeshProUGUI rewind;
    public RewindPlayer rp;
    void Start()
    {
        rewind=GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        rewind.text = rp.rewindCount.ToString();
    }
}

