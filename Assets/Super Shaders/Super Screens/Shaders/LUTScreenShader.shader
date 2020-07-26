Shader "Hidden/LUTScreenShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Base (RGB)", 2D) = "white" {}

		_LUT("LUT Texture", 2D) = "white" {}
		_Offset("Offset", Float) = 0
		_Strength("Strength", Float) = 1
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

			sampler2D _LUT;
			half _Offset;
			half _Strength;

			fixed4 frag(v2f_img i) : COLOR
			{
				fixed4 c = tex2D(_MainTex, i.uv);

				half b = ceil(c.b);
				half by = floor(b);
				half bx = floor(b - by);

				fixed2 uv = (c.rg + fixed2(bx, by)) * _Offset;

				fixed3 lc = tex2D(_LUT, uv).rgb;

				c.rgb = lerp(c.rgb, lc, _Strength);

				return c;
			}

		ENDCG

		}
	}
}
