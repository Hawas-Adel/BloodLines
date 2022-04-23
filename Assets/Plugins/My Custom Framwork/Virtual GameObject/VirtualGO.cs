using NaughtyAttributes;
using UnityEngine;

public abstract class VirtualGO : ScriptableObject
{
	[Layer] public int Layer = 0;
	[Tag] public string Tag = "Untagged";

	public Color GizmosColor = new Color(1, 1, 1, 0.1f);
	[ShowAssetPreview(128, 128)] public GameObject WorldModel = default;
	public GameObject[] Plugins = default;

	public virtual void OnInstanceAwake(VirtualGOInstance instance) { }
	public virtual void OnInstanceDestroy(VirtualGOInstance instance) { }
	public virtual void OnGizmosSelected(VirtualGOInstance instance) { }

	public GameObject Spawn(Vector3 LocalPos = default, Quaternion LocalRot = default, Transform Parent = null)
	{
		var VGO = new GameObject($"VGO - {name}");
		VGO.SetActive(false);
		VGO.AddComponent<VirtualGOInstance>().VirtualGO = this;
		VGO.SetActive(true);
		VGO.transform.SetParent(Parent);
		VGO.transform.localPosition = LocalPos;
		VGO.transform.localRotation = LocalRot;
		return VGO;
	}

#if UNITY_EDITOR
	[Button]
	protected void Spawn()
	{
		GameObject New = Spawn(UnityEditor.SceneView.lastActiveSceneView.pivot, UnityEditor.SceneView.lastActiveSceneView.rotation);
		UnityEditor.Undo.RegisterCreatedObjectUndo(New, "Added Virtual GameObject To Scene");
		UnityEditor.Selection.activeGameObject = New;
	}
#endif
}
