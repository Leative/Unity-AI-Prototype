using Unity.VisualScripting;
using UnityEngine;

public class GetCoverState : AbstractCharacterState
{
    private float searchRadius = 10f;
    private LayerMask coverMask = Physics.AllLayers & ~(1 << 6) & ~(1 << 5);

    // Gizmos
    public GetCoverState(CharacterController controller, CharacterStateProvider stateProvider, CharacterAnimationProvider animProvider)
        : base(controller, stateProvider, animProvider)
    {
    }

    public override void EnterState()
    {
        if (HasNextCover(out Collider cover))
        {
            Vector3 destination = FindBestCoverPosition(cover);
            Controller.Agent.SetDestination(destination);
            AnimationProvider.SetRunAnimation();
        }
    }

    public override void ExitState()
    {
        // GizmoUtilities.Instance.ClearGizmos();
    }

    public override void UpdateState()
    {
        if (Controller.IsCoverTriggered)
        {
            // Cancel the cover action
            Controller.SwitchState(StateProvider.Idle);
        }

        if (ReachedDestination())
        {
            TurnToThreat();
            Controller.SwitchState(StateProvider.Cover);
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
        Vector3 threatToCoverDirection = cover.transform.position - Controller.Opponent.transform.position;
        float approximateCoverDiameter = GetApproxMaxCoverDiameter(cover);
        Vector3 positionBehindCover = cover.transform.position + (threatToCoverDirection.normalized * approximateCoverDiameter);
        Ray ray = new Ray(positionBehindCover, -threatToCoverDirection);
        GizmoUtilities.Instance.DrawLine(positionBehindCover, cover.transform.position, Color.red);
        cover.Raycast(ray, out RaycastHit backHit, approximateCoverDiameter + 1.0f);        
        Vector3 coverPoint = backHit.point + backHit.normal * 0.5f;           
        GizmoUtilities.Instance.PlaceVisualizer(coverPoint, "Cover position");
        return coverPoint;
    }

    private float GetApproxMaxCoverDiameter(Collider cover)
    {
        return Vector3.Distance(cover.bounds.min, cover.bounds.max);
    }

    private bool ReachedDestination()
    {
        bool finishedPath = false;
        if (!Controller.Agent.pathPending)
        {
            if (Controller.Agent.remainingDistance <= Controller.Agent.stoppingDistance)
            {
                if (!Controller.Agent.hasPath || Mathf.Approximately(Controller.Agent.velocity.sqrMagnitude, 0f))
                {
                    finishedPath = true;
                }
            }
        }
        return finishedPath;
    }

private void TurnToThreat()
        {
            Vector3 threatDirection = Controller.Opponent.transform.position - Controller.Agent.transform.position;
            Controller.Agent.transform.rotation = Quaternion.LookRotation(threatDirection);
        }
}