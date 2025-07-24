#nullable enable
using System;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace net.rs64.TexTransCore
{
    [Serializable]
    public struct TriangleVertexIndices : IEnumerable<int>, IEquatable<TriangleVertexIndices>
    {
        public int zero;
        public int one;
        public int two;

        public TriangleVertexIndices(int zero, int one, int two)
        {
            this.zero = zero;
            this.one = one;
            this.two = two;
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        [DoesNotReturn]
        static void ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureValidIndexRange(int i)
        {
            if (3 <= (uint)i)
            {
                ThrowIndexOutOfRange();
            }
        }
        public int this[int i]
        {
            readonly get
            {
                EnsureValidIndexRange(i);
                return Unsafe.Add(ref Unsafe.As<TriangleVertexIndices, int>(ref Unsafe.AsRef(in this)), i);
            }
            set
            {
                EnsureValidIndexRange(i);
                Unsafe.Add(ref Unsafe.As<TriangleVertexIndices, int>(ref this), i) = value;
            }
        }
        public Triangle ToTriangle(Span<Vector3> vert)
        {
            return new(vert[zero], vert[one], vert[two]);
        }
        public Triangle2D ToTriangle2D(Span<Vector2> vert)
        {
            return new(vert[zero], vert[one], vert[two]);
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return zero;
            yield return one;
            yield return two;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override bool Equals(object obj)
        {
            return obj is TriangleVertexIndices other && Equals(other);
        }

        public override int GetHashCode() => HashCode.Combine(zero, one, two);

        public static bool operator ==(TriangleVertexIndices left, TriangleVertexIndices right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TriangleVertexIndices left, TriangleVertexIndices right)
        {
            return !(left == right);
        }

        public bool Equals(TriangleVertexIndices other)
        {
            return zero == other.zero && one == other.one && two == other.two;
        }

        public static IEnumerable<int> SelectMany(IEnumerable<TriangleVertexIndices> ints) { foreach (var tri in ints) { for (var i = 0; 3 > i; i += 1) { yield return tri[i]; } } }
    }
    public struct Triangle : IEnumerable<Vector3>
    {
        public Vector3 zero;
        public Vector3 one;
        public Vector3 two;

        public Triangle(TriangleVertexIndices TriIndex, Span<Vector3> vector3s)
        {
            zero = vector3s[TriIndex.zero];
            one = vector3s[TriIndex.one];
            two = vector3s[TriIndex.two];
        }

        public Triangle(Vector3 vector31, Vector3 vector32, Vector3 vector33)
        {
            zero = vector31;
            one = vector32;
            two = vector33;
        }

        public TTVector4 Cross(Vector3 TargetPoint)
        {
            var u = Vector3.Cross(two - one, TargetPoint - one).Z;
            var v = Vector3.Cross(zero - two, TargetPoint - two).Z;
            var w = Vector3.Cross(one - zero, TargetPoint - zero).Z;
            var uvw = Vector3.Cross(one - zero, two - zero).Z;
            return new TTVector4(u, v, w, uvw);
        }

        public Vector3 FromBCS(Vector3 SourceTBC)
        {
            var conversionPos = new Vector3(0, 0, 0);
            conversionPos += zero * SourceTBC.X;
            conversionPos += one * SourceTBC.Y;
            conversionPos += two * SourceTBC.Z;
            return conversionPos;
        }

        public IEnumerator<Vector3> GetEnumerator()
        {
            yield return zero;
            yield return one;
            yield return two;
        }

        public Vector3[] ToArray()
        {
            return new Vector3[3] { zero, one, two };
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DoesNotReturn]
        static void ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureValidIndexRange(int i)
        {
            if (3 <= (uint)i)
            {
                ThrowIndexOutOfRange();
            }
        }
        public Vector3 this[int i]
        {
            readonly get
            {
                EnsureValidIndexRange(i);
                return Unsafe.Add(ref Unsafe.As<Triangle, Vector3>(ref Unsafe.AsRef(in this)), i);
            }
            set
            {
                EnsureValidIndexRange(i);
                Unsafe.Add(ref Unsafe.As<Triangle, Vector3>(ref this), i) = value;
            }
        }

        
    }
    public struct Triangle2D : IEnumerable<Vector2>
    {
        public Vector2 zero;
        public Vector2 one;
        public Vector2 two;

        public Triangle2D(TriangleVertexIndices TriIndex, Span<Vector2> vectors)
        {
            zero = vectors[TriIndex.zero];
            one = vectors[TriIndex.one];
            two = vectors[TriIndex.two];
        }

        public Triangle2D(Vector2 vector1, Vector2 vector2, Vector2 vector3)
        {
            zero = vector1;
            one = vector2;
            two = vector3;
        }


        public IEnumerator<Vector2> GetEnumerator()
        {
            yield return zero;
            yield return one;
            yield return two;
        }

        public Vector2[] ToArray()
        {
            return new Vector2[3] { zero, one, two };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DoesNotReturn]
        static void ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureValidIndexRange(int i)
        {
            if (3 <= (uint)i)
            {
                ThrowIndexOutOfRange();
            }
        }
        public Vector2 this[int i]
        {
            readonly get
            {
                EnsureValidIndexRange(i);
                return Unsafe.Add(ref Unsafe.As<Triangle2D, Vector2>(ref Unsafe.AsRef(in this)), i);
            }
            set
            {
                EnsureValidIndexRange(i);
                Unsafe.Add(ref Unsafe.As<Triangle2D, Vector2>(ref this), i) = value;
            }
        }
    }

}
