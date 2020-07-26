Shader "Hidden/PaletteScreenShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		_ColorA("Color A", Color) = (0.2, 0.17, 0.31, 1)
		_ColorB("Color B", Color) = (0.27, 0.53, 0.56, 1)
		_ColorC("Color C", Color) = (0.58, 0.89, 0.27, 1)
		_ColorD("Color D", Color) = (0.89, 0.95, 0.89, 1)

		_Intensity("Intensity", Range(0, 1)) = 1
		_Threshold("Threshold", Range(0, 1)) = 0.15
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;

			fixed4 _ColorA;
			fixed4 _ColorB;
			fixed4 _ColorC;
			fixed4 _ColorD;

			half _Intensity;
			float _Threshold;

			fixed4 frag(v2f_img i) : COLOR
			{
				fixed4 c = tex2D(_MainTex, i.uv);

				fixed ct = c.rgb;

				fixed cat = _ColorA.rgb;
				fixed cbt = _ColorB.rgb;
				fixed cct = _ColorC.rgb;
				fixed cdt = _ColorD.rgb;

				c.rgb = lerp(c.rgb, (ct > cat - _Threshold && ct < cat + _Threshold) ? _ColorA.rgb : c.rgb, _Intensity);
				c.rgb = lerp(c.rgb, (ct > cbt - _Threshold && ct < cbt + _Threshold) ? _ColorB.rgb : c.rgb, _Intensity);
				c.rgb = lerp(c.rgb, (ct > cct - _Threshold && ct < cct + _Threshold) ? _ColorC.rgb : c.rgb, _Intensity);
				c.rgb = lerp(c.rgb, (ct > cdt - _Threshold && ct < cdt + _Threshold) ? _ColorD.rgb : c.rgb, _Intensity);

				return c;
			}

		ENDCG

		}
	}
}