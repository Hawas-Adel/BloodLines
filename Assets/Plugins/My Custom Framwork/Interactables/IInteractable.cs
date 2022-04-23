using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
	string InteractionPrompt { get; }
	bool IsInteractionPossible(GameObject SRC);
	UnityAction<GameObject> OnInteract { get; }
}
