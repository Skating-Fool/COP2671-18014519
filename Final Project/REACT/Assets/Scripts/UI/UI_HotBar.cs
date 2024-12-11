using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class UI_HotBar : MonoBehaviour
{

    [SerializeField] private SelectionManager selectionManager;

    void Start()
    {
        Assert.IsNotNull(selectionManager);
    }

    void Update()
    {

    }


    // Backlog
    public void SpawnObjectAtCursor(GameObject prefab)
    {
        StartCoroutine(PlaceObject(prefab));
    }

    private IEnumerator PlaceObject(GameObject prefab)
    {

        if (selectionManager.enableSelection && selectionManager.cursor != null)
        {
            GameObject newObject = Instantiate(prefab, selectionManager.cursor.transform.position, selectionManager.cursor.transform.rotation);
            newObject.transform.SetParent(selectionManager.cursor.transform);
            newObject.GetComponent<Collider>().enabled = false;
            newObject.GetComponent<WeaponBase>().canFire = false;

            yield return new WaitWhile(() => !Input.GetKey("mouse 1"));

            newObject.transform.SetParent(null);
            newObject.GetComponent<Collider>().enabled = true;
            newObject.GetComponent<WeaponBase>().canFire = true;

        }

        yield return null;

    }

}
