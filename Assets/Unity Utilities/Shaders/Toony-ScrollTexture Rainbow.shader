Shader "Toon/Scroll Rainbow" {
    Properties {
        _MainTex ("Base (RGBA)", 2D) = "white" {}
        colorMap ("Base (RGB)", 2D) = "white" {}
//        _Blend ("Blend", Range (0, 1) ) = 0.0
        alpha ("alpha", Float) = 0.5 
        direction ("direction", Float) = 1
        speed ("speed", Float) = 1
     
//     	_OverlayTexture ("Texture 2 with alpha", 2D) = "" {}

    }
    SubShader {
    	Tags{ "IgnoreProjector"="True" "RenderType"="Transparent" "Queue"="Transparent-1000" }
    	Blend SrcAlpha OneMinusSrcAlpha
        Pass {
        	ZWrite Off
            CGPROGRAM
//            #pragma exclude_renderers gles
            #pragma vertex vert_img
//            #pragma fragment frag
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Two textures, one for the base texture, another for the color map
            sampler2D _MainTex, colorMap;
            float alpha;
            int direction;
            int speed;
            float4 frag(v2f_img i) : COLOR 
            {

            	
             	float _speed = 3;
//                i.uv.y += cos(_Time)*_speed; // Now scroll it too!
//                i.uv.x += sin(_Time)*_speed;
                // First get the color of the base texture for this fragment
                // i.uv is the unity provided texture coordinate
                half4 grayscale = tex2D (_MainTex, i.uv);

                // Use one of the components of the grayscale color to calculate 
                // an index into the colorMap. Adding time will shift the index 
                // to give the desired animation.
                // Textures wrap automatically so we don't need to worry about 
                // keeping the index between 0 and 1.
                // _Time is a unity provided variable that is useful for animating things
                half index = grayscale.r + _Time[0]*10*direction*speed; 
//                half index = grayscale.r += sin(_Time*10)[0]*1; 

                // Get the mapped color from the colorMap texture. The first component of
                // the texture coordinate is the index that we just calculated. The second 
                // component is always 0 since colorMap is a 1-dimensional texture.
                half4 ret  = tex2D (colorMap, float2(index, 0));


                ret.a = alpha;
				//LOOK! _MainTex_ST.xy is tiling and _MainTex_ST.zw is offset
//					i.uv =  i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
			     return ret;
		        
//				float4 texcol = tex2D (_MainTex, i.uv); //base texture
//
//				ret = tex2D(_MainTex, i.uv);
//				ret.rgb += _Color;
//                return ret; //tex2D(_MainTex, i.uv);
            }


            ENDCG
        }
//        Pass {
//          SetTexture[_MainTex]
//             SetTexture[_OverlayTexture] {
//                 ConstantColor (0,0,0, [_Blend]) 
//                 combine texture Lerp(constant) previous
//             }
//         }
    } 
    FallBack "Diffuse"
}