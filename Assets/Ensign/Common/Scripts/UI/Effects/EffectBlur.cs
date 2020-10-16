using UnityEngine;

namespace Ensign.Unity.Effect
{
	[RequireComponent(typeof(Camera))]
	public class EffectBlur : EffectBlurBase
	{
		// [Range(0, 1f)]
        // public float Value = 0;

		// void OnPostRender()
		// {
        //     SetValue(Value);
        // }

		public void SetValue(float value)
		{
			this.interpolation = Mathf.Lerp(0f, 1f, value);
			this.downsample = System.Convert.ToInt32(Mathf.Lerp(0, 4, value));
		}

		public void Fade(float fromValue, float toValue, float timeAction)
        {
            ThirdParty.Tween.LeanTween.value(gameObject, fromValue, toValue, timeAction).setOnUpdate((value) =>
            {
                SetValue(value);
            }).setEase(ThirdParty.Tween.LeanTweenType.easeInQuint);
        }

		void OnRenderImage (RenderTexture source, RenderTexture destination) 
		{
			if (blurMaterial == null || UIMaterial == null) return;

			int tw = source.width >> downsample;
			int th = source.height >> downsample;

			var rt = RenderTexture.GetTemporary(tw, th, 0, source.format);

			Graphics.Blit(source, rt);

			if (renderMode == RenderMode.Screen)
			{
				Blur(rt, destination);
			}
			else if (renderMode == RenderMode.UI)
			{
				Blur(rt, rt);
				UIMaterial.SetTexture(Uniforms._BackgroundTexture, rt);
				Graphics.Blit(source, destination);
			}
			else if (renderMode == RenderMode.OnlyUI)
			{
				Blur(rt, rt);
				UIMaterial.SetTexture(Uniforms._BackgroundTexture, rt);
			}

			RenderTexture.ReleaseTemporary(rt);
		}
			
	}

}
