using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item", order = 0)]
public class Item : ScriptableObject
{
	public const int _TextureSize = 512;

	[Required] [ShowAssetPreview] public GameObject WorldModel = default;
	[Min(0)] public float Weight = 0;
	[Min(0)] public int Value = 0;
	[ResizableTextArea] public string Description = "";

	public GameObject SpawnInScene(Vector3 Position, Quaternion Rotation)
	{
		var GO = new GameObject($"WI ~ {name}");
		GO.AddComponent<WorldItem>().Item = this;
		GO.transform.position = Position;
		GO.transform.rotation = Rotation;
		return GO;
	}
#if UNITY_EDITOR
	[Button]
	protected void SpawnInScene() => SpawnInScene(UnityEditor.SceneView.lastActiveSceneView.pivot, UnityEditor.SceneView.lastActiveSceneView.rotation);
#endif

	public virtual void HandleDescriptionUI(ItemDescriptionUI UI)
	{
		if (UI)
		{
			if (UI.TitleText)
				Instantiate(UI.TitleText, UI.TitleText.transform.parent).text = name;

			if (UI.Image)
				Instantiate(UI.Image, UI.Image.transform.parent).sprite = Sprite.Create(RuntimePreviewGenerator.GenerateModelPreview(WorldModel.transform, _TextureSize, _TextureSize), new Rect(0, 0, _TextureSize, _TextureSize), 0.5f * Vector2.one);

			if (UI.GenericText && !string.IsNullOrWhiteSpace(Description))
				Instantiate(UI.GenericText, UI.GenericText.transform.parent).text = Description;
		}
	}
}
