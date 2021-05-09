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

	private List<StateMachineState> ManagedStates = new List<StateMachineState>();

	private void Awake()
	{
		for (int i = 0 ; i < transform.childCount ; i++)
		{
			if (transform.GetChild(i).TryGetComponent(out StateMachineState S))
				ManagedStates.Add(S);
		}

		if (ManagedStates.Count == 0) return;

		for (int i = 0 ; i < ManagedStates.Count ; i++)
			ManagedStates[i].gameObject.SetActive(i == 0);

		CurrentState = ManagedStates[0];
	}

	public StateMachineState GetTransitionTargetState(string TargetStateName)
	{
		StateMachineState R = default;
		if (!string.IsNullOrWhiteSpace(TargetStateName))
			R = ManagedStates.FirstOrDefault(S => S.name == TargetStateName);
		return R;
	}

	public void FollowTransition(StateMachineState TargetState)
	{
		if (TargetState && TargetState != CurrentState && ManagedStates.Contains(TargetState))
			CurrentState = TargetState;
	}
	public void FollowTransition(string TargetStateName) => FollowTransition(GetTransitionTargetState(TargetStateName));
}
