using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractionSpot : MonoBehaviour
{
    public SideType sideType;
    public List<Vector2Int> coords;
}