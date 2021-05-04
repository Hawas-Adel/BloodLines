using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMDeath : MonoBehaviour
{
	[SerializeField] private ActorLife ActorLife = default;
	[SerializeField] private StateMachineManager SMM = default;
	[SerializeField] private StateMachineState BusyState = default;

	private ActorAnimator AA;

	private void Awake() => AA = GetComponentInParent<Rigidbody>().GetComponentInChildren<ActorAnimator>();

	private void OnEnable() => ActorLife.OnDeath += EnterBusyState;
	private void OnDisable() => ActorLife.OnDeath -= EnterBusyState;

	private void EnterBusyState()
	{
		SMM.FollowTransition(BusyState);
		AA.AnimateDeath();
	}
}
