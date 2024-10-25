using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Machine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Selectable.OnSelect.AddListener(OnSelectEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (gameObject == this)
        {
            Debug.Log("WHY ARE YOU CLICKING ME!!!!!!");
        }
    }

}
