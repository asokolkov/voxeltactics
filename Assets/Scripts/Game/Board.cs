using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform boardPivot;
    public float squareSize = 1;

    public Vector3 CalculatePosition(Vector2Int coords)
    {
        return boardPivot.position + new Vector3(coords.x * squareSize, 0, 
            coords.y * squareSize);
    }
}