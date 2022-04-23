using NaughtyAttributes;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(Stat))]
public class SliderStat : MonoBehaviour
{
	[SerializeField] private float currentValue;
	public UltEvent OnCurrentValueChanged;

	public Stat MaxValue { get; set; }

	public float CurrentValue
	{
		get => currentValue;
		set
		{
			currentValue = Mathf.Clamp(value, 0, MaxValue.Value);
			OnCurrentValueChanged?.InvokeSafe();
		}
	}

	private void Awake()
	{
		MaxValue = GetComponent<Stat>();
		MaxValue.OnValueChanged += OnCurrentValueChanged.InvokeSafe;
	}

	private void Start() => OnCurrentValueChanged.InvokeSafe();

	private void OnValidate()
	{
		if (!MaxValue) MaxValue = GetComponent<Stat>();
		currentValue = Mathf.Clamp(currentValue, 0, MaxValue.Value);
	}
}
