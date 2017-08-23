using UnityEngine;
using System.Collections;

public static class CameraExtensions
{
	//******Orthographic Camera Only******//
	public static Vector3 BoundsMin(this Camera camera)
	{
		return camera.transform.position - camera.Extents();
	}

	public static Vector3 BoundsMax(this Camera camera)
	{
		return camera.transform.position + camera.Extents();
	}

	public static Vector3 Extents(this Camera camera)
	{
		if (camera.orthographic)
			return new Vector3(camera.orthographicSize * Screen.width/Screen.height, camera.orthographicSize, 0f);
		else
		{
			Debug.LogError("Camera is not orthographic!", camera);
			return new Vector3();
		}
	}
	//*****End of Orthographic Only*****//
}

