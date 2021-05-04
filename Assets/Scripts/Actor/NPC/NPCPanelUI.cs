using UnityEngine;

public class NPCPanelUI : MonoBehaviour
{
	[SerializeField] private Transform UIWorldAnchor = default;
	[SerializeField] private Vector3 UIOffset = Vector3.up * 2;


	private Transform MainCamera;

	private void Awake() => MainCamera = Camera.main.transform;

	private void Update()
	{
		transform.position = UIWorldAnchor.TransformPoint(UIOffset);
		transform.forward = MainCamera.forward;
	}
}
