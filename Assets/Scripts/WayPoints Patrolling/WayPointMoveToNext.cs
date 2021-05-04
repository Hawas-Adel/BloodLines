using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachineState))]
public class WayPointMoveToNext : MonoBehaviour
{
	[SerializeField] private StateMachineManager SMM;
	[SerializeField] private WayPointPatrollingState WPPS;
	[SerializeField] private StateMachineState WaitState;
	private NavMeshAgent NMA;

	private void Awake() => NMA = GetComponentInParent<NavMeshAgent>();

	private void OnEnable() => StartCoroutine(MoveToNextWaypoint());

	private IEnumerator MoveToNextWaypoint()
	{
		NMA.SetDestination(WPPS.CurrentTargetWaypoint.GetRandomPointInRange());
		yield return null;
		yield return new WaitWhile(() => NMA.remainingDistance > NMA.stoppingDistance);
		SMM.FollowTransition(WaitState);
	}
}
