using NaughtyAttributes;
using UnityEngine;

public class TransformMonitor : MonoBehaviour
{
	[ShowNativeProperty] public Vector3 Velocity { get; private set; }
	[ShowNativeProperty] public Vector3 AngularVelocity { get; private set; }

	private Vector3 _OldPosition;
	private Vector3 _OldRotation;

	private void Awake()
	{
		_OldPosition = transform.localPosition;
		_OldRotation = transform.localEulerAngles;
	}

	private void FixedUpdate()
	{
		var _DPosition = transform.localPosition - _OldPosition;
		Velocity = _DPosition / Time.fixedDeltaTime;

		var _DRotation = transform.localEulerAngles - _OldRotation;
		AngularVelocity = _DRotation / Time.fixedDeltaTime;

		_OldPosition = transform.localPosition;
		_OldRotation = transform.localEulerAngles;
	}
}
