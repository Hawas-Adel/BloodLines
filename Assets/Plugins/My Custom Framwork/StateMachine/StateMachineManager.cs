using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class StateMachineManager : MonoBehaviour
{
	private StateMachineState currentState = default;
	public StateMachineState CurrentState
	{
		get => currentState;
		set
		{
			if (value != currentState)
			{
				if (currentState) currentState.gameObject.SetActive(false);
				currentState = value;
				if (currentState) currentState.gameObject.SetActive(true);
			}
		}
	}

	[SerializeField] private List<StateMachineState> AnyStateTransitions;

	private void Awake()
	{
		List<StateMachineState> ChildStates = new List<StateMachineState>();
		for (int i = 0 ; i < transform.childCount ; i++)
		{
			if (transform.GetChild(i).TryGetComponent(out StateMachineState S))
				ChildStates.Add(S);
		}

		if (ChildStates.Count == 0) return;

		for (int i = 0 ; i < ChildStates.Count ; i++)
			ChildStates[i].gameObject.SetActive(i == 0);

		CurrentState = ChildStates[0];
	}

	public StateMachineState GetTransitionTargetState(string TargetStateName)
	{
		StateMachineState R = default;
		if (!string.IsNullOrWhiteSpace(TargetStateName))
		{
			R = AnyStateTransitions.FirstOrDefault(S => S.name == TargetStateName);
			if (!R) R = CurrentState.StateTransitions.FirstOrDefault(S => S.name == TargetStateName);
		}
		return R;
	}

	public void FollowTransition(StateMachineState TargetState)
	{
		if (TargetState && TargetState != CurrentState && TargetState.transform.IsChildOf(transform) &&
			(AnyStateTransitions.Contains(TargetState) || CurrentState.StateTransitions.Contains(TargetState)))
		{
			CurrentState = TargetState;
		}
	}
	public void FollowTransition(string TargetStateName)
	{
		var targetState = GetTransitionTargetState(TargetStateName);
		if (targetState) FollowTransition(targetState);
	}
}
