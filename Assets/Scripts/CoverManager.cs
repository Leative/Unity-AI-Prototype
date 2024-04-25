using UnityEngine;

public class CoverManager {
    
    private Transform _coveringObject;
    private LayerMask _coverMask;
    
    //Physics.AllLayers & ~(1 << 6) & ~(1 << 5);

    public bool EnableDebugGizmos {get; set;}
   
    public CoverManager(Transform coveringObject, LayerMask coverMask) {
        _coveringObject = coveringObject;
        _coverMask = coverMask;
    }

    public bool HasNextCover(out Collider cover, float radius)
    {
        GizmoUtilities.Instance.ClearGizmos();
        GizmoUtilities.Instance.DrawCircle(_coveringObject.position, radius, Color.green);
        Collider[] coverObjects = Physics.OverlapSphere(_coveringObject.position, radius, _coverMask);

        if (coverObjects.Length == 0)
        {
            cover = null;
            return false;
        }

        cover = coverObjects[0];
        foreach (Collider c in coverObjects)
        {
            float coverDistance = Vector3.Distance(_coveringObject.position, c.transform.position);
            if (coverDistance < Vector3.Distance(_coveringObject.position, cover.transform.position))
            {
                cover = c;
            }
        }
        GizmoUtilities.Instance.PlaceVisualizer(cover, "Cover");
        return true;
    }

}