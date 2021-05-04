using NaughtyAttributes;
using UltEvents;
using UnityEngine;

[ExecutionOrder(-102)]
[DisallowMultipleComponent]
public class Stat : MonoBehaviour, IStat
{
	[SerializeField] private float baseValue = 100;
	[SerializeField] private float multiplier = 1;
	[SerializeField] private float additive = 0;
	[ShowNativeProperty] public float Value => BaseValue * Multiplier + Additive;

	public string ID => name;
	public float BaseValue
	{
		get => baseValue;
		set
		{
			baseValue = value;
			OnValueChanged?.InvokeSafe();
		}
	}
	public float Multiplier
	{
		get => multiplier;
		set
		{
			multiplier = value;
			OnValueChanged?.InvokeSafe();
		}
	}
	public float Additive
	{
		get => additive;
		set
		{
			additive = value;
			OnValueChanged?.InvokeSafe();
		}
	}

	public UltEvent OnValueChanged;

	private void Awake()
	{
		if (!GetComponentInParent<ActorStats>().TryRegisterStat(this))
			Destroy(this);
	}
}
