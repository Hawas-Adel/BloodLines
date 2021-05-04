using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class FitNavMeshAgentToCapsule : MonoBehaviour
{
	private void OnEnable()
	{
		var NMA = GetComponentInParent<NavMeshAgent>();
		var Capsule = NMA.GetComponentInChildren<CapsuleCollider>();
		if (NMA && Capsule)
		{
			NMA.baseOffset = -0.15f;
			NMA.height = Capsule.height + NMA.baseOffset;
			NMA.radius = Capsule.radius;
		}
	}
}
