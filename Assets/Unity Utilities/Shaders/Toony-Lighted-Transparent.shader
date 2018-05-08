Shader "Toon/LightedTransparent" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
		_InvisibleDistance ("Invisible Distance", Range (5, 500)) = 120
	}

	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200
		
		ZWrite On
		ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha		
		
CGPROGRAM
#pragma surface surf ToonRamp alpha

sampler2D _Ramp;
float _SpecBrightness;
float _SpecPower;

// custom lighting function that uses a texture ramp based
// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass 
inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
{
	#ifndef USING_DIRECTIONAL_LIGHT
	lightDir = normalize(lightDir);
	#endif
	half3 h = normalize (lightDir + viewDir);
	float nh = max (0, dot (s.Normal, h));
	float spec = pow (nh, pow(2,_SpecPower));	
	
	half d = dot (s.Normal, lightDir)*0.5 + 0.5;
	half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	
	half4 c;
	
	float modSpec = clamp((spec - 0.8f) * 8,0,1);
	half3 specColor = half3(1,1,1) * modSpec * _SpecBrightness;
	
	c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2) + specColor;
	c.a = s.Alpha + modSpec * _SpecBrightness;
	return c;
}


sampler2D _MainTex;
float4 _Color;
float _RimAmount;

struct Input {
	float2 uv_MainTex : TEXCOORD0;
	float3 viewDir;
	float4 screenPos;

};

float _InvisibleDistance = 120;

void surf (Input IN, inout SurfaceOutput o) {
	if (IN.screenPos.z < _InvisibleDistance) {
		half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color ;
		half3 vd = IN.viewDir;
		half3 normy = o.Normal;
		half rim = 1.0 - saturate(dot (normalize(vd), normalize(normy)));
		half rim2 = min(max(rim * 3 - (3 * (1-_RimAmount)),0),1);
		//half rim = IN.uv_MainTex;
	//	o.Albedo
		
	//	o.color = c.rgb;
		o.Albedo = c.rgb * (1 + rim2 * 1.0f);
		o.Alpha = min(c.a,(_InvisibleDistance - IN.screenPos.z)/_InvisibleDistance) ;
//		o.Alpha = c.a;
	}
}

//void surf (Input IN, inout SurfaceOutput o) {
//	half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color ;
//	half3 vd = IN.viewDir;
//	half3 normy = o.Normal;
//	half rim = 1.0 - saturate(dot (normalize(vd), normalize(normy)));
//	half rim2 = min(max(rim * 3 - (3 * (1-_RimAmount)),0),1);
//	//half rim = IN.uv_MainTex;
////	o.Albedo
//	
////	o.color = c.rgb;
//	o.Albedo = c.rgb * (1 + rim2 * 1.0f);
//	o.Alpha = c.a;
//}
ENDCG

	} 

	Fallback "Diffuse"
}
