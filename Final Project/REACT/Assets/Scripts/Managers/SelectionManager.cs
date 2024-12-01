using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{

    public static UnityEvent<GameObject, int> OnSelect;

    private void Awake()
    {
        OnSelect ??= new UnityEvent<GameObject, int>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Select(1);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Select(2);
        }

    }

    private void Select(int mouseNum)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            //Debug.Log($"Hit: {hit.transform.gameObject}");
            OnSelect.Invoke(hit.transform.gameObject, mouseNum);
        }
    }

}
