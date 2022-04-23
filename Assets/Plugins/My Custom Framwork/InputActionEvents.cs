using UltEvents;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionEvents : MonoBehaviour
{
	[SerializeField] private InputActionReference InputAction = default;
	[SerializeField] private bool PersistentAction = false;
	[Space]
	public UltEvent<InputAction.CallbackContext> OnStarted;
	public UltEvent<InputAction.CallbackContext> OnPerformed;
	public UltEvent<InputAction.CallbackContext> OnCanceled;
	[Space]
	public UltEvent<InputAction> OnUpdate;
	public UltEvent<InputAction> OnFixedUpdate;

	private void Awake()
	{
		if (PersistentAction) InputAction.action.Enable();
	}
	private void OnEnable()
	{
		if (InputAction)
		{
			if (!PersistentAction) InputAction.action.Enable();
			InputAction.action.started += OnStarted.InvokeSafe;
			InputAction.action.performed += OnPerformed.InvokeSafe;
			InputAction.action.canceled += OnCanceled.InvokeSafe;
		}
	}
	private void OnDisable()
	{
		if (InputAction)
		{
			if (!PersistentAction) InputAction.action.Disable();
			InputAction.action.started -= OnStarted.InvokeSafe;
			InputAction.action.performed -= OnPerformed.InvokeSafe;
			InputAction.action.canceled -= OnCanceled.InvokeSafe;
		}
	}
	private void OnDestroy()
	{
		if (PersistentAction) InputAction.action.Disable();
	}

	private void Update()
	{
		if (InputAction)
		{
			OnUpdate.InvokeSafe(InputAction.action);
		}
	}
	private void FixedUpdate()
	{
		if (InputAction)
		{
			OnFixedUpdate.InvokeSafe(InputAction.action);
		}
	}
}
