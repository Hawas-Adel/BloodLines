using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class ActorCombat : MonoBehaviour
{
	[SerializeField] private ActorStats ActorStats = default;

	[System.NonSerialized] public UltEvent<List<Rigidbody>> DecladeFoundHitTargets;
	public UltEvent<Rigidbody> OnHitEnemy;
	public UltEvent<Rigidbody> OnHitByEnemy;

	private Rigidbody RB;

	private void Awake() => RB = GetComponentInParent<Rigidbody>();

	private void OnEnable() => DecladeFoundHitTargets += InvokeHitEventsOnFirstTarget;
	private void OnDisable() => DecladeFoundHitTargets -= InvokeHitEventsOnFirstTarget;

	public void InvokeHitEventsOnFirstTarget(List<Rigidbody> Targets)
	{
		if (Targets != null && Targets.Count > 0)
		{
			InvokeHitEventsOnTarget(Targets[0]);
		}
	}
	public void InvokeHitEventsOnTarget(Rigidbody Target)
	{
		OnHitEnemy?.Invoke(Target);
		Target.GetComponentInChildren<ActorCombat>().OnHitByEnemy?.Invoke(RB);
	}

	public void DealDamageToTarget(Rigidbody Enemy)
	{
		var EnemyStats = Enemy.GetComponentInChildren<ActorStats>();
		if (EnemyStats &&
			ActorStats.TryGetStat("Damage", out Stat Damage) &&
			EnemyStats.TryGetStat("Armor", out Stat EnemyArmor) &&
			EnemyStats.TryGetStat("Health", out SliderStat EnemyHealth))
		{
			float DR = -80 / (EnemyArmor.Value + 80) + 1;
			float NetDamage = Mathf.Max(1 - DR, 0) * Damage.Value;
			if (ActorStats.TryGetStat("Critical Chance", out Stat CriticalChance) &&
				ActorStats.TryGetStat("Critical Damage", out Stat CriticalDamage) &&
				Random.value <= CriticalChance.Value)
			{
				NetDamage *= CriticalDamage.Value;
			}
			EnemyHealth.CurrentValue -= NetDamage;
		}
	}

	private void DebugEvent(Rigidbody RB) => Debug.Log(RB.name);
}
