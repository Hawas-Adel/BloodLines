using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedStateAnimator : MonoBehaviour
{
	[SerializeField] private ActorGroundedMonitor AGM;
	private ActorModel ActorAnimator;

	private void Awake() => ActorAnimator = GetComponentInParent<Rigidbody>().GetComponentInChildren<ActorModel>();

	private void Update() => ActorAnimator.AnimateIsGrounded(AGM.IsGrounded);
}
