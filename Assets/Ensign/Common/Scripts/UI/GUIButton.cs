using Ensign.Unity.UI;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ensign
{
#if UNITY_EDITOR
	[ExecuteInEditMode]
#endif
	[RequireComponent(typeof(Button))]
	public class GUIButton : EnsignButton<GUIButton>
	{
		protected override void Awake()
		{
			base.Awake();
#if UNITY_EDITOR
			lblCaption = GetComponentInChildren<Text>();
#endif
		}

#if UNITY_EDITOR
		[ContextMenu("Remove GUIButton", false, 0)]
		void RemoveGUIButton()
		{
            GUIButton guiButton = Selection.activeGameObject.GetComponent<GUIButton>();
            Button uguiBtn 		= Selection.activeGameObject.GetComponent<Button>();
			if(guiButton 	!= null)
            	GameObject.DestroyImmediate(guiButton);
			if(uguiBtn 		!= null)	
				GameObject.DestroyImmediate(uguiBtn);
        }
#endif
	}
}