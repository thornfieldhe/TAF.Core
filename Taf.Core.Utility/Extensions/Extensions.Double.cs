// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Double.cs" company="">
//   
// </copyright>
// <summary>
//   double扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace Taf.Core.Utility
{
    using System;

    /// <summary>
    /// The extensions.
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// 将双精度浮点值按指定的小数位数截断
        /// </summary>
        /// <param name="d">
        /// 要截断的双精度浮点数
        /// </param>
        /// <param name="s">
        /// 小数位数，s大于等于0，小于等于15
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double ToFixed(this double d, int s)
        {
            var sp = Math.Pow(10, s);

            if (d < 0)
            {
                return Math.Truncate(d) + Math.Ceiling((d - Math.Truncate(d)) * sp) / sp;
            }

            return Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
        }

        /// <summary>
        /// 按照位数四舍五入
        /// </summary>
        /// <param name="d">
        /// </param>
        /// <param name="s">
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double Round(this double d, int s) => Math.Round(d, s);

        /// <summary>
        /// 是否在范围之间
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <param name="max">
        /// </param>
        /// <param name="min">
        /// </param>
        /// <param name="allowEqual">
        /// 是否包含等于
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsBetween(this double obj, double max, double min, bool allowEqual = false)
        {
            if (allowEqual)
            {
                return obj >= min && obj <= max;
            }

            return obj > min && obj < max;
        }

        /// <summary>
        /// 获取格式化字符串x.xx
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <param name="defaultValue">
        /// 空值显示的默认文本
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Format(this double number, string defaultValue = "") => number == 0 ? defaultValue : string.Format("{0:0.##}", number);

        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <param name="defaultValue">
        /// 空值显示的默认文本
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Format(this double? number, string defaultValue = "") => Format(number.SafeValue(), defaultValue);

        /// <summary>
        /// 获取格式化字符串,x.xx%
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatPercent(this double number) => number == 0 ? string.Empty : string.Format("{0:0.##}%", number);

        /// <summary>
        /// 获取格式化字符串,带%
        /// </summary>
        /// <param name="number">
        /// 数值
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatPercent(this double? number,int sd=2) => FormatPercent(number.SafeValue(),sd);

        /// <summary>
        /// 转换成科学计数法
        /// </summary>
        /// <param name="v"></param>
        /// <param name="sd"></param>
        /// <returns></returns>
        public static string FormatScience(this double v, int sd=3)
        {
            if (v == 0)
                return "0";
            return v.ToString("E" + sd.ToString());
        }


        #region Math
        
        /// <summary>
        /// 求平方
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static double Sqr(this double self) => self * self;

        /// <summary>
        /// 求对数
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static double Ln(this double self) => Math.Log(self, Math.E);

        private const double PrecisionDiff = 0.0000000001D;
        public static bool EqualsEx(this double self, double dest, double diffPre = PrecisionDiff)
        {
            var diff = Math.Abs(self - dest);
            return diff < diffPre;
        }

        public static double Magnitude(this double input) => Math.Log10(Math.Abs(input));


        /// <summary>
        /// 得到三维数组中某个子二维数组
        /// </summary>
        /// <param name="array">输入的三维数组</param>
        /// <param name="remove">要去除的维度</param>
        /// <param name="retainIndex">被去除维度中要保留数据的索引</param>
        /// <returns>结果二维数组</returns>
        /// <example>
        /// z1 x1 x2 x3 x4
        /// y1 1  2  3  5
        /// y2 4  5  6  6
        /// y3 7  8  9  7
        /// =============
        /// z2 x1 x2 x3 x4
        /// y1 1  2  3  5
        /// y2 4  5  6  6
        /// y3 7  8  9  7
        /// 去除Z维保留1 等于
        /// z1 x1 x2 x3 x4
        /// y1 1  2  3  5
        /// y2 4  5  6  6
        /// y3 7  8  9  7
        /// </example>
        public static double[,] Sub(this double[,,] array, ArrayDirection remove, int retainIndex)
        {
            switch (remove)
            {
                case ArrayDirection.Z:
                    var valuesXY = new double[array.GetLength(0), array.GetLength(1)];
                    for (var x = 0; x < array.GetLength(0); x++)
                    {
                        for (var y = 0; y < array.GetLength(1); y++)
                        {
                            valuesXY[x, y] = array[x, y, retainIndex];
                        }
                    }
                    return valuesXY;
                case ArrayDirection.X:
                    var valuesYZ = new double[array.GetLength(1), array.GetLength(2)];
                    for (var y = 0; y < array.GetLength(1); y++)
                    {
                        for (var z = 0; z < array.GetLength(2); z++)
                        {
                            valuesYZ[y, z] = array[retainIndex, y, z];
                        }
                    }
                    return valuesYZ;
                case ArrayDirection.Y:
                    var valuesXZ = new double[array.GetLength(0), array.GetLength(2)];
                    for (var x = 0; x < array.GetLength(0); x++)
                    {
                        for (var z = 0; z < array.GetLength(2); z++)
                        {
                            valuesXZ[x, z] = array[x, retainIndex, z];
                        }
                    }
                    return valuesXZ;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 得到二维数组中某个子一维数组
        /// </summary>
        /// <param name="array">输入的二维数组</param>
        /// <param name="remove">要去除的维度</param>
        /// <param name="retainIndex">被去除维度中要保留数据的索引</param>
        /// <returns>结果一维数组</returns>
        /// <example>
        ///   x1 x2 x3 x4
        /// y1 1  2  3  5
        /// y2 4  5  6  6
        /// y3 7  8  9  7
        /// 去除Y维保留1
        /// X1 X2 X3 X4
        /// 1  2  3  5
        /// </example>
        public static double[] Sub(this double[,] array, ArrayDirection remove, int retainIndex)
        {
            switch (remove)
            {
                case ArrayDirection.X:
                    var valuesY = new double[array.GetLength(1)];
                    for (var y = 0; y < array.GetLength(1); y++)
                    {
                        valuesY[y] = array[retainIndex, y];
                    }
                    return valuesY;
                case ArrayDirection.Y:
                    var valuesX = new double[array.GetLength(0)];
                    for (var x = 0; x < array.GetLength(0); x++)
                    {
                        valuesX[x] = array[x, retainIndex];
                    }
                    return valuesX;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 把三维数组求和为二维数组
        /// </summary>
        /// <param name="array">输入的三维数组</param>
        /// <param name="d1">保留的第一维</param>
        /// <param name="d2">保留的第二维</param>
        /// <returns>结果二维数组</returns>
        /// <example>
        /// z1 x1 x2 x3 x4
        /// y1 1  2  3  5
        /// y2 4  5  6  6
        /// y3 7  8  9  7
        /// =============
        /// z2 x1 x2 x3 x4
        /// y1 1  2  3  5
        /// y2 4  5  6  6
        /// y3 7  8  9  7
        /// 保留X和Y 等于
        ///    x1  x2  x3  x4
        /// y1 2   4   6   10
        /// y2 8   10  12  12
        /// y3 14  16  18  14
        /// </example>
        public static double[,] Sum(this double[,,] array, ArrayDirection d1, ArrayDirection d2)
        {
            if ((int)d1 == (int)d2)
                throw new ArgumentException("参数不能相同");

            var retval = new double[array.GetLength((int)d1), array.GetLength((int)d2)];
            var arrayDirection = (int)d1 + (int)d2;
            switch (arrayDirection)
            {
                case 1:
                    for (var x = 0; x < array.GetLength(0); x++)
                        for (var y = 0; y < array.GetLength(1); y++)
                            for (var z = 0; z < array.GetLength(2); z++)
                            {
                                if (d1 == ArrayDirection.X && d2 == ArrayDirection.Y)
                                    retval[x, y] += array[x, y, z];
                                else
                                    retval[y, x] += array[x, y, z];
                            }
                    break;
                case 2:
                    for (var x = 0; x < array.GetLength(0); x++)
                        for (var z = 0; z < array.GetLength(2); z++)
                            for (var y = 0; y < array.GetLength(1); y++)
                            {
                                if (d1 == ArrayDirection.X && d2 == ArrayDirection.Z)
                                    retval[x, z] += array[x, y, z];
                                else
                                    retval[z, x] += array[x, y, z];
                            }
                    break;
                case 3:
                    for (var y = 0; y < array.GetLength(1); y++)
                        for (var z = 0; z < array.GetLength(2); z++)
                            for (var x = 0; x < array.GetLength(0); x++)
                            {
                                if (d1 == ArrayDirection.Y && d2 == ArrayDirection.Z)
                                    retval[y, z] += array[x, y, z];
                                else
                                    retval[z, y] += array[x, y, z];

                            }
                    break;
            }
            return retval;
        }

        /// <summary>
        /// 对三维数组求和
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double Sum(this double[,,] array) => array.Sum(ArrayDirection.X, ArrayDirection.Y).Sum();

        /// <summary>
        /// 对二维数组根据某个维度进行求和
        /// 即把二维数组变为一维数组
        /// </summary>
        /// <param name="array"></param>
        /// <param name="d">要保留的维度</param>
        /// <returns>结果一维数组</returns>
        /// <example>
        /// x1 x2 x3 x4
        /// y1 1  2  3  5
        /// y2 4  5  6  6
        /// y3 7  8  9  7
        /// 根据X维进行求和
        /// X1 X2 X3 X4
        /// 12 15 18 18
        /// </example>
        public static double[] Sum(this double[,] array, ArrayDirection d)
        {
            var count1 = array.GetLength((int)d);
            var retval = new double[count1];
            if (d == ArrayDirection.X)
            {
                for (var x = 0; x < count1; x++)
                {
                    var count2 = array.GetLength(1);
                    var subValues = new double[count2];
                    for (var y = 0; y < count2; y++)
                    {
                        subValues[y] = array[x, y];
                    }
                    retval[x] = subValues.Sum();
                }
            }
            else if (d == ArrayDirection.Y)
            {
                for (var y = 0; y < count1; y++)
                {
                    var count2 = array.GetLength(0);
                    var subValues = new double[count2];
                    for (var x = 0; x < count2; x++)
                    {
                        subValues[x] = array[x, y];
                    }
                    retval[y] = subValues.Sum();
                }
            }
            return retval;
        }

        /// <summary>
        /// 对二维数组根据进行求和
        ///    x1 x2 x3
        /// y1 1  2  3
        /// y2 4  5  6
        /// y3 7  8  9 
        /// 等于
        /// 45
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double Sum(this double[,] array) => array.Sum(ArrayDirection.X).Sum();

        /// <summary>
        /// 一维数组中的所有数都除以同一个数 arraA/valueB
        /// </summary>
        /// <param name="arrayA">一维数组</param>
        /// <param name="valueB">分母数</param>
        /// <returns></returns>
        public static double[] Divide(this double[] arrayA, double valueB)
        {
            if (valueB == 0)
                valueB = 1; //throw new ArgumentException("除数不能为零");

            var retval = new double[arrayA.Length];
            for (var x = 0; x < arrayA.Length; x++)
            {
                retval[x] = arrayA[x] / valueB;
            }
            return retval;
        }
        /// <summary>
        /// 二维数组的所有数都除以同一个数 arrayA/valueB
        /// </summary>
        /// <param name="arrayA">二维数组</param>
        /// <param name="valueB">分母数</param>
        /// <returns></returns>
        public static double[,] Divide(this double[,] arrayA, double valueB)
        {
            if (valueB == 0)
                valueB = 1; //throw new ArgumentException("除数不能为零");

            var retval = new double[arrayA.GetLength(0), arrayA.GetLength(1)];
            for (var x = 0; x < arrayA.GetLength(0); x++)
                for (var y = 0; y < arrayA.GetLength(1); y++)
                {
                    retval[x, y] = arrayA[x, y] / valueB;
                }
            return retval;
        }
        /// <summary>
        /// 二维数组的所有数按照维度除以一维数组 arrayA/arrayB
        /// 要保证在这个维度上，元素数量一样
        /// </summary>
        /// <param name="arrayA">二维数组</param>
        /// <param name="arrayB">一维数组</param>
        /// <param name="d">除的维度</param>
        /// <returns></returns>
        public static double[,] Divide(this double[,] arrayA, double[] arrayB, ArrayDirection d)
        {
            //
            if (arrayA.GetLength((int)d) != arrayB.Length)
                throw new ArgumentException("数组维度不一致");

            var retval = new double[arrayA.GetLength(0), arrayA.GetLength(1)];
            if (d == ArrayDirection.X)
            {
                for (var x = 0; x < arrayA.GetLength(0); x++)
                {
                    if (arrayB[x] != 0)
                    {
                        for (var y = 0; y < arrayA.GetLength(1); y++)
                        {
                            retval[x, y] = arrayA[x, y] / arrayB[x];
                        }
                    }
                }
            }
            else if (d == ArrayDirection.Y)
            {
                for (var y = 0; y < arrayA.GetLength(1); y++)
                {
                    if (arrayB[y] != 0)
                    {
                        for (var x = 0; x < arrayA.GetLength(0); x++)
                        {
                            retval[x, y] = arrayA[x, y] / arrayB[y];
                        }
                    }
                }
            }
            return retval;
        }

        /// <summary>
        /// 三维数组的所有数都除以同一个数 arrayA/valueB
        /// </summary>
        /// <param name="arrayA">三维数组</param>
        /// <param name="valueB">分母数</param>
        /// <returns></returns>
        public static double[,,] Divide(this double[,,] arrayA, double valueB)
        {
            if (valueB == 0)
                valueB = 1; //throw new ArgumentException("除数不能为零");

            var retval = new double[arrayA.GetLength(0), arrayA.GetLength(1), arrayA.GetLength(2)];
            for (var x = 0; x < arrayA.GetLength(0); x++)
                for (var y = 0; y < arrayA.GetLength(1); y++)
                    for (var z = 0; z < arrayA.GetLength(2); z++)
                    {
                        retval[x, y, z] = arrayA[x, y, z] / valueB;
                    }
            return retval;
        }

        /// <summary>
        /// 三维数组的所有数按照维度除以一维数组 arrayA/arrayB
        /// 要保证在这个维度上，元素数量一样
        /// </summary>
        /// <param name="arrayA">三维数组</param>
        /// <param name="arrayB">一维数组</param>
        /// <param name="d">除以的维度</param>
        /// <returns></returns>
        public static double[,,] Divide(this double[,,] arrayA, double[] arrayB, ArrayDirection d)
        {
            if (arrayA.GetLength((int)d) != arrayB.Length)
                throw new ArgumentException("数组维度不一致");

            var retval = new double[arrayA.GetLength(0), arrayA.GetLength(1), arrayA.GetLength(2)];
            if (d == ArrayDirection.X)
            {
                for (var x = 0; x < arrayA.GetLength(0); x++)
                {
                    if (arrayB[x] != 0)
                    {
                        for (var y = 0; y < arrayA.GetLength(1); y++)
                            for (var z = 0; z < arrayA.GetLength(2); z++)
                            {
                                retval[x, y, z] = arrayA[x, y, z] / arrayB[x];
                            }
                    }
                }
            }
            else if (d == ArrayDirection.Y)
            {
                for (var y = 0; y < arrayA.GetLength(1); y++)
                {
                    if (arrayB[y] != 0)
                    {
                        for (var x = 0; x < arrayA.GetLength(0); x++)
                            for (var z = 0; z < arrayA.GetLength(2); z++)
                            {
                                retval[x, y, z] = arrayA[x, y, z] / arrayB[y];
                            }
                    }
                }
            }
            else if (d == ArrayDirection.Z)
            {
                for (var z = 0; z < arrayA.GetLength(2); z++)
                {
                    if (arrayB[z] != 0)
                    {
                        for (var x = 0; x < arrayA.GetLength(0); x++)
                            for (var y = 0; y < arrayA.GetLength(1); y++)
                            {
                                retval[x, y, z] = arrayA[x, y, z] / arrayB[z];
                            }
                    }
                }
            }
            return retval;
        }
        
        /// <summary>
        /// 数组维度
        /// </summary>
        public enum ArrayDirection
        {
            X = 0,
            Y = 1,
            Z = 2
        }

        #endregion
    }
}