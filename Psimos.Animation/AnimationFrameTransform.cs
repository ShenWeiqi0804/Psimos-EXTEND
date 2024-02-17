using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psimos.Animation
{
    public delegate float TransformFunc(float x);
    public class AnimationFrameTransform
    {
        /// <summary>
        /// 推荐使用预设的模型，因为这样可以防止出现一些因函数不过(0, 0)和(1, 1)所引发的问题
        /// </summary>
        public static class Models
        {
            /// <summary>
            /// 线性模型 y = x
            /// </summary>
            public static AnimationFrameTransform Linear { get
                {
                    Dictionary<float, float> factors = new Dictionary<float, float>();
                    factors.Add(1.0f, 1.0f);
                    return new AnimationFrameTransform(factors);
                } 
            }
            /// <summary>
            /// 对数模型，y = log2(x + 1)
            /// </summary>
            public static AnimationFrameTransform Logarithmic { get
                {
                    return new AnimationFrameTransform(new TransformFunc(x => (float)(Math.Log(x + 1) / Math.Log(2))));
                }
            }
            /// <summary>
            /// 平方模型 y = x^2
            /// </summary>
            public static AnimationFrameTransform Quadratic
            {
                get
                {
                    Dictionary<float, float> factors = new Dictionary<float, float>();
                    factors.Add(2.0f, 1.0f);
                    return new AnimationFrameTransform(factors);
                }
            }
            /// <summary>
            /// 立方模型 y = x^3
            /// </summary>
            public static AnimationFrameTransform Cubic
            {
                get
                {
                    Dictionary<float, float> factors = new Dictionary<float, float>();
                    factors.Add(3.0f, 1.0f);
                    return new AnimationFrameTransform(factors);
                }
            }
            /// <summary>
            /// 平方根模型 y = x^(1/2)
            /// </summary>
            public static AnimationFrameTransform Sqrt
            {
                get
                {
                    Dictionary<float, float> factors = new Dictionary<float, float>();
                    factors.Add(0.5f, 1.0f);
                    return new AnimationFrameTransform(factors);
                }
            }
            /// <summary>
            /// 反比模型， y = -2 / (x + 1) + 2
            /// </summary>
            public static AnimationFrameTransform Inverse
            {
                get
                {
                    return new AnimationFrameTransform(new TransformFunc(x => (-2) / (x + 1) + 2));
                }
            }
        }
        public float Calcute(float x)
        {
            return func(x);
        }
        private float FactorsCalcute(float x)
        {
            float sum = 0.0f;
            for (int i = 0;i < this.factors.Count;i++)
            {
                float factor, power;
                KeyValuePair<float, float> pair = factors.Skip(i).First();
                factor = pair.Value;
                power = pair.Key;
                sum += factor * (float)Math.Pow(x, power);
            }
            return sum;
        }
        private Dictionary<float, float> factors;
        private TransformFunc func;
        public AnimationFrameTransform(Dictionary<float, float> factors)
        {
            this.factors = factors;
            func = this.FactorsCalcute;
        }
        public AnimationFrameTransform(TransformFunc func)
        {
            factors = null;
            this.func = func;
        }
    }
}
