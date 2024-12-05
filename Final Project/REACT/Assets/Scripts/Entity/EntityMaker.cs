using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityMaker : MonoBehaviour
{

    public int cost = 5;

    [SerializeField] private GameObject prefab;
    [SerializeField] private ResourceManager resourceManager;

    void Start()
    {
        SelectionManager.OnSelect.AddListener(OnSelectEvent);

        if (resourceManager == null)
        {
            resourceManager = FindObjectOfType<ResourceManager>();
        }
    }

    public void OnSelectEvent(GameObject gameObject, int mouseClickNum)
    {
        if (transform.gameObject.Equals(gameObject))
        {

            if (mouseClickNum == 1 && resourceManager.metal >= 5)
            {
                resourceManager.metal -= 5;
                Instantiate(prefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }
    }

}
