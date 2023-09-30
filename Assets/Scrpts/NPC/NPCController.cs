using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NPCController : MonoBehaviour
{

    public GameObject notification;
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            notification.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            notification.SetActive(false);
        }
    }
}