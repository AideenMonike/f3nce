Shader "Hidden/CrtPostProcess"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            uniform float u_time;
			uniform float u_bend;
			uniform float u_scanline_size1;
			uniform float u_scanline_spd1;
			uniform float u_scanline_size2;
			uniform float u_scanline_spd2;
			uniform float u_scanline_No;
			uniform float u_vigSize;
			uniform float u_vigSmooth;
			uniform float u_vigEdgeRnd;
			uniform float u_noiseSize;
			uniform float u_noiseNo;
			uniform half2 u_redOff;
			uniform half2 u_greenOff;
			uniform half2 u_blueOff;

            half2 crt_coords(half2 uv, float bend)
            {
                // uv is currently in a range of [0, 1]. 
                // uv - 0.5 = [-0.5, 0.5]
                // uv * 2. = [-1, 1]
                uv -= 0.5;
                uv *= 2.;

                // Uses a quadratic function to manipulate resolution into a bend
                // This example should give us something like:
                // y = (x / 4)^2
                // x = (y / 4)^2
                uv.x *= 1. + pow(abs(uv.y) / bend, 2.);
                uv.y *= 1. + pow(abs(uv.x) / bend, 2.);

                // This then returns uv to range of [0, 1]
                uv /= 2.5;
                return uv + 0.5;
            }

            float vignette(half2 uv, float size, float smoothness, float edgeRounding)
            {
                // sets uv to [-.5, .5] and applies as multiple of size
                // this will allow the vignette to be manipulated more by size. bigger size, closer vignette is to the centre
                uv -= .5;
                uv *= size;

                // applies distance formula to vignette. on it's own, this will focus the vignette to the centre and not to the outside
                // hence why amount is subtracted from 1, effectively inversing the vignette
                // smoothstep is also applied to control the intensity of the black is
                float amount = sqrt(pow(abs(uv.x), edgeRounding) + pow(abs(uv.y), edgeRounding));
                amount = 1. - amount;
                return smoothstep(0, smoothness, amount);
            }

            float scanline(half2 uv, float lines, float speed)
            {
                // scanlines sort of function like waves right?
                // sin already has a range of [-1, 1], so that's nice for this
                // lines isn't actually amount of lines, more of just a multiplier of magnitude, but it's better than rateOfChange
                // time then lets the lines move, as the value changes
                // speed then controls the direction + magnitude of movement
                return sin(uv.y * lines + u_time * speed);
            }

            float random(half2 uv)
            {
                // this is just a pseudo-random function. slap a bunch of functions and numbers and some value pops out.
                // this is to generate value noise, which is sort of like a bunch of random values that transition nicely
                return frac(sin(dot(uv, half2(15.1511, 42.5225))) * 12341.51611 * sin(u_time * 0.03));
            }

            float noise(half2 uv)
            {
                // this is taken from Book of Shaders (https://thebookofshaders.com/11/), but it's essentially lerping a bunch of random vector values together
                half2 i = floor(uv);
				half2 f = frac(uv);

				float a = random(i);
				float b = random(i + half2(1., 0.));
				float c = random(i + half2(0, 1.));
				float d = random(i + half2(1., 1.));

				half2 u = smoothstep(0., 1., f);

				return lerp(a, b, u.x) + (c - a) * u.y * (1. - u.x) + (d - b) * u.x * u.y;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // this is sorta just the mainline of .shader
                // applies the noise, vignette, bend and scanlines
                // also adds chromatic abberations with the rgb offsets
                half2 crt_uv = crt_coords(i.uv, u_bend);
				fixed4 col;
				col.r = tex2D(_MainTex, crt_uv + u_redOff).r;
				col.g = tex2D(_MainTex, crt_uv + u_greenOff).g;
				col.b = tex2D(_MainTex, crt_uv + u_blueOff).b;
				col.a = tex2D(_MainTex, crt_uv).a;

				float s1 = scanline(i.uv, u_scanline_size1, u_scanline_spd1);
				float s2 = scanline(i.uv, u_scanline_size2, u_scanline_spd2);

				col = lerp(col, fixed(s1 + s2), u_scanline_No);

				return lerp(col, fixed(noise(i.uv * u_noiseSize)), u_noiseNo) * vignette(i.uv, u_vigSize, u_vigSmooth, u_vigEdgeRnd);
            }
            ENDCG
        }
    }
}
