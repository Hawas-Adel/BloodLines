using UltEvents;
using UnityEngine;

[DisallowMultipleComponent]
public class StateMachine : MonoBehaviour
{
	public UltEvent OnStateChanged;

	private Transform currentState = default;
	public Transform CurrentState
	{
		get => currentState;
		set
		{
			if (value != currentState)
			{
				if (currentState) currentState.gameObject.SetActive(false);
				currentState = value;
				OnStateChanged?.InvokeSafe();
				if (currentState) currentState.gameObject.SetActive(true);
			}
		}
	}

	private void Awake()
	{
		if (transform.childCount == 0) return;

		for (int i = 0 ; i < transform.childCount ; i++)
			transform.GetChild(i).gameObject.SetActive(i == 0);

		CurrentState = transform.GetChild(0);
	}

	public void FollowTransition(Transform TargetState)
	{
		if (TargetState && TargetState != CurrentState && TargetState.IsChildOf(transform))
			CurrentState = TargetState;
	}
	public void FollowTransition(string TargetStateName) => FollowTransition(transform.Find(TargetStateName));
}
