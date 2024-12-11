using TMPro;
using UnityEngine;

public class UI_Inspector : MonoBehaviour
{



    [SerializeField] private GameObject inspectorObj;
    [SerializeField] private TMP_Text entityData;
    [SerializeField] private Entity inspectedEntity;

    public Entity InspectedEntity
    {
        get
        {
            return inspectedEntity;
        }
        set
        {
            inspectedEntity = value;
        }
    }

    void Update()
    {

        if (inspectorObj != null)
        {
            if (inspectedEntity != null)
            {
                inspectorObj.SetActive(true);
                entityData.text = $"{InspectedEntity.SelectionData.Replace(@"\n", "\n")}";
            }
            else
            {
                inspectorObj.SetActive(false);
            }
        }

    }

}
