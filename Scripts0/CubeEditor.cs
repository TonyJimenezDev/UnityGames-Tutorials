using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent (typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update ()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int grideSize = waypoint.GetGridSize();
        transform.position = new Vector3
            (
                waypoint.GetGridPos().x * grideSize,
                0f,
                waypoint.GetGridPos().y * grideSize
            );
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = 
            waypoint.GetGridPos().x +
            "," + 
            waypoint.GetGridPos().y;

        textMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }
}
