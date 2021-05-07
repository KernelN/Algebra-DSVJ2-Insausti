using UnityEngine;
using System;
namespace CustomMath
{
    [System.Serializable]
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude { get { return this.x * this.x + this.y * this.y + this.z * this.z; } }
        public Vec3 normalized { get { return sqrMagnitude > 0 ? this / magnitude : this; } }
        public float magnitude { get { return Mathf.Sqrt(this.sqrMagnitude); } }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector3(v2.x, v2.y);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        public static float Angle(Vec3 from, Vec3 to) //MUST CHECK
        {
            return (float)Math.Atan((from.y - to.y) / (from.x - to.x));
        }
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength) //MUST CHECK
        {
            if (SqrMagnitude(vector) > maxLength * maxLength) //check current magnitude is bigger than maxLength
            {
                vector.Normalize(); //make it's magnitude 1
                vector *= maxLength; //multiply everything by the target magnitude
            }

            return vector;
        }
        public static float Magnitude(Vec3 vector)
        {
            return Mathf.Sqrt(SqrMagnitude(vector));
            //the 3 components of vector can be used to make a rectangle triangle, 
            //the magnitude function returns the hypothenuse using pythagoras theorem
        }
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(a.y * b.z - b.y * a.z, a.z * b.x - b.z * a.x, a.x * b.y - b.x * a.y);
        }
        public static float Distance(Vec3 a, Vec3 b)
        {
            return Magnitude(a - b);
        }
        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            if (t > 1)
            {
                t = 1;
            }
            else if (t < 0)
            {
                t = 0;
            }

            return new Vec3(a + (b - a) * t);
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            return new Vec3(a + (b - a) * t);
        }
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            if (a.x < b.x) { a.x = b.x; }
            if (a.y < b.y) { a.y = b.y; }
            if (a.z < b.z) { a.z = b.z; }

            return a; //creates vector that is made from the largest components of the two vectors.
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            if (a.x > b.x) { a.x = b.x; }
            if (a.y > b.y) { a.y = b.y; }
            if (a.z > b.z) { a.z = b.z; }

            return a; //creates vector that is made from the smallest components of the two vectors.
        }
        public static float SqrMagnitude(Vec3 vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }
        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            if (onNormal.sqrMagnitude == 0)
            {
                return onNormal;
            }

            onNormal.Normalize();

            return onNormal * Dot(vector, onNormal);
            //Dot(vector, normal) gives you the target magnitude eliminating the null components of onNormal
            //(.Dot is like magnitude but using 2 different vectors instead of 1)
            //multiplies onNormal (magnitude 1) by the target magnitude

            //https://stackoverflow.com/questions/26958198/vector-projection-rejection-in-c
        }
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            //inNormal.Normalize();
            Vec3 aux = Project(inDirection, inNormal) + inDirection;
            return Project(aux, inNormal);
            //return inDirection - 2 * Dot(inDirection, inNormal) * inNormal;
            //inDirection = speed | inNormal = normal vector of the plane in which inDirection is reflected

            /*
             * Reflect creates a isosceles triangle (between inDirection and result)
             * when crossed by the inNormal vector, the triangle becomes 2 rectangle triangles
             * inDirection + the adjacent leg of the rectangle triangle = result
             * so 
             * 
             */

            //https://docs.google.com/drawings/d/1ZiFlsP0eCDdVjM5VYNB4_npeqmbwRZMAeRs7Bg3EoPA/edit?usp=sharing
            //http://bocilmania.com/2018/04/21/how-to-get-reflection-vector/
        }
        public void Set(float newX, float newY, float newZ)
        {
            this = new Vec3(newX, newY, newZ);
        }
        public void Scale(Vec3 scale)
        {
            this = new Vec3(this.x * scale.x, this.y * scale.y, this.z * scale.z); //must be a shorter way
        }
        public void Normalize()
        {
            if (sqrMagnitude > 0)
            {
                this /= magnitude;
            }

            //grab the triangle formed by the components and the magnitude, and divide each compontent by the magnitude
            //dividing each component by the magnitude is like dividing the magnitude by itself, 
            //so the magnitude becomes 1

            //https://www.khanacademy.org/computing/computer-programming/programming-natural-simulations/programming-vectors/a/vector-magnitude-normalization
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}