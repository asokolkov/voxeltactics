using System;
using UnityEngine;

[RequireComponent(typeof(Field))]
public class FieldInputHandler : MonoBehaviour, IInputHandler
{
    private Field field;

    private void Awake()
    {
        field = GetComponent<Field>();
    }
    
    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
    {
        field.OnSquareSelected(inputPosition, selectedObject);
    }
}