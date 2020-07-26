Shader "Hidden/ScanlineScreenShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		[Header(Keep color to greyscale for a transparent effect.)]
		_Color("Color", Color) = (0, 0, 0, 1)
		_Size("Size", Range(1, 10)) = 1
		_Delta("Delta", Range(0, 1)) = 0
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

			fixed4 _Color; 
			half _Size;
			half _Delta;

			fixed4 frag(v2f_img i) : COLOR
			{
				fixed4 c = tex2D(_MainTex, i.uv);

				half l = i.uv.x / (i.uv.x / i.uv.y) + _Delta;

				if ((uint)(l * _ScreenParams.y / floor(_Size)) % 2 == 0)
				{
					return c;
				}

				_Color.rgb *= c.rgb;

				return _Color;
			}

		ENDCG

		}
	}
}