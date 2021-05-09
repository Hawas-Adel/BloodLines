using UnityEngine;

public class SwitchCombatAnimations : MonoBehaviour
{
	[SerializeField] private bool InCombat = default;
	private ActorModel AA;

	private void Awake() => AA = GetComponentInParent<Rigidbody>().GetComponentInChildren<ActorModel>();

	private void OnEnable() => AA.AnimateInCombat(InCombat);
}
