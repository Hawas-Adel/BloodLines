using Cinemachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class PlayerCameraControls : MonoBehaviour
{
	[SerializeField] private InputActionReference CameraLookActionREF = default;
	private CinemachinePOV POV = default;
	[Space]
	[SerializeField] private InputActionReference CameraZoomActionREF = default;
	private CinemachineFramingTransposer FramingTransposer = default;
	[SerializeField] private float ZoomStep = -1;
	[SerializeField] [MinMaxSlider(0, 15)] private Vector2 ZoomRange = new Vector2(1, 7);


	private void Awake()
	{
		var VCAM = GetComponent<CinemachineVirtualCamera>();
		POV = VCAM.GetCinemachineComponent<CinemachinePOV>();
		FramingTransposer = VCAM.GetCinemachineComponent<CinemachineFramingTransposer>();
		transform.SetParent(null, true);
	}

	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		CameraLookActionREF.action.performed += CameraLook_performed;
		CameraZoomActionREF.action.performed += CameraZoom_performed;
	}
	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		CameraLookActionREF.action.performed -= CameraLook_performed;
		CameraZoomActionREF.action.performed -= CameraZoom_performed;
	}

	private void CameraLook_performed(InputAction.CallbackContext obj)
	{
		var v2 = obj.ReadValue<Vector2>();
		POV.m_HorizontalAxis.m_InputAxisValue = v2.x;
		POV.m_VerticalAxis.m_InputAxisValue = v2.y;
	}

	private void CameraZoom_performed(InputAction.CallbackContext obj) => FramingTransposer.m_CameraDistance =
				Mathf.Clamp(FramingTransposer.m_CameraDistance + ZoomStep * obj.ReadValue<float>(), ZoomRange.x, ZoomRange.y);
}
