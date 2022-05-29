using UnityEngine;

public class ColliderInputReceiver : InputReceiver
{
    private Vector3 clickedPosition;
    private GameObject clickedGO;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return;
        clickedPosition = hit.point;
        clickedGO = hit.transform.gameObject;
        OnInputReceived();
    }

    public override void OnInputReceived()
    {
        foreach (var handler in inputHandlers)
            handler.ProcessInput(clickedPosition, clickedGO, null);
    }
}