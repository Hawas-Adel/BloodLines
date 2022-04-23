using System.Collections.Generic;
using System.Linq;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public const int _TextureSize = 256;

	[SerializeField] private Image ItemImage = default;
	[SerializeField] private TextMeshProUGUI ItemName = default;
	[SerializeField] private TextMeshProUGUI ItemCount = default;
	[SerializeField] private ItemDescriptionUI ItemDescriptionUI = default;
	[SerializeField] private GameObject EquippedMarker = default;

	[SerializeField] private List<Behaviour> OnHoverListners = default;

	public Item Item { get; private set; }
	public Inventory Inventory { get; private set; }

	public void OnPointerEnter(PointerEventData eventData) => OnHoverListners.ForEach(M => M.enabled = true);
	public void OnPointerExit(PointerEventData eventData) => OnHoverListners.ForEach(M => M.enabled = false);

	public void SetUIForItem(Item item, Inventory Inv, Equipment EQ)
	{
		int count = 0;
		if (item && Inv)
			count = Inv.HasItem(item);
		gameObject.SetActive(count > 0);
		if (gameObject.activeSelf)
		{
			Item = item;
			Inventory = Inv;
			if (ItemImage)
			{
				ItemImage.sprite = Sprite.Create(RuntimePreviewGenerator.GenerateModelPreview(item.WorldModel.transform, _TextureSize, _TextureSize),
						new Rect(0, 0, _TextureSize, _TextureSize), 0.5f * Vector2.one);
				ItemImage.color = Color.white;
			}

			if (ItemName)
				ItemName.text = item.name;

			if (ItemCount)
			{
				ItemCount.gameObject.SetActive(count > 1);
				if (ItemCount.gameObject.activeSelf)
					ItemCount.text = $"({count})";
			}

			EquippedMarker.SetActive(EquippedMarker && EQ && item is EquipableItem e && EQ.IsEquipped(e));
		}
	}

	public void TryUseItem()
	{
		if (Item is UsableItem usableItem)
		{
			usableItem.UseItem(PlayerREF.Current.gameObject);
		}
		else
		{
			Debug.Log("Item Not Usable");
		}
	}

	public void DropItem() => Inventory.DropItem(Item);

	private void OnEnable() => OnHoverListners.ForEach(M => M.enabled = false);
}
