using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GizmoUtilities : MonoBehaviour
{
    [SerializeField]
    private Visualizer visualizerPrefab;
    [SerializeField]
    private LineRenderer lineRendererPrefab;

    private List<GameObject> spawnedGizmos = new List<GameObject>();

    private static GizmoUtilities _instance;
    public static GizmoUtilities Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GizmoUtilities>("Prefabs/GizmoUtilities");
                if (_instance == null) {
                    
                }
            }
            return _instance;
        }
    }

    public GameObject DrawCircle(Vector3 position, float radius, Color color)
    {
        LineRenderer lr = Instantiate(lineRendererPrefab, position, Quaternion.identity);
        int steps = 10;
        lr.positionCount = steps;
        for (int i = 0; i < steps; i++)
        {
            float portion = (float)i / steps;
            float radian = portion * 2 * Mathf.PI;
            float unitCircleX = Mathf.Cos(radian);
            float unitCircleY = Mathf.Sin(radian);

            Vector3 point = new Vector3(unitCircleX * radius, position.y, unitCircleY * radius);
            lr.SetPosition(i, point);
            lr.material.color = color;
        }
        spawnedGizmos.Add(lr.gameObject);
        return lr.gameObject;
    }

    public void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        LineRenderer lr = Instantiate(lineRendererPrefab);
        lr.positionCount = 2;
        lr.SetPositions(new Vector3[] { start, end });
        lr.material.color = color;
        spawnedGizmos.Add(lr.gameObject);
    }

    public GameObject PlaceVisualizer(Vector3 position, string label)
    {
        Visualizer v = Instantiate(visualizerPrefab, position, Quaternion.identity);
        v.SetLabel(label);
        spawnedGizmos.Add(v.gameObject);
        return v.gameObject;
    }

    public GameObject PlaceVisualizer(Collider collider, string label)
    {
        Vector3 position = collider.transform.position + new Vector3(0, collider.bounds.extents.y, 0);
        Visualizer v =  Instantiate(visualizerPrefab, position, Quaternion.identity);
        v.SetLabel(label);
        spawnedGizmos.Add(v.gameObject);
        return v.gameObject;
    }
    
    /// <summary>
    /// Destroys all gizmos created with the utility
    /// </summary>
    public void ClearGizmos() {
        spawnedGizmos.ForEach(o => Destroy(o));
        spawnedGizmos.Clear();
    }

    /// <summary>
    /// Destroys the given Gizmo
    /// </summary>
    /// <param name="obj"></param>
    public void ClearGizmo(GameObject obj) {
        spawnedGizmos.Remove(obj);
        Destroy(obj);
    }
}