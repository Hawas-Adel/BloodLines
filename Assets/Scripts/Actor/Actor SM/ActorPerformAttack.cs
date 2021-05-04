using System.Linq;
using UnityEngine;

[RequireComponent(typeof(StateMachineState))]
public class ActorPerformAttack : MonoBehaviour
{
	[SerializeField] private ActorStats ActorStats = default;
	[SerializeField] private ActorCombat ActorCombat = default;
	[SerializeField] [Range(0, 1)] private float AttackAnimHitNormalizedDelay = 0.5f;

	private StateMachineManager SMM = default;
	[SerializeField] private StateMachineState UnSheathedState = default;
	private ActorAnimator AA;
	private Rigidbody RB;

	[Header("Hit Target Capsule")]
	[SerializeField] private Vector3 P1 = Vector3.zero;
	[SerializeField] private Vector3 P2 = Vector3.up;
	[SerializeField] [Min(0.05f)] private float R = 0.5f;
	[SerializeField] private LayerMask LayerMask = default;

	private void Awake()
	{
		RB = GetComponentInParent<Rigidbody>();
		AA = RB.GetComponentInChildren<ActorAnimator>();
		SMM = UnSheathedState.GetComponentInParent<StateMachineManager>();
	}

	private void OnEnable()
	{
		if (ActorStats.TryGetStat("Attack Speed", out Stat AS) && AS.Value != 0)
		{
			AA.AnimateAttack();
			float AttackTime = 1 / AS.Value;
			Invoke(nameof(FindHitTargets), AttackTime * AttackAnimHitNormalizedDelay);
			Invoke(nameof(LeaveState), AttackTime);
		}
	}

	private void FindHitTargets()
	{
		var targets = Physics.OverlapCapsule(transform.TransformPoint(P1), transform.TransformPoint(P2), R, LayerMask).ToList().ConvertAll(C => C.attachedRigidbody).Distinct().ToList();
		targets.Remove(RB);
		ActorCombat.DecladeFoundHitTargets?.Invoke(targets);
	}

	private void LeaveState() => SMM.FollowTransition(UnSheathedState);


	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireSphere(P1, R);
		Gizmos.DrawWireSphere(P2, R);
		Gizmos.DrawLine(P1, P2);
	}
}
