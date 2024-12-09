using UnityEngine;

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

            if (mouseClickNum == 1 && resourceManager.metal >= cost)
            {
                resourceManager.metal -= cost;
                Instantiate(prefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }
    }

}
