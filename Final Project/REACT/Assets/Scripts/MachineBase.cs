using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBase : Entity
{

    public virtual void Start()
    {
        SelectionManager.OnSelect.AddListener(OnSelectEvent);
    }

    void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {
            Debug.Log("Click Event Not Implemented Yet");
        }
    }

}
