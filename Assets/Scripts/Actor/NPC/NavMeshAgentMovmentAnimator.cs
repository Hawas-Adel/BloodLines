using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMovmentAnimator : MonoBehaviour
{
	private NavMeshAgent NMA;
	private ActorModel AA;

	private void Awake()
	{
		NMA = GetComponentInParent<NavMeshAgent>();
		AA = NMA.GetComponentInChildren<ActorModel>();
	}

	private void Update() => AA.AnimateVelocity(NMA.transform.InverseTransformVector(NMA.velocity), 0.1f, Time.deltaTime);
}
