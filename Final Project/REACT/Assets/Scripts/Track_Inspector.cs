using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;

[CustomEditor(typeof(Track))]
public class Track_Inspector : Editor
{

    private Button b_AddNewPoint;
    private Button b_RemoveFirst;
    private Button b_RemoveLast;
    private Button b_DeleteFirst;
    private Button b_DeleteLast;

    private Track track;

    public VisualTreeAsset m_InspectorXML;

    private void OnEnable()
    {
        track = (Track)target;
    }

    public override VisualElement CreateInspectorGUI()
    {

        VisualElement root = new VisualElement();
        
        m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/InspectorUI/Track_Inspector.uxml");
        root = m_InspectorXML.Instantiate();

        b_AddNewPoint = root.Q<Button>("AddNewPoint");
        b_RemoveFirst = root.Q<Button>("RemoveFirst");
        b_RemoveLast = root.Q<Button>("RemoveLast");
        b_DeleteFirst = root.Q<Button>("DeleteFirst");
        b_DeleteLast = root.Q<Button>("DeleteLast");

        b_AddNewPoint.RegisterCallback<ClickEvent>(CreateNewPoint);
        b_RemoveFirst.RegisterCallback<ClickEvent>(RemoveFirst);
        b_RemoveLast.RegisterCallback<ClickEvent>(RemoveLast);
        b_DeleteFirst.RegisterCallback<ClickEvent>(DeleteFirst);
        b_DeleteLast.RegisterCallback<ClickEvent>(DeleteLast);

        VisualElement InspectorFoldout = root.Q("Default_Inspector");
        
        // Attach a default Inspector to the Foldout.
        InspectorElement.FillDefaultInspector(InspectorFoldout, serializedObject, this);

        return root;

    }

    private void CreateNewPoint(ClickEvent evt)
    {
        track.CreateNewPoint();
    }

    private void RemoveLast(ClickEvent evt)
    {
        track.RemoveLast();
    }

    private void RemoveFirst(ClickEvent evt)
    {
        track.RemoveFirst();
    }

    private void DeleteLast(ClickEvent evt)
    {
        track.DeleteLast();
    }

    private void DeleteFirst(ClickEvent evt)
    {
        track.DeleteFirst();
    }

}
