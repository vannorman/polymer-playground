Shader "Toon/Lit Outline Transparent" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
		_RimAmount  ("Rim Lighting Value", range (0,1)) = 0.1
		_SpecBrightness ("Specular Brightness", range(0,1)) = 0
		_SpecPower ("Specular Exponent", range(0,8)) = 0		
	}

	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200
		
		ZWrite On
		ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha	
		UsePass "Toon/LightedTransparent/FORWARD"
		UsePass "Toon/Basic Outline/OUTLINE"
	} 
	
	Fallback "Toon/LightedTransparent"
}
