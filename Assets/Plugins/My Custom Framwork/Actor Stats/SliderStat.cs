using UltEvents;
using UnityEngine;

[ExecutionOrder(-101)]
[DisallowMultipleComponent]
[RequireComponent(typeof(Stat))]
public class SliderStat : MonoBehaviour, IStat
{
	[SerializeField] private float currentValue;
	public UltEvent OnCurrentValueChanged;

	public string ID => MaxValue ? MaxValue.ID : null;
	public Stat MaxValue { get; private set; }
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
		if (!GetComponentInParent<ActorStats>().TryRegisterStat(this))
			Destroy(this);
	}

	private void OnValidate() => currentValue = Mathf.Clamp(currentValue, 0, GetComponent<Stat>().Value);
}
