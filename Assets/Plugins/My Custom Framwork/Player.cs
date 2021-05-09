using UnityEngine;
using UnityEngine.Events;

[ExecutionOrder(-600)]
public class Player : MonoBehaviour
{
	private static Rigidbody current;
	public static event UnityAction OnPlayerChanged;

	public static Rigidbody Current
	{
		get => current;
		private set
		{
			current = value;
			OnPlayerChanged?.Invoke();
		}
	}

	private void Awake()
	{
		if (Current) Destroy(Current);
		Current = GetComponentInParent<Rigidbody>();
		Destroy(this);
	}
}
