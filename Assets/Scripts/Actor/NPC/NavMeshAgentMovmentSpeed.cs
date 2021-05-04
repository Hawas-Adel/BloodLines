using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMovmentSpeed : MonoBehaviour
{
	private NavMeshAgent NMA;
	private Stat MS;

	private void OnEnable()
	{
		NMA = GetComponentInParent<NavMeshAgent>();
		if (NMA && NMA.GetComponentInChildren<ActorStats>().TryGetStat("Movment Speed", out MS))
		{
			MS.OnValueChanged += UpdateNavMeshAgentMovmentSpeed;
			UpdateNavMeshAgentMovmentSpeed();
		}
	}

	private void OnDisable()
	{
		if (MS)
		{
			MS.OnValueChanged -= UpdateNavMeshAgentMovmentSpeed;
		}
	}

	private void UpdateNavMeshAgentMovmentSpeed() => NMA.speed = MS.Value;

}
