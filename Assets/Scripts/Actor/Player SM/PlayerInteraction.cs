using System.Collections;
using System.Collections.Generic;
using PhysicsCasters;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
	[SerializeField] private InputActionReference InteractionInput = default;
	[SerializeField] private TextMeshProUGUI Text = default;
	private GameObject Canvas = default;

	private PhysicsCaster MainCamCaster = default;
	private void Awake()
	{
		MainCamCaster = Camera.main.GetComponent<PhysicsCaster>();
		Canvas = Text.GetComponentInParent<Canvas>().gameObject;
		Canvas.SetActive(false);
	}
	private void Update()
	{
		if (MainCamCaster.Cast(out RaycastHit hit))
		{
			var Interactable = hit.transform.GetComponentInChildren<IInteractable>();
			Canvas.SetActive(Interactable != null && Interactable.IsInteractionPossible(Player.Current.gameObject));
			if (Canvas.activeSelf)
			{
				Text.text = Interactable.InteractionPrompt;
				if (InteractionInput.action.triggered)
					Interactable.OnInteract?.Invoke(Player.Current.gameObject);
			}
		}
	}
}
