Shader "Custom/Scroll Texture" {
	Properties {
	    _MainTex ("Texture", 2D) = "black" {}
	    _Color ("Main Color", Color) = (1,1,1)
	    _speed ("Speed", Float) = 0.2
	}

	Category {
//	    Tags { "IgnoreProjector"="True" "RenderType"="Opaque" }
//	    Blend Srcalpha Zero

	    Cull Off Lighting Off ZWrite On Fog { Color (0,0,0) }

	    BindChannels {
	        Bind "Color", color
	        Bind "Vertex", vertex
	        Bind "TexCoord", texcoord
	    }

	    SubShader {
	        Pass {
	            CGPROGRAM
	            #pragma vertex vert_img
	            #pragma fragment frag

	            #include "UnityCG.cginc"

	            uniform sampler2D _MainTex;
	            uniform float4 _MainTex_ST; // needed for tiling and offset
	            float4 _Color;
	            float _speed = 3;

	          

//
//                 struct vertexInput{
//			         float4 vertex : POSITION; 
//			         float4 texcoord : TEXCOORD0;
//			     };
//
//	            struct fragmentInput {
//			         float4 pos : SV_POSITION;
//			         half2 uv : TEXCOORD0;
//			     };
//
//	             fragmentInput vert( vertexInput i ) {
//			         fragmentInput o;
//			         o.pos = mul( UNITY_MATRIX_MVP, i.vertex );
//			 
//					 //This is a standard defined function in Unity, 
//					 //Does exactly the same as the next line of code
//			         //o.uv = TRANSFORM_TEX( i.texcoord, _MainTex );
//			     
//					 //LOOK! _MainTex_ST.xy is tiling and _MainTex_ST.zw is offset
//			         o.uv =  i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
//			     
//			         return o;
//			     }
//			     half4 frag( fragmentInput i ) : COLOR {
//			         return half4( tex2D( _MainTex, i.uv ).rgb, 1.0);
//			     }

		       float4 frag(v2f_img i) : COLOR {


	                i.uv.y += _Time*_speed;

					//LOOK! _MainTex_ST.xy is tiling and _MainTex_ST.zw is offset
					i.uv =  i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
//					i.uv = UnityStereoScreenSpaceUVAdjust(i.uv.xy,_MainTex_ST);
				     
			        
					float4 texcol = tex2D (_MainTex, i.uv); //base texture

					fixed4 ret = tex2D(_MainTex, i.uv);
					ret.rgb += _Color;
	                return ret; //tex2D(_MainTex, i.uv);
	            }
	            ENDCG
	        }



//	        Pass {
//	            CGPROGRAM
//	            #pragma vertex vert_img
//	            #pragma fragment frag
//
//	            #include "UnityCG.cginc"
//
//	            uniform sampler2D _MainTex;
//	            float _speed = 3;
//
//	            float4 frag(v2f_img i) : COLOR {
//
//	                i.uv.x += sin(_Time*_speed);
////					i.uv.x += _speed;
//	                return tex2D(_MainTex, i.uv);
//	            }
//	            ENDCG
//	        }
	    }
		Fallback "Toon/Lit"
	}
}