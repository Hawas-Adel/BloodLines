using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedStateAnimator : MonoBehaviour
{
	[SerializeField] private ActorGroundedMonitor AGM;
	private ActorAnimator ActorAnimator;

	private void Awake() => ActorAnimator = GetComponentInParent<Rigidbody>().GetComponentInChildren<ActorAnimator>();

	private void Update() => ActorAnimator.AnimateIsGrounded(AGM.IsGrounded);
}
