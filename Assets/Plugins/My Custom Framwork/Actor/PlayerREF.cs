using UltEvents;
using UnityEngine;
using UnityEngine.AI;

[ExecutionOrder(-100)]
public class PlayerREF : MonoBehaviour
{
	private static PlayerREF current = default;
	public static UltEvent OnPlayerChanged = new UltEvent();
	public static PlayerREF Current
	{
		get => current;
		private set
		{
			current = value;
			OnPlayerChanged?.InvokeSafe();
		}
	}

	private void Awake()
	{
		if (Current && Current != this)
			Destroy(Current);// destroy Old
		Current = this;// assign new

		Debug.Log($"Player set to : {Current.name}");
	}
}
