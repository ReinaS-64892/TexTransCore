#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace net.rs64.TexTransCore
{
    public struct ColorWOAlpha
    {
        public float R;
        public float G;
        public float B;
        public float[] ToArray() { return new float[] { R, G, B }; }
        public ColorWOAlpha(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
    /// <summary>
    /// ガンマ色空間の色を表現する
    /// </summary>
    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static Color Zero => new Color(0, 0, 0, 0);

        public float[] ToArray() { return new float[] { R, G, B, A }; }

        public override string ToString()
        {
            return base.ToString() + $"{R}-{G}-{B}-{A}";
        }
    }
    /// <summary>
    /// System.Numeric の Vector4 は W が先頭にあるためそれを回避するための存在
    /// </summary>
    public struct TTVector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;
        public TTVector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public override string ToString()
        {
            return $"{X}-{Y}-{Z}-{W}";
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }
        public override bool Equals(object obj)
        {
            var o = (TTVector4)obj;
            if (X != o.X) { return false; }
            if (Y != o.Y) { return false; }
            if (Z != o.Z) { return false; }
            if (W != o.W) { return false; }
            return true;
        }
    }


    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class Range : System.Attribute
    {
        public float Min;
        public float Max;
        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }

    public static class TTMath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NormalizeOf4Multiple(int v)
        {
            return v % 4 is not 0 ? v + (4 - v % 4) : v;
        }

        public static float LinearToGamma(float v)
        {
            if (v <= 0f) { return 0f; }
            if (v <= 0.0031308f) { return 12.92f * v; }
            if (v < 1.0f) { return 1.055f * (float)Math.Pow(Math.Abs(v), 0.4166667f) - 0.055f; }
            return (float)Math.Pow(Math.Abs(v), 0.45454545f);
        }
        public static float GammaToLinear(float v)
        {
            if (v <= 0.04045) { return 0.0f; }
            if (v < 1.0f) { return (float)Math.Pow((Math.Abs(v) + 0.055f) / 1.055f, 2.4f); }
            return (float)Math.Pow(Math.Abs(v), 2.2f);
        }

        public static int RoundToInt(float value)
        {
            return (int)Math.Round(value);
        }

        public static bool Approximately(float l, float r, float delta = float.Epsilon * 8f)
        {
            return Math.Abs(l - r) < delta;
        }

        public static float Saturate(float v)
        {
            return Math.Clamp(v, 0f, 1f);
        }
        public static float Trunc(float v)
        {
            return v - Frac(v);
        }
        public static float Frac(float v)
        {
            return v % 1f;
        }

        public static float Lerp(float l, float r, float v)
        {
            return l + (r - l) * v;
        }
        public static float NotNaN(float v, float replace = 0f)
        {
            return float.IsNaN(v) is false ? v : replace;
        }
        public static ColorWOAlpha GammaToLinear(ColorWOAlpha color)
        {
            color.R = GammaToLinear(color.R);
            color.G = GammaToLinear(color.G);
            color.B = GammaToLinear(color.B);
            return color;
        }

        public static ColorWOAlpha LinearToGamma(ColorWOAlpha color)
        {
            color.R = LinearToGamma(color.R);
            color.G = LinearToGamma(color.G);
            color.B = LinearToGamma(color.B);
            return color;
        }
    }

    public static class TTEnumerable // TODO : もう少しいい感じのファイルに移したいよね
    {
        public static T? FirstOrValueNull<T>(this IEnumerable<T> values, Func<T, bool> comp)
        where T : struct
        {
            foreach (var v in values)
                if (comp(v)) { return v; }

            return null;
        }
    }
}
