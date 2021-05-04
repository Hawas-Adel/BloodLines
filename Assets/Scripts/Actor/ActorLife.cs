using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UltEvents;
using UnityEngine;

public class ActorLife : MonoBehaviour
{
	public UltEvent OnDeath;
	[ShowNativeProperty] public bool IsAlive { get; private set; } = true;

	private SliderStat CurrentHealth;

	private void Awake() => enabled = GetComponentInParent<Rigidbody>().GetComponentInChildren<ActorStats>().TryGetStat("Health", out CurrentHealth);

	private void OnEnable()
	{
		if (IsAlive) CurrentHealth.OnCurrentValueChanged += CheckIfHealthDepleted;
	}
	private void OnDisable() => CurrentHealth.OnCurrentValueChanged += CheckIfHealthDepleted;

	private void CheckIfHealthDepleted()
	{
		if (CurrentHealth.CurrentValue == 0)
		{
			IsAlive = false;
			OnDeath?.InvokeSafe();
		}
	}
}
