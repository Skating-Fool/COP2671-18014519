using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    public UnityEvent OnFoundChanged;

    public bool sortByDistance = true;

    public List<GameObject> objectsList = new List<GameObject>();

    private void Awake()
    {
        if (OnFoundChanged == null)
        {
            OnFoundChanged = new UnityEvent();
        }
    }

    private void Update()
    {
        if (sortByDistance)
        {
            objectsList.Sort(SortByDistance);
        }
    }

    private int SortByDistance(GameObject obj1, GameObject obj2)
    {
        float distanceA = Vector3.Distance(transform.position, obj1.transform.position);
        float distanceB = Vector3.Distance(transform.position, obj2.transform.position);
        
        if (distanceA > distanceB)
        {
            return 1;
        }
        else if (distanceA < distanceB)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsList.Add(other.gameObject);
        OnFoundChanged.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        objectsList.Remove(other.gameObject);
        OnFoundChanged.Invoke();
    }

}
