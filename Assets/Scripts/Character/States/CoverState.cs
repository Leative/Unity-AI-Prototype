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
        if (!HasNextCover(out Collider cover)) {
            //agent.SetDestination();
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
}