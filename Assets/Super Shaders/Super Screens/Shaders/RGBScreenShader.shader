Shader "Hidden/RGBScreenShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Base (RGB)", 2D) = "white" {}

		_Red("Red", Range(0, 1)) = 1
		_Green("Green", Range(0, 1)) = 1
		_Blue("Blue", Range(0, 1)) = 1
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

			half _Red;
			half _Green;
			half _Blue;

			float4 frag(v2f_img i) : COLOR 
			{
				half4 c = tex2D(_MainTex, i.uv);

				c.r *= _Red;
				c.g *= _Green;
				c.b *= _Blue;

				return c;
			}

		ENDCG

		}
	}
}
