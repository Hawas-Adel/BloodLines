using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionEvents : MonoBehaviour
{
	[SerializeField] private InputActionReference InputAction = default;
	[Space]
	public UltEvent<InputAction.CallbackContext> OnStarted;
	public UltEvent<InputAction.CallbackContext> OnPerformed;
	public UltEvent<InputAction.CallbackContext> OnCanceled;
	[Space]
	public UltEvent<InputAction> OnUpdate;
	public UltEvent<InputAction> OnFixedUpdate;

	private void OnEnable()
	{
		InputAction.action.started += OnStarted.InvokeX;
		InputAction.action.performed += OnPerformed.InvokeX;
		InputAction.action.canceled += OnCanceled.InvokeX;
	}
	private void OnDisable()
	{
		InputAction.action.started -= OnStarted.InvokeX;
		InputAction.action.performed -= OnPerformed.InvokeX;
		InputAction.action.canceled -= OnCanceled.InvokeX;
	}

	private void Update() => OnUpdate.InvokeX(InputAction.action);
	private void FixedUpdate() => OnFixedUpdate.InvokeX(InputAction.action);
}
