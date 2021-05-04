using UnityEngine;

[RequireComponent(typeof(StateMachineManager))]
public class PlayerSMGroundedMonitor : MonoBehaviour
{
	[SerializeField] private ActorGroundedMonitor AGM = default;
	[SerializeField] private StateMachineManager SMM = default;
	[SerializeField] private StateMachineState GroundedState = default;
	[SerializeField] private StateMachineState AirBourneState = default;

	private void FixedUpdate()
	{
		if (AGM.IsGrounded)
			SMM.FollowTransition(GroundedState);
		else
			SMM.FollowTransition(AirBourneState);
	}
}
