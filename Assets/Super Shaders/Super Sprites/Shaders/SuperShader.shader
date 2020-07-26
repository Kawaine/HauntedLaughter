Shader "Super/SuperShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}

		[Header(Texture Properties)]
		_Color("Tint", Color) = (1, 1, 1, 1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		[Header(Color Effect Properties)]
		[Toggle(ENABLE_NEGATIVE)] _EnableNegative("Negative", Int) = 0

		[Space]
		[Toggle(ENABLE_GREYSCALE)] _EnableGreyscale("Enable Greyscale", Int) = 0
		_Greyscale("Greyscale", Range(0, 1)) = 0

		[Space]
		[Toggle(ENABLE_SEPIA)] _EnableSepia("Enable Sepia", Int) = 0
		_Sepia("Sepia", Range(0, 1)) = 0

		[Space]
		[Toggle(ENABLE_FADE)] _EnableFade("Enable Fade", Int) = 0
		_Fade("Fade", Range(0, 1)) = 0

		[Space]
		[Toggle(ENABLE_FILL)] _EnableFill("Enable Fill", Int) = 0
		_FillColor("Fill Color", Color) = (1, 1, 1, 1)

		[Space]
		[Toggle(ENABLE_RGB)] _EnableRGB("Enable RGB", Int) = 0
		_Red("Red", Range(0, 1)) = 1
		_Green("Green", Range(0, 1)) = 1
		_Blue("Blue", Range(0, 1)) = 1

		[Header(HSL Properties)]
		[Toggle(ENABLE_HSL)] _EnableHSL("Enable HSL", Int) = 0
		_Hue("Hue", Range(-1, 1)) = 0
		_Saturation("Saturation", Range(-1, 1)) = 0
		_Light("Light", Range(-1, 1)) = 0

		[Header(Color Swap Properties)]
		[Toggle(ENABLE_COLOR_SWAP)] _EnableColorSwap("Enable Color Swap", Int) = 0
		_SourceA("Source A", Color) = (0, 0, 1, 1)
		_TargetA("Target A", Color) = (1, 0, 0, 1)
		_SourceB("Source B", Color) = (0, 0, 1, 1)
		_TargetB("Target B", Color) = (1, 0, 0, 1)
		_SourceC("Source C", Color) = (0, 0, 1, 1)
		_TargetC("Target C", Color) = (1, 0, 0, 1)

		[Header(Outline Properties)]
		[Toggle(ENABLE_OUTLINE)] _EnableOutline("Enable Outline", Int) = 0
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
		_OutlineSize("Outline Size", Int) = 1

		[Header(Shift Properties)]
		[Toggle(ENABLE_SHIFT)] _EnableShift("Enable Shift", Int) = 0
		_HorizontalShift("Horizontal Shift", Float) = 0
		_VerticalShift("Vertical Shift", Float) = 0

		[Header(Blur Properties)]
		[Toggle(ENABLE_BLUR)] _EnableBlur("Enable Blur", Int) = 0
		_Blur("Blur", Range(0, 60)) = 15
		_Focus("Focus", Range(150, 900)) = 350

		[Header(Chromatic Properties)]
		[Toggle(ENABLE_CHROMATIC)] _EnableChromatic("Enable Chromatic", Int) = 0
		_HorizontalChromaticOffset("Horizontal Offset", Range(-1, 1)) = 0
		_VerticalChromaticOffset("Vertical Offset", Range(-1, 1)) = 0
		_ChromaticStrength("Strength", Range(-1, 1)) = 0.15
		_ChromaticOffset("Offset", Range(-1, 1)) = 0.25

		[Header(Pixelate Properties)]
		[Toggle(ENABLE_PIXELATE)] _EnablePixelate("Enable Pixelate", Int) = 0
		_PixelSize("Pixel Size", Float) = 0.001

		[Header(Shadow Properties)]
		[Toggle(ENABLE_SHADOW)] _EnableShadow("Enable Shadow", Int) = 0
		_ShadowColor("Shadow Color", Color) = (1, 1, 1, 1)
		_ShadowOffset("Shadow Offset", Vector) = (0, 0, 0, 0)

		[Header(Dissolve Properties)]
		[Toggle(ENABLE_DISSOLVE)] _EnableDissolve("Enable Dissolve", Int) = 0
		_DissolveTexture("Dissolve Texture", 2D) = "white" {}
		_EmissionColor("Emission Color", color) = (1,1,1,1)
		_DissolveAmount("Dissolve Amount", Range(0,1)) = 0
		_EmissionThickness("Emission Thickness", Range(0, 1)) = 0.15
		_EmissionThreshold("Emission Threshold", Range(0, 1)) = 0.1

		[Header(Distort Properties)]
		[Toggle(ENABLE_DISTORT)] _EnableDistort("Enable Distort", Int) = 0
		_DistortTex("Distort Texture", 2D) = "bump" {}
		_HorizontalDistort("Horizontal Distort", Range(-0.1, 0.1)) = 0.1
		_VerticalDistort("Vertical Distort", Range(-0.1, 0.1)) = 0.1
		_HorizontalScale("Horizontal Scale", Float) = 1
		_VerticalScale("Vertical Scale", Float) = 1

		[Header(Dither Properties)]
		[Toggle(ENABLE_DITHER)] _EnableDither("Enable Dither", Int) = 0
		_DitherTex("Dither Texture", 2D) = "white" {}
		_DitherColorA("Dither Color A", Color) = (1, 1, 1, 1)
		_DitherColorB("Dither Color B", Color) = (1, 1, 1, 1)
		_DitherScale("Dither Scale", Range(0, 0.1)) = 0.001
		_DitherStrength("Dither Strength", Range(0, 1)) = 1

		[Header(Stencil Properties)]
		[Toggle(ENABLE_STENCIL)] _EnableStencil("Enable Stencil", Int) = 0
		_Stencil("Stencil", Float) = 5
		[Enum(UnityEngine.Rendering.CompareFunction)]
		_Comparison("Comparison", Float) = 8
		[Enum(UnityEngine.Rendering.StencilOp)]
		_Operation("Operation", Float) = 2
		_WriteMask("Write Mask", Range(0, 255)) = 255
		_ReadMask("Read Mask", Range(0, 255)) = 255
		_AlphaCutoff("Alpha Cutoff", Range(0.01, 1.0)) = 0.01

		[Space]
		[HideInInspector] _RendererColor("RendererColor", Color) = (1, 1, 1, 1)
		[HideInInspector] _Flip("Flip", Vector) = (1, 1, 1, 1)
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
				Stencil
				{
					Ref[_Stencil]
					Comp[_Comparison]
					Pass[_Operation]
					ReadMask[_ReadMask]
					WriteMask[_WriteMask]
				}

				CGPROGRAM

				#pragma vertex SpriteVert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile _ ETC1_EXTERNAL_ALPHA

				#pragma shader_feature ENABLE_NEGATIVE
				#pragma shader_feature ENABLE_GREYSCALE
				#pragma shader_feature ENABLE_SEPIA
				#pragma shader_feature ENABLE_FADE
				#pragma shader_feature ENABLE_FILL
				#pragma shader_feature ENABLE_RGB
				#pragma shader_feature ENABLE_SHIFT
				#pragma shader_feature ENABLE_BLUR
				#pragma shader_feature ENABLE_CHROMATIC
				#pragma shader_feature ENABLE_HSL
				#pragma shader_feature ENABLE_PIXELATE
				#pragma shader_feature ENABLE_SHADOW
				#pragma shader_feature ENABLE_OUTLINE
				#pragma shader_feature ENABLE_COLOR_SWAP
				#pragma shader_feature ENABLE_DISSOLVE
				#pragma shader_feature ENABLE_DISTORT
				#pragma shader_feature ENABLE_DITHER
				#pragma shader_feature ENABLE_STENCIL

				#include "UnitySprites.cginc"
				#include "UnityShaderVariables.cginc"

				fixed4 _MainTex_TexelSize;

				half _Greyscale;
				half _Sepia;
				half _Fade;
				fixed4 _FillColor; 
				half _Red;
				half _Green;
				half _Blue;

				half _HorizontalShift;
				half _VerticalShift;

				float _Blur;
				float _Focus;

				half _HorizontalChromaticOffset;
				half _VerticalChromaticOffset;
				half _ChromaticStrength;
				half _ChromaticOffset;

				half _Hue;
				half _Saturation;
				half _Light;

				float _PixelSize;

				fixed4 _ShadowOffset;
				fixed4 _ShadowColor;

				fixed4 _OutlineColor;
				int _OutlineSize;

				fixed4 _SourceA;
				fixed4 _TargetA;
				fixed4 _SourceB;
				fixed4 _TargetB;
				fixed4 _SourceC;
				fixed4 _TargetC;

				sampler2D _DissolveTexture;
				fixed4 _EmissionColor;
				half _DissolveAmount;
				half _EmissionThickness;
				half _EmissionThreshold;

				sampler2D _DistortTex;
				float _HorizontalDistort;
				float _VerticalDistort;
				float _HorizontalScale;
				float _VerticalScale;

				sampler2D _DitherTex;
				fixed4 _DitherColorA;
				fixed4 _DitherColorB;
				half _DitherScale;
				half _DitherStrength;

				float _UsingStencil;
				fixed _AlphaCutoff;

#if ENABLE_HSL
				// color conversions by Ian Taylor
				float3 RGBtoHCV(in float3 RGB)
				{
					// Based on work by Sam Hocevar and Emil Persson
					float4 P = (RGB.g < RGB.b) ? float4(RGB.bg, -1.0, 2.0 / 3.0) : float4(RGB.gb, 0.0, -1.0 / 3.0);
					float4 Q = (RGB.r < P.x) ? float4(P.xyw, RGB.r) : float4(RGB.r, P.yzx);
					float C = Q.x - min(Q.w, Q.y);
					float H = abs((Q.w - Q.y) / (6 * C + 1e-10) + Q.z);
					return float3(H, C, Q.x);
				}

				float3 HUEtoRGB(in float H)
				{
					float R = abs(H * 6 - 3) - 1;
					float G = 2 - abs(H * 6 - 2);
					float B = 2 - abs(H * 6 - 4);
					return saturate(float3(R, G, B));
				}

				float3 HSLtoRGB(in float3 HSL)
				{
					float3 RGB = HUEtoRGB(HSL.x);
					float C = (1 - abs(2 * HSL.z - 1)) * HSL.y;
					return (RGB - 0.5) * C + HSL.z;
				}

				float3 RGBtoHSL(in float3 RGB)
				{
					float3 HCV = RGBtoHCV(RGB);
					float L = HCV.z - HCV.y * 0.5;
					float S = HCV.y / (1 - abs(L * 2 - 1) + 1e-10);
					return float3(HCV.x, S, L);
				}
#endif

				fixed4 frag(v2f IN) : SV_Target
				{
					float2 uv = IN.texcoord.xy;

#if ENABLE_SHIFT
					uv += float2(_HorizontalShift, _VerticalShift);
#endif

					fixed4 c = SampleSpriteTexture(uv) * IN.color;

#if ENABLE_DISTORT
					float2 s = float2(_HorizontalScale, _VerticalScale);
					float2 d = float2(_HorizontalDistort, _VerticalDistort);

					uv += (d * tex2D(_DistortTex, s * IN.texcoord));
#endif

#if ENABLE_PIXELATE
					float2 size = float2(_PixelSize, _PixelSize) * 0.01;

					uv /= size;
					uv = round(uv);
					uv *= size;
#endif

					c = SampleSpriteTexture(uv) * IN.color;

#if ENABLE_SHADOW
					fixed4 sh = SampleSpriteTexture(uv + _ShadowOffset) * IN.color;
					sh.rgb = _ShadowColor;
					sh.a *= _ShadowColor.a;
#endif

#if ENABLE_CHROMATIC
					fixed2 cd = fixed2(_HorizontalChromaticOffset, _VerticalChromaticOffset) * _ChromaticStrength;
					fixed4 cr = SampleSpriteTexture(uv + cd * _ChromaticOffset) * IN.color;
					fixed4 cg = SampleSpriteTexture(uv + cd * _ChromaticOffset * 2) * IN.color;
					fixed4 cb = SampleSpriteTexture(uv + cd * _ChromaticOffset * 3) * IN.color;
					c.a = (cr.a + cg.a + cb.a) / 3;
					c.r = cr.r;
					c.g = cg.g;
					c.b = cb.b;
#endif

#if ENABLE_FILL
					c.rgb = _FillColor.rgb;
#endif

#if ENABLE_BLUR
					float4 adj = float4(0, 0, 0, 0);

					half pixels = (_Blur / _Focus * 0.2) * 0.5;

					adj += tex2D(_MainTex, float2(uv.x - 3 * pixels, uv.y - 3 * pixels)) * 0.1;
					adj += tex2D(_MainTex, float2(uv.x + 3 * pixels, uv.y + 3 * pixels)) * 0.1;

					adj += tex2D(_MainTex, float2(uv.x - 2 * pixels, uv.y - 2 * pixels)) * 0.1;
					adj += tex2D(_MainTex, float2(uv.x + 2 * pixels, uv.y + 2 * pixels)) * 0.1;

					adj += tex2D(_MainTex, float2(uv.x - 1 * pixels, uv.y - 1 * pixels)) * 0.2;
					adj += tex2D(_MainTex, float2(uv.x + 1 * pixels, uv.y + 1 * pixels)) * 0.2;

					adj += tex2D(_MainTex, float2(uv.x, uv.y)) * 0.2;

					adj.a *= IN.color.a;

					c = adj;
#endif

#if ENABLE_COLOR_SWAP
					// color swap a
					if (c.r >= _SourceA.r - 0.005 && c.r <= _SourceA.r + 0.005 && c.g >= _SourceA.g - 0.005 &&
						c.g <= _SourceA.g + 0.005 && c.b >= _SourceA.b - 0.005 && c.b <= _SourceA.b + 0.005)
					{
						c.rgb = _TargetA.rgb;
					}

					// color swap b
					if (c.r >= _SourceB.r - 0.005 && c.r <= _SourceB.r + 0.005 && c.g >= _SourceB.g - 0.005 &&
						c.g <= _SourceB.g + 0.005 && c.b >= _SourceB.b - 0.005 && c.b <= _SourceB.b + 0.005)
					{
						c.rgb = _TargetB.rgb;
					}

					// color swap c
					if (c.r >= _SourceC.r - 0.005 && c.r <= _SourceC.r + 0.005 && c.g >= _SourceC.g - 0.005 &&
						c.g <= _SourceC.g + 0.005 && c.b >= _SourceC.b - 0.005 && c.b <= _SourceC.b + 0.005)
					{
						c.rgb = _TargetC.rgb;
					}
#endif

#if ENABLE_HSL
					float3 hsl = RGBtoHSL(c.rgb);
					float mx = step(0, hsl.r) * step(hsl.r, 1);

					c.rgb = HSLtoRGB((hsl + float3(_Hue, _Saturation, _Light)) * mx);
#endif

#if ENABLE_OUTLINE
					if (c.a != 0)
					{
						float totalAlpha = 1;

						[unroll(16)]
						for (int i = 1; i < _OutlineSize + 1; i++)
						{
							fixed4 pixelUp = tex2D(_MainTex, uv + fixed2(0, i * _MainTex_TexelSize.y));
							fixed4 pixelDown = tex2D(_MainTex, uv - fixed2(0, i *  _MainTex_TexelSize.y));
							fixed4 pixelRight = tex2D(_MainTex, uv + fixed2(i * _MainTex_TexelSize.x, 0));
							fixed4 pixelLeft = tex2D(_MainTex, uv - fixed2(i * _MainTex_TexelSize.x, 0));

							totalAlpha = totalAlpha * pixelUp.a * pixelDown.a * pixelRight.a * pixelLeft.a;
						}

						if (totalAlpha == 0)
						{
							c.rgba = fixed4(1, 1, 1, 1) * _OutlineColor;
						}
					}
#endif

#if ENABLE_DITHER
					half v = tex2D(_DitherTex, round(uv / _DitherScale) * _DitherScale).r;

					c *= lerp(_DitherColorA, _DitherColorB, max(1 - _DitherStrength, step(v, c.r)));
#endif

#if ENABLE_STENCIL
					clip(c.a - _AlphaCutoff);
#endif

#if ENABLE_NEGATIVE
					c.rgb = (1 - c);
#endif

#if ENABLE_GREYSCALE
					c.rgb = lerp(c.rgb, dot(c.rgb, float3(0.3, 0.59, 0.11)), _Greyscale);
#endif

#if ENABLE_SEPIA
					fixed3 sep;
					sep.r = dot(c.rgb, half3(0.39, 0.77, 0.19));
					sep.g = dot(c.rgb, half3(0.35, 0.69, 0.17));
					sep.b = dot(c.rgb, half3(0.27, 0.53, 0.13));
					c.rgb = lerp(c.rgb, sep, _Sepia);
#endif

#if ENABLE_DISSOLVE
					fixed4 b = fixed4(0, 0, 0, 0);

					half dv = tex2D(_DissolveTexture, uv).r;

					if (dv < _DissolveAmount + _EmissionThickness)
					{
						half a = _DissolveAmount / _EmissionThreshold;

						b = fixed4(_EmissionColor.r, _EmissionColor.g, _EmissionColor.b, c.a * a);
					}

					if (dv <= _DissolveAmount)
					{
						b = c;
					}

					b.rgb *= _EmissionColor.a;

					c = b;
#endif

#if ENABLE_FADE
					c.a -= max(_Fade, 0);
#endif

#if ENABLE_RGB
					c.r *= _Red;
					c.g *= _Green;
					c.b *= _Blue;
#endif

#if ENABLE_SHADOW
					fixed4 o;

					o.rgb = (sh.rgb * (1 - c.a)) + (c.rgb * c.a);
					o.a = min(sh.a + c.a, 1);

					//c.rgb *= c.a;

					return o;
#else
					return c;
#endif
				}

				ENDCG

			}
		}

	Fallback "Sprites/Default"
}
