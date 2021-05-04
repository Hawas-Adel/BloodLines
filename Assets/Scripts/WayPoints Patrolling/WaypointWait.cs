using System.Collections;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(StateMachineState))]
public class WaypointWait : MonoBehaviour
{
	[SerializeField] private StateMachineManager SMM;
	[SerializeField] private WayPointPatrollingState WPPS;
	[MinMaxSlider(0, 30)] public Vector2 WaitTimeRange = Vector2.one;
	[SerializeField] private StateMachineState MoveState;

	private void OnEnable() => StartCoroutine(WaitAtWaypoint());

	private IEnumerator WaitAtWaypoint()
	{
		WPPS.SetToNextNode();
		yield return new WaitForSeconds(Random.Range(WaitTimeRange.x, WaitTimeRange.y));
		SMM.FollowTransition(MoveState);
	}
}
