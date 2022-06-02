using System;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class InputHandler : MonoBehaviour, IInputHandler
{
    private GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }
    
    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
    {
        gameController.OnClick(selectedObject);
    }
}