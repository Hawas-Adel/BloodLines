using Cinemachine;
using UnityEngine;

[ExecuteBefore(typeof(Player))]
[RequireComponent(typeof(CinemachineVirtualCameraBase), typeof(PlayerCameraControls))]
public class AttachCameraToPlayer : MonoBehaviour
{
	private void Awake() => Player.OnPlayerChanged += AttachCamera;

	private void AttachCamera()
	{
		GetComponent<CinemachineVirtualCameraBase>().Follow = Player.Current.transform;
		Player.Current.GetComponentInChildren<PlayerUICanvasToggle>().CameraControls = GetComponent<PlayerCameraControls>();
	}
}
