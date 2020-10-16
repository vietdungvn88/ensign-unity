using UnityEngine;

namespace Ensign.Unity.Effect
{
	[RequireComponent(typeof(Camera))]
	public class EffectCrossFade : MonoBehaviour
	{
		private readonly int _BlendAmount = Shader.PropertyToID("_BlendAmount");

		[SerializeField]
		private Material material;

		[SerializeField]
        [Range(0, 1f)]
        private float Value = 0;

		void OnPostRender()
		{
            material.SetFloat(_BlendAmount, Value);
        }

        void OnRenderImage (RenderTexture source, RenderTexture destination) 
		{
			Graphics.Blit(source, destination, material);
        }

		public void Fade(float fromValue, float toValue, float timeAction)
        {
            ThirdParty.Tween.LeanTween.value(gameObject, fromValue, toValue, timeAction).setOnUpdate((value) =>
            {
                this.Value = value;
            }).setEase(ThirdParty.Tween.LeanTweenType.easeInQuint);
        }

		public void SetValue(float value)
		{
            this.Value = value;
        }
	}
}
