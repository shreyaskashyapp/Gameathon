using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpacePrompt : MonoBehaviour
{
  // Start is called before the first frame update
  public TextMeshProUGUI t1;
  private void OnTriggerEnter(Collider other)
  {
    Debug.Log(other.gameObject.tag);
    if (other.gameObject.tag == "Player")
    {
      Time.timeScale = 0.2f;
      t1.enabled = true;
    }
  }
  private void Start()
  {
    t1.enabled = false;
  }
  private void Update()
  {
    if (t1.enabled && Input.GetButtonDown("Jump"))
    {
      t1.enabled = false;
      Invoke("reset", 0.5f);
    }
  }

  private void reset()
  {
      Time.timeScale=1f;
  }
}