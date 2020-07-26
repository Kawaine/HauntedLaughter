Shader "Hidden/ChromaticScreenShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Base (RGB)", 2D) = "white" {}

		_Chromatic("Chromatic", Range(-1, 1)) = 0
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

			float _Chromatic;

			float4 frag(v2f_img i) : COLOR 
			{
				fixed4 c = tex2D(_MainTex, i.uv);

				half2 ca = half2(_Chromatic, _Chromatic);

				half r = tex2D(_MainTex, i.uv - ca).r;
				half g = tex2D(_MainTex, i.uv).g;
				half b = tex2D(_MainTex, i.uv + ca).b;

				return fixed4(r, g, b, c.a);
			}

		ENDCG

		}
	}
}
