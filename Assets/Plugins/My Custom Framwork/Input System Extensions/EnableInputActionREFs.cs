using UnityEngine;
using UnityEngine.InputSystem;

public class EnableInputActionREFs : MonoBehaviour
{
	[SerializeField] private InputActionReference[] REFs = default;

	private void OnEnable()
	{
		for (int i = 0 ; i < REFs.Length ; i++)
		{
			REFs[i].action.Enable();
		}
	}
	private void OnDisable()
	{
		for (int i = 0 ; i < REFs.Length ; i++)
		{
			REFs[i].action.Disable();
		}
	}
}
