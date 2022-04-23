using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(VirtualGOInstance), typeof(Rigidbody))]
public class WorldItem : MonoBehaviour, IInteractable
{
	[Min(1)] public int Count = 1;

	public Item Item { get; private set; }

	private void Awake()
	{
		if (GetComponent<VirtualGOInstance>().VirtualGO is Item item)
		{
			Item = item;
		}
		else
		{
			Destroy(this);
			return;
		}

		var RB = GetComponent<Rigidbody>();
		RB.mass = item.Weight;
	}

	public string InteractionPrompt => $"Pickup\n{Item.name}{(Count > 1 ? $"({Count})" : "")}";
	bool IInteractable.IsInteractionPossible(GameObject SRC) => Item && SRC.GetComponentInChildren<Inventory>();
	public UnityAction<GameObject> OnInteract => PickUpItem;

	private void PickUpItem(GameObject SRC)
	{
		SRC.GetComponentInChildren<Inventory>().AddItem(Item, Count);
		Destroy(gameObject);
	}
}
