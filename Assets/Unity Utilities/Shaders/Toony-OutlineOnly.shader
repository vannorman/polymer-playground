Shader "Toon/Outline Only" {
	Properties {
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (0, 0.03)) = .005
	}

	SubShader {
		Tags {
		"RenderType"="Opaque"
		"Queue"="Geometry"
		 }
		Cull Front
		UsePass "Toon/Basic Outline/OUTLINE"
	} 
	
	Fallback "Toon/Lighted"
}
