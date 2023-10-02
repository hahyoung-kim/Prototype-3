using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_bar : MonoBehaviour
{
    [SerializeField] Text DataText;
    GameObject obj;

    void Awake()
    {
        obj = GameObject.FindGameObjectWithTag ("Player");
    }


    void Update()
    {
        DataText.text = "Health: " + obj.GetComponent<Health>().currentHealth;
    }
}
