using NaughtyAttributes;
using UltEvents;
using UnityEngine;

public class Stat : MonoBehaviour
{
	[SerializeField] private float baseValue = 100;
	public float? BaseOverride = null;
	[SerializeField] private float multiplier = 1;
	[SerializeField] private float additive = 0;
	public float Min = Mathf.NegativeInfinity;
	public float Max = Mathf.Infinity;

	public float BaseValue
	{
		get => baseValue;
		set
		{
			baseValue = value;
			OnValueChanged.InvokeSafe();
		}
	}
	public float Multiplier
	{
		get => multiplier;
		set
		{
			multiplier = value;
			OnValueChanged.InvokeSafe();
		}
	}
	public float Additive
	{
		get => additive;
		set
		{
			additive = value;
			OnValueChanged.InvokeSafe();
		}
	}

	[ShowNativeProperty] public float Value => Mathf.Clamp((BaseOverride.HasValue ? BaseOverride.Value : BaseValue) * Multiplier + Additive, Min, Max);

	public UltEvent OnValueChanged = new UltEvent();

	private void Start() => OnValueChanged.InvokeSafe();
}
