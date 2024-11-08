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

        for (int i = 0; i < objectsList.Count; i++)
        {

            GameObject obj = objectsList[i];

            if (obj == null)
            {
                //objectsList.Remove(obj);
            }
        }

        if (sortByDistance)
        {
            objectsList.Sort(SortByDistance);
        }
    }

    private int SortByDistance(GameObject obj1, GameObject obj2)
    {
        if (obj1 == null)
        {
            return 1;
        }
        else if (obj2 == null)
        {
            return -1;
        }
        else
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
