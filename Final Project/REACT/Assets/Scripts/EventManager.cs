using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{

    public static UnityEvent<GameObject, int> OnSelect;

    void Start()
    {
        OnSelect ??= new UnityEvent<GameObject, int>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("M1");
            Select(1);
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            Debug.Log("M2");
            Select(2);
        }

    }

    private void Select(int mouseNum)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            Debug.Log($"Hit: {hit.transform.gameObject}");
            OnSelect.Invoke(hit.transform.gameObject, mouseNum);
        }
    }

}
