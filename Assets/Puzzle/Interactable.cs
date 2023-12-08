using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;
    public UnityEvent onInteraction;


    PlayerInteraction interaction;

    // Reference to the TV object
    public GameObject Binoculars;
    public GameObject hollow_knight;

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
         interaction = FindObjectOfType<PlayerInteraction>();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void Interact()
    {    
        
            onInteraction.Invoke();
        
    }


    public void EnableOutline()
    {
        outline.enabled = true;
    }

    // Check if the TV is in the player's line of sight
    public bool IsLookingAtTV()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider != null && hit.collider.gameObject == Binoculars)
            {
                
                return true;
            }
        }

        return false;
    }
    public void FixedUpdate()
    {
        hollow();
    }

    // Check if the TV is NOT in the player's line of sight
    public bool IsNotLookingAtTV()
    {
        return !IsLookingAtTV();
    }

    public void hollow()
    {

        if ( interaction.NumberOfCollectibles == 1)
        {
            Destroy(Binoculars);
            Destroy(hollow_knight);
        }
    }
}
