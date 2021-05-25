using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosExtra {

	public static void DrawLabel(Vector3 pos, string label, Color color, int fontSize = 12){
		#if UNITY_EDITOR
		Vector3 dir = pos - Camera.current.transform.position;
		float dot = Vector3.Dot(Camera.current.transform.forward, dir.normalized);

		if(dot > 0){
			GUIStyle style = new GUIStyle ();
			style.normal.textColor = color;
			style.fontSize = fontSize;
			style.alignment = TextAnchor.MiddleCenter;
      style.fixedWidth = 10;
			UnityEditor.Handles.Label (pos, label, style);
		}
		#endif
	}
}
