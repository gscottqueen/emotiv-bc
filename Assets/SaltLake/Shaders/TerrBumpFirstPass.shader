Shader "Custom/SaltLake/Bumped Specular" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.03, 1)) = 0.078125

	[HideInInspector] _Splat3 ("Layer 3 (A)", 2D) = "white" {}
	[HideInInspector] _Splat2 ("Layer 2 (B)", 2D) = "white" {}
	[HideInInspector] _Splat1 ("Layer 1 (G)", 2D) = "white" {}
	[HideInInspector] _Splat0 ("Layer 0 (R)", 2D) = "white" {}
	[HideInInspector] _Normal3 ("Normal 3 (A)", 2D) = "bump" {}
	[HideInInspector] _Normal2 ("Normal 2 (B)", 2D) = "bump" {}
	[HideInInspector] _Normal1 ("Normal 1 (G)", 2D) = "bump" {}
	[HideInInspector] _Normal0 ("Normal 0 (R)", 2D) = "bump" {}


	_Mask ("Mask (RGB)", 2D) = "white" {}
	_Parallax ("Height", Range (0.005, 0.08)) = 0.02
	_ColorTex ("ColorMap (RGB)", 2D) = "white" {}	
	_NormalMap ("NormalMap (RGB)", 2D) = "white" {}

	_SandColorTex ("SandColorMap (RGB)", 2D) = "white" {}	
	_HeightSplatAll ("heights (r.g.b.a)", 2D) = "black" {}
	
	_Cube ("Reflection Cubemap", Cube) = "" {}
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
}
	
SubShader { 
	Tags {
		"SplatCount" = "4"
		"Queue" = "Geometry-100"
		"RenderType" = "Opaque"
		}
CGPROGRAM
#pragma surface surf BlinnPhong vertex:vert
#pragma target 3.0

void vert (inout appdata_full v)
{
	v.tangent.xyz = cross(v.normal, float3(0,0,1));
	v.tangent.w = -1;
}

struct Input {
	float2 uv_Splat0 : TEXCOORD1;
	float2 uv_Splat1 : TEXCOORD2;
	float2 uv_Splat2 : TEXCOORD3;
	float2 uv_Splat3 : TEXCOORD4;
	float2 uv_Mask : TEXCOORD0;
	float2 uv_SandColorTex;
	float3 worldPos;
	float3 worldRefl;
	float3 viewDir;
	INTERNAL_DATA
};

sampler2D _Mask;
sampler2D _ColorTex;
sampler2D _SandColorTex;
sampler2D _HeightSplatAll;
sampler2D _NormalMap;
fixed4 _ReflectColor;
float _Parallax;

sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
sampler2D _Normal0,_Normal1,_Normal2,_Normal3;
half _Shininess;

fixed4 _Color;
float3 viewDir;

samplerCUBE _Cube;
	
void surf (Input IN, inout SurfaceOutput o) {
	
	half4 h = tex2D (_HeightSplatAll, IN.uv_Splat0).r;
	float2 offset = ParallaxOffset (h, _Parallax, IN.viewDir);
	IN.uv_Splat0 += offset;

	half4 h2 = tex2D (_HeightSplatAll, IN.uv_Splat3).a;
	float2 offset3 = ParallaxOffset (h2, _Parallax, IN.viewDir);
	IN.uv_Splat3 += offset3;
	
	half4 MaskTex = tex2D (_Mask, IN.uv_Mask);
	half4 NormalTex = tex2D (_NormalMap, IN.uv_Mask);
	
	half4 Detail0 = tex2D(_Splat0,IN.uv_Splat0) * tex2D(_SandColorTex, IN.worldPos.xy/100);
	half4 Detail1 = tex2D(_Splat1,IN.uv_Splat1) * tex2D(_SandColorTex, IN.worldPos.xy/100);
	half4 Detail2 = tex2D(_Splat2,IN.uv_Splat2) * tex2D(_SandColorTex, IN.worldPos.xy/100);
	half4 Detail3 = tex2D(_Splat3,IN.uv_Splat3) * tex2D(_SandColorTex, IN.worldPos.xy/100);
	
	half4 HeightSplatTex1 = tex2D (_HeightSplatAll, IN.uv_Splat0).r;
	half4 HeightSplatTex2 = tex2D (_HeightSplatAll, IN.uv_Splat1).g;
	half4 HeightSplatTex3 = tex2D (_HeightSplatAll, IN.uv_Splat2).b;
	half4 HeightSplatTex4 = tex2D (_HeightSplatAll, IN.uv_Splat3).a; 
	
	float a0 = MaskTex.r;
	float a1 = MaskTex.g;
	float a2 = MaskTex.b;
	float a3 = MaskTex.a;
	
	float ma = (max(max(max(HeightSplatTex1.rgb + a0, HeightSplatTex2.rgb + a1), HeightSplatTex3.rgb + a2), HeightSplatTex4.rgb + a3)) - 0.1;

	float b0 = max(HeightSplatTex1.rgb + a0 - ma, 0);
	float b1 = max(HeightSplatTex2.rgb + a1 - ma, 0);
	float b2 = max(HeightSplatTex3.rgb + a2 - ma, 0);
	float b3 = max(HeightSplatTex4.rgb + a3 - ma, 0);
		    
	float4 texture0 = Detail0;
	float4 texture1 = Detail1;
	float4 texture2 = Detail2;
	float4 texture3 = Detail3;
	fixed4 tex = (texture0 * b0 + texture1 * b1 + texture2 * b2 + texture3 * b3) / (b0 + b1 + b2 + b3);

	texture0 = tex2D (_Normal0, IN.uv_Splat0) + NormalTex - 0.4;
	texture1 = tex2D (_Normal1, IN.uv_Splat1) + NormalTex - 0.4;
	texture2 = tex2D (_Normal2, IN.uv_Splat2) + NormalTex - 0.4;
	texture3 = tex2D (_Normal3, IN.uv_Splat3) + NormalTex - 0.4;
	float4 mixnormal = (texture0 * b0 + texture1 * b1 + texture2 * b2 + texture3 * b3) / (b0 + b1 + b2 + b3);
			
	o.Albedo = tex.rgb * _Color.rgb * tex2D(_ColorTex, IN.uv_Mask);

	o.Normal =  UnpackNormal(mixnormal);

	o.Gloss = tex.a * tex2D(_ColorTex, IN.uv_Mask);
	o.Specular = _Shininess;
	
	float3 worldRefl = WorldReflectionVector (IN, UnpackNormal(NormalTex));
	fixed4 Water_refl = texCUBE (_Cube, worldRefl) / 3;
	Water_refl *= MaskTex.a;
	Water_refl *= b3 * 5;
	Water_refl *= Detail3.a * 5;
	o.Emission = Water_refl.rgb * _ReflectColor.rgb;

}
ENDCG  
}

FallBack "Legacy Shaders/Reflective/Bumped Specular"
}
