Shader "Super/BarrelShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		_HorizontalShift("Horizontal Shift", Float) = 1
		_VerticalShift("Vertical Shift", Float) = 1

		_Distortion("Distortion", Range(-2.5, 2.5)) = -0.7
		_View("View", Range(-2.5, 2.5)) = 1

		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM

			#pragma vertex SpriteVert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA

			#include "UnitySprites.cginc"

			half _HorizontalShift;
			half _VerticalShift;

			half _Distortion;
			half _View;

			float4 frag(v2f IN) : COLOR
			{
				fixed2 uv = IN.texcoord.xy - fixed2(0.5, 0.5);

				half r = (uv.x * uv.x * _HorizontalShift) + (uv.y * uv.y * _VerticalShift);

				half d = 1 + r * (_Distortion + 0.5 * sqrt(r));

				fixed4 c = SampleSpriteTexture(uv * d * _View + 0.5) * IN.color;

				c.rgb *= c.a;

				return c;
			}

			ENDCG
		}
	}

	Fallback "Sprites/Default"
}