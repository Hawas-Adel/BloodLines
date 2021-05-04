using UnityEngine;

public class SwitchCombatAnimations : MonoBehaviour
{
	[SerializeField] private bool InCombat = default;
	private ActorAnimator AA;

	private void Awake() => AA = GetComponentInParent<Rigidbody>().GetComponentInChildren<ActorAnimator>();

	private void OnEnable() => AA.AnimateInCombat(InCombat);
}
