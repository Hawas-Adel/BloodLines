using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorCombat : MonoBehaviour
{
	public event UnityAction<ActorCombat> OnHitTarget;
	public event UnityAction<ActorCombat> OnHitSelf;

	[SerializeField] private Stat Damage;
	[SerializeField] private SliderStat CurrentHealth;
	[SerializeField] private Stat Armor;

	public bool IsAlive { get; private set; } = true;

	private void Awake()
	{
		OnHitTarget += ActorCombat_OnHitTarget;
		CurrentHealth.OnCurrentValueChanged += CurrentHealth_OnCurrentValueChanged;
	}

	private void CurrentHealth_OnCurrentValueChanged() => IsAlive = IsAlive && CurrentHealth.CurrentValue == 0;

	private void ActorCombat_OnHitTarget(ActorCombat Target)
	{
		Target.OnHitSelf?.Invoke(this);
		DealDamge(Target);
	}

	private void DealDamge(ActorCombat target)
	{
		var DR = -80 / (target.Armor.Value + 80) + 1;
		var NetDamge = DR * Damage.Value;
		target.CurrentHealth.CurrentValue -= NetDamge;
	}

	public void PerformAttack() { } /*=> Actor.Animator.CrossFade("Attack 0", 0.1f, 1);*/

	public void HandleTargets(params ActorCombat[] Targets) { }

	//private void OnEnable() => Actor.OnAnimationEvent += OnAnimEventTriggered;
	//private void OnDisable() => Actor.OnAnimationEvent -= OnAnimEventTriggered;

	private void OnAnimEventTriggered(string EventName, object Param)
	{
		if (EventName == "AttackHit")
		{
			Debug.Log("Hit Frame");
		}
	}
}
