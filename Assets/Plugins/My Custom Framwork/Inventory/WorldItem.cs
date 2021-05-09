using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class WorldItem : MonoBehaviour, IInteractable
{
	[SerializeField] private Item item;
	public Item Item
	{
		get => item;
		set
		{
			item = value;
			GetComponent<Rigidbody>().mass = item.Weight;
			for (int i = 0 ; i < transform.childCount ; i++)
			{
				if (Application.isPlaying) Destroy(transform.GetChild(i));
				else DestroyImmediate(transform.GetChild(i--));
			}
			if (Application.isPlaying) Instantiate(Item.WorldModel, transform);
#if UNITY_EDITOR
			else UnityEditor.PrefabUtility.InstantiatePrefab(Item.WorldModel, transform);
#endif
		}
	}

	[Min(1)] public int Count = 1;


	public string InteractionPrompt => $"Pickup\n{item.name}{(Count > 1 ? $"({Count})" : "")}";
	bool IInteractable.IsInteractionPossible(GameObject SRC) => item && SRC.GetComponentInChildren<Inventory>();
	public UnityAction<GameObject> OnInteract => PickUpItem;

	private void PickUpItem(GameObject SRC)
	{
		SRC.GetComponentInChildren<Inventory>().AddItem(Item, Count);
		Destroy(gameObject);
	}
}
