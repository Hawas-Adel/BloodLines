using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PhysicsCasters;
using TMPro;
using UnityEngine;

public class InteractableMonitor : MonoBehaviour
{
	public static IInteractable IInteractable { get; private set; }

	[SerializeField] private TextMeshProUGUI InteractableUI_TXT = default;
	[Min(0)] public float MinInteractionDistance = 1.5f;

	private PhysicsCaster PC;

	private void Awake()
	{
		PC = Camera.main.GetComponent<PhysicsCaster>();
		InvokeRepeating(nameof(ScanForInteractable), 0.1f, 0.1f);
	}

	private void ScanForInteractable()
	{
		IInteractable = PC.CastAll().ToList().
			ConvertAll(hit => hit.transform.GetComponent<IInteractable>()).
			FirstOrDefault(II => II != null &&
			Vector3.Distance((II as Component).transform.position, PlayerREF.Current.transform.position) <= MinInteractionDistance &&
			II.IsInteractionPossible(PlayerREF.Current.gameObject));
		InteractableUI_TXT.gameObject.SetActive(IInteractable != null);
		if (InteractableUI_TXT.gameObject.activeSelf)
		{
			InteractableUI_TXT.text = IInteractable.InteractionPrompt;
		}
	}

	public void PlayerTryInteract()
	{
		if (IInteractable != null)
		{
			IInteractable.OnInteract?.Invoke(PlayerREF.Current.gameObject);
		}
	}
}
