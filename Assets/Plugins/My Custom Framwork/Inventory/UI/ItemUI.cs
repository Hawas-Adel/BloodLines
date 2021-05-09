using TMPro;
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

	public Item Item { get; private set; }
	public Inventory SRC { get; private set; }

	public void OnPointerEnter(PointerEventData eventData) => ItemDescriptionUI.SetUIForItem(this);
	public void OnPointerExit(PointerEventData eventData) => ItemDescriptionUI.SetUIForItem(null);
	private void OnDisable() => ItemDescriptionUI.SetUIForItem(null);

	public void SetUIForItem(Item item, Inventory _SRC)
	{
		Item = item;
		SRC = _SRC;

		var count = SRC.HasItem(item);
		gameObject.SetActive(item && count > 0);
		if (gameObject.activeSelf)
		{
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
		}
	}

	public void TryUseItem()
	{
		if (Item is UsableItem usableItem)
		{
			usableItem.UseItem(Player.Current.gameObject);
			SetUIForItem(Item, SRC);
		}
		else
		{
			Debug.Log("Item Not Usable");
		}
	}
}
