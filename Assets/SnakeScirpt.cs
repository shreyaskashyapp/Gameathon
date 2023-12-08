using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScirpt : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    public void Start(){
        animator = GetComponent<Animator>();
    }
    public GameObject mouth;
    public LayerMask playerLayer;
    bool isInRange;

    // Update is called once per frame
    void Update()
    {
        isInRange = Physics.CheckSphere(mouth.transform.position, 5f, playerLayer);

        if (isInRange)
        {
            animator.SetBool("inRange", true);
        }
        else
        {
            animator.SetBool("inRange", false);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mouth.transform.position, 15f);
    }
}
