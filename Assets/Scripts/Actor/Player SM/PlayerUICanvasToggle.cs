using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachineManager))]
public class PlayerUICanvasToggle : MonoBehaviour
{
	[SerializeField] private StateMachineState FallbackState = default;
	[SerializeField] private StateMachineManager BusyStateSMM = default;

	private StateMachineManager SMM = default;
	[System.NonSerialized] public PlayerCameraControls CameraControls = default;
	private float FallbackTimeScale;

	private void Awake()
	{
		SMM = GetComponent<StateMachineManager>();
		FallbackTimeScale = Time.timeScale;
	}

	public void ToggleState(StateMachineState state)
	{
		if (SMM.CurrentState != state)
		{
			SMM.FollowTransition(state);
			BusyStateSMM.FollowTransition("Busy");
			CameraControls.enabled = false;
			Time.timeScale = 0;
		}
		else
		{
			SMM.FollowTransition(FallbackState);
			BusyStateSMM.FollowTransition("Active");
			CameraControls.enabled = true;
			Time.timeScale = FallbackTimeScale;
		}
	}
}
