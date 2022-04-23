using UnityEngine;

[SelectionBase]
public class VirtualGOInstance : MonoBehaviour
{
	[SerializeField] [NaughtyAttributes.Expandable] public VirtualGO VirtualGO = default;

	private void Awake()
	{
		if (VirtualGO)
		{
			gameObject.SetActive(false);
			Instantiate(VirtualGO.WorldModel, transform);
			System.Array.ForEach(VirtualGO.Plugins, Plugin => Instantiate(Plugin, transform));
			gameObject.tag = VirtualGO.Tag;
			System.Array.ForEach(GetComponentsInChildren<Transform>(), T => T.gameObject.layer = VirtualGO.Layer);
			VirtualGO.OnInstanceAwake(this);
			gameObject.SetActive(true);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void OnDestroy()
	{
		if (VirtualGO)
		{
			VirtualGO.OnInstanceDestroy(this);
		}
	}


	private void OnDrawGizmosSelected()
	{
		if (!Application.isPlaying && VirtualGO)
		{
			Gizmos.matrix = transform.localToWorldMatrix;
			VirtualGO.OnGizmosSelected(this);
		}
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying && VirtualGO && VirtualGO.WorldModel)
		{
			Gizmos.color = VirtualGO.GizmosColor;
			Gizmos.matrix = transform.localToWorldMatrix;
			foreach (var item in VirtualGO.WorldModel.GetComponentsInChildren<MeshFilter>())
				Gizmos.DrawWireMesh(item.sharedMesh, item.transform.position, item.transform.rotation, item.transform.lossyScale);
			foreach (var item in VirtualGO.WorldModel.GetComponentsInChildren<SkinnedMeshRenderer>())
				Gizmos.DrawWireMesh(item.sharedMesh, item.transform.position, item.transform.rotation, item.transform.lossyScale);
		}
	}
}
