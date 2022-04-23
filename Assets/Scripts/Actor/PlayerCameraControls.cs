using Cinemachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraControls : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera VCAM;

	[MinMaxSlider(0, 15)] public Vector2 ZoomLimits = new Vector2(1, 10);
	[Min(0)] public float ZoomStep = 1;

	[SerializeField] private Stat Height;
	[SerializeField] [Range(0, 1)] private float VCAMAttachmentHeight = 0.8f;


	private CinemachineFramingTransposer FT;
	private CinemachinePOV POV;

	private void Awake()
	{
		VCAM.Follow = transform;
		FT = VCAM.GetCinemachineComponent<CinemachineFramingTransposer>();
		POV = VCAM.GetCinemachineComponent<CinemachinePOV>();
	}

	private void Update() => FT.m_TrackedObjectOffset = Vector3.up * Height.Value * VCAMAttachmentHeight;

	public void OnCameraMove(InputAction.CallbackContext ctx)
	{
		var IN = ctx.ReadValue<Vector2>();
		POV.m_HorizontalAxis.m_InputAxisValue = IN.x;
		POV.m_VerticalAxis.m_InputAxisValue = IN.y;
	}
	public void StopCamera()
	{
		POV.m_HorizontalAxis.m_InputAxisValue = 0;
		POV.m_VerticalAxis.m_InputAxisValue = 0;
	}

	public void OnCamerZoom(InputAction.CallbackContext ctx) => FT.m_CameraDistance = Mathf.Clamp(FT.m_CameraDistance - ctx.ReadValue<float>() * ZoomStep, ZoomLimits.x, ZoomLimits.y);

	private void OnDrawGizmosSelected()
	{
		if (Height)
		{
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(Vector3.zero, Height.Value * Vector3.up);
			Gizmos.DrawWireSphere(Vector3.up * Height.Value * VCAMAttachmentHeight, 0.02f);
		}
	}
}
