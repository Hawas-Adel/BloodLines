using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachineState), typeof(StateMachineManager))]
public class WayPointPatrollingState : MonoBehaviour
{
	public enum PatrolMode { Circular, BackAndForth }


	public PatrolMode Mode = PatrolMode.Circular;
	public List<WayPoint> WayPoints = default;

	public int CurrentTargetWaypointIndex { get; private set; } = 0;
	private int ListTraversalDirection = 1;

	public WayPoint CurrentTargetWaypoint => WayPoints[CurrentTargetWaypointIndex];


	public void SetToNextNode()
	{
		CurrentTargetWaypointIndex += ListTraversalDirection;
		switch (Mode)
		{
			case PatrolMode.Circular:
				if (CurrentTargetWaypointIndex >= WayPoints.Count)
					CurrentTargetWaypointIndex = 0;
				break;
			case PatrolMode.BackAndForth:
				if (CurrentTargetWaypointIndex >= WayPoints.Count || CurrentTargetWaypointIndex < 0)
				{
					CurrentTargetWaypointIndex -= ListTraversalDirection * 2;
					ListTraversalDirection *= -1;
				}
				break;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		for (int i = 0 ; i < WayPoints.Count - 1 ; i++)
		{
			WayPoint w1 = WayPoints[i], w2 = WayPoints[i + 1];
			if (w1 && w2) Gizmos.DrawLine(w1.transform.position, w2.transform.position);
		}
		if (Mode == PatrolMode.Circular && WayPoints.Count > 2 && WayPoints[0] && WayPoints[WayPoints.Count - 1])
			Gizmos.DrawLine(WayPoints[0].transform.position, WayPoints[WayPoints.Count - 1].transform.position);
	}
}
