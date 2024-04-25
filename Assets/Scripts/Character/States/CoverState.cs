using Unity.VisualScripting;
using UnityEngine;

public class CoverState : AbstractCharacterState
{
    private float searchRadius = 10f;
    private LayerMask coverMask = Physics.AllLayers & ~(1 << 6) & ~(1 << 5);

    // Gizmos
    public CoverState(CharacterController controller, CharacterStateProvider stateProvider, CharacterAnimationProvider animProvider)
        : base(controller, stateProvider, animProvider)
    {
    }

    public override void EnterState()
    {
        if (HasNextCover(out Collider cover)) {  
            Vector3 destination = FindBestCoverPosition(cover);          
            Controller.Agent.SetDestination(destination);
            AnimationProvider.SetRunAnimation();
        } else {
            //Controller.SwitchState(StateProvider.Shoot);
        }
    }

    public override void ExitState()
    {        
        GizmoUtilities.Instance.ClearGizmos();
    }

    public override void UpdateState()
    {
        if (Controller.IsCoverTriggered)
        {
            // Cancel the cover action
            Controller.SwitchState(StateProvider.Idle);
        }
    }

    private bool HasNextCover(out Collider cover)
    {
        GizmoUtilities.Instance.DrawCircle(Controller.Transform.position, searchRadius, Color.green);
        Collider[] coverObjects = Physics.OverlapSphere(Controller.transform.position, searchRadius, coverMask);

        if (coverObjects.Length == 0)
        {
            cover = null;
            return false;
        }

        cover = coverObjects[0];
        foreach (Collider c in coverObjects)
        {
            float coverDistance = Vector3.Distance(Controller.transform.position, c.transform.position);
            if (coverDistance < Vector3.Distance(Controller.transform.position, cover.transform.position))
            {
                cover = c;
            }
        }
        GizmoUtilities.Instance.PlaceVisualizer(cover, "Cover");
        return true;
    }

    private Vector3 FindBestCoverPosition(Collider cover)
        {
            Vector3 threatDirection = cover.transform.position - Controller.Opponent.transform.position;
            Vector3 raycastEnd = Controller.Opponent.transform.position + threatDirection + (threatDirection.normalized * GetApproxMaxCoverDiameter(cover));
            GizmoUtilities.Instance.PlaceVisualizer(raycastEnd, "Raycast length");
            Ray ray = new Ray(Controller.Opponent.transform.position, threatDirection);
            GizmoUtilities.Instance.DrawLine(Controller.Opponent.transform.position, raycastEnd, Color.red);
            Physics.Raycast(ray, out RaycastHit hit);
            GizmoUtilities.Instance.PlaceVisualizer(hit.point, "Raycast hit");

            Vector3 farSideOfCover = hit.point + threatDirection.normalized * GetApproxMaxCoverDiameter(cover);
            GizmoUtilities.Instance.PlaceVisualizer(farSideOfCover, " farside");
            Vector3 coverPosition = cover.ClosestPoint(farSideOfCover);

            GizmoUtilities.Instance.PlaceVisualizer(coverPosition, "Cover position");
            return coverPosition;
        }

        private float GetApproxMaxCoverDiameter(Collider cover)
        {
            return Vector3.Distance(cover.bounds.min, cover.bounds.max);
        }
}