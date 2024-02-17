using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Psimos.Support;

namespace Psimos.Animation
{
    public sealed class BehaviourAdministrator<T>
    {
        readonly FieldInfo fieldInfo;
        public readonly T val;
        /// <summary>
        /// 初始化一个BehaviourAdministrator对象
        /// </summary>
        /// <param name="behaviour">行为对象</param>
        /// <param name="fieldName">要操作的字段名称，该字段必须可读可写</param>
        /// <param name="val">值，必须是一个可以进行与实数的乘法运算并且结果可以相加的对象</param>
        public BehaviourAdministrator(Behaviour behaviour, string fieldName, T val)
        {
            System.Reflection.FieldInfo field = behaviour.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.GetProperty);
            fieldInfo = field;
            this.val = val;
        }
    }
}
