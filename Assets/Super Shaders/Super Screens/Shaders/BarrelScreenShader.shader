Shader "Hidden/BarrelScreenShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		_Distortion("Distortion", Range(-5, 5)) = -0.7
		_View("View", Range(-5, 5)) = 1

		_HorizontalShift("Horizontal Shift", Float) = 1
		_VerticalShift("Vertical Shift", Float) = 1
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

			half _Distortion;
			half _View;

			half _HorizontalShift;
			half _VerticalShift;

			fixed4 frag(v2f_img i) : COLOR
			{
				fixed2 uv = i.uv.xy - fixed2(0.5, 0.5);

				half r = (uv.x * uv.x * _HorizontalShift) + (uv.y * uv.y * _VerticalShift);

				half d = 1 + r * (_Distortion + 0.5 * sqrt(r));

				return tex2D(_MainTex, uv * d * _View + 0.5);
			}

		ENDCG

		}
	}
}