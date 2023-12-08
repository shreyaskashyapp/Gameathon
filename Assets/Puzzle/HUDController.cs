using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactionText;

    public void EnableInteractionText(string Text)
    {
        interactionText.text = Text ;
        interactionText.gameObject.SetActive(true); // Call the method with ()
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false); // Call the method with ()
    }
}