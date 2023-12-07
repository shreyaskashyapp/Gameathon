using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleUI : MonoBehaviour
{
    private TextMeshProUGUI collectibleText;
    public PlayerInteraction playerInteraction;
    // Start is called before the first frame update
    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();


    }

    // Update is called once per frame
    public void UpdateCollectibleText()
    {
        collectibleText.text = playerInteraction.NumberOfCollectibles.ToString() + "/3";
    }

    public void FixedUpdate()
    {
        UpdateCollectibleText();
    }
}