using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{

    public bool enableSelection;

    public GameObject cursor;

    public static UnityEvent<GameObject, int> OnSelect;
    
    private Ray ray;
    private RaycastHit rayHit;

    private void Awake()
    {
        OnSelect ??= new UnityEvent<GameObject, int>();
    }

    void Update()
    {

        if (enableSelection)
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                this.rayHit = rayHit;
                if (!EventSystem.current.IsPointerOverGameObject())
                {

                    cursor.SetActive(true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        Select(1);
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        Select(2);
                    }
                    else
                    {

                        cursor.transform.position = rayHit.point;
                        Debug.DrawLine(Camera.main.gameObject.transform.position, rayHit.transform.position, Color.red);
                        Debug.DrawLine(rayHit.point, rayHit.point + (rayHit.normal / 2), Color.red);
                        Debug.DrawLine(Camera.main.gameObject.transform.position, rayHit.point, Color.green);

                    }

                }
                else
                {
                    cursor.SetActive(false);
                }

            }
            else
            {
                cursor.SetActive(false);
            }

        }

    }

    private void Select(int mouseNum)
    {
        Debug.DrawLine(Camera.main.gameObject.transform.position, rayHit.transform.position, Color.red, 2f);
        Debug.DrawLine(rayHit.point, rayHit.point + (rayHit.normal / 2), Color.red, 2f);
        Debug.DrawLine(Camera.main.gameObject.transform.position, rayHit.point, Color.green, 2f);
        OnSelect.Invoke(rayHit.transform.gameObject, mouseNum);
    }

}
