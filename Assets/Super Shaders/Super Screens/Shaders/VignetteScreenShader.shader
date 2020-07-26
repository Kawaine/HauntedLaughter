Shader "Hidden/VignetteScreenShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Base (RGB)", 2D) = "white" {}

		_Radius("Radius", Range(0.0, 1.0)) = 0.9
		_Softness("Softness", Range(0.0, 1.0)) = 0.4
		_Offset("Offset", Vector) = (0.5, 0.5, 0, 0)
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

			half _Radius;
			half _Softness;
			half2 _Offset;

			float4 frag(v2f_img i) : COLOR 
			{
				fixed4 c = tex2D(_MainTex, i.uv);

				fixed d = distance(i.uv, _Offset);

				fixed v = smoothstep(_Radius, _Radius - _Softness, d);

				c = saturate(c * v);

				return c;
			}

		ENDCG

		}
	}
}
