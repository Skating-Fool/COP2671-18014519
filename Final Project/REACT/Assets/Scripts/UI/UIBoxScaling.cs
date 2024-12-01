using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class UI_BoxScaling : MonoBehaviour
{
    /*
     
     I'm putting this on the backburner for now,
     -until then i'll just make sure the box fits 
      -a theoretical max or use something else to show info,
       -like a %bar instead of just numbers.
     
     */


    //private TMP_Text[] textObjects;
    private Dictionary<TMP_Text, Vector2> textObjects = new();
    private TMP_Text[] tmpObjects;
    private RectTransform rectTransform;
    private Vector2 defaultSize;

    void Start()
    {
        tmpObjects = GetComponentsInChildren<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
        defaultSize = rectTransform.sizeDelta;
        foreach (TMP_Text obj in tmpObjects)
        {
            textObjects.Add(obj, obj.textBounds.size);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
