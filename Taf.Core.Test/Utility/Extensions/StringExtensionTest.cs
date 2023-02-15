// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensionTest.cs" company="">
//   
// </copyright>
// <summary>
//   The string extension test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Taf.Core.Test
{


    /// <summary>
    /// The string extension test.
    /// </summary>
    
    public class StringExtensionTest
    {
        #region PinYin(获取拼音简码)

        /// <summary>
        /// 获取拼音简码
        /// </summary>
        [Fact]
        public void TestPinYin()
        {
            string pinyin = null;
            Assert.Equal(string.Empty, pinyin.GetChineseSpell());
            pinyin = string.Empty;
            Assert.Equal(string.Empty, pinyin.GetChineseSpell());
            Assert.Equal("ZG", "中国".GetChineseSpell());
//            Assert.Equal("ZhongGuo", "中国".ConvertCh());
            Assert.Equal("A1BCB2", "a1宝藏b2".GetChineseSpell());
            Assert.Equal("TT", "饕餮".GetChineseSpell());
        }

        #endregion

        #region FirstUpper(将值的首字母大写)

        /// <summary>
        /// 将值的首字母大写
        /// </summary>
        [Fact]
        public void TestFirstUpper()
        {
            const string text = "aBc";
            var actual = text.ToCapit();
            Assert.Equal("ABc", actual);
        }

        #endregion

        #region ToCamel(将字符串转成驼峰形式)

        /// <summary>
        /// 将字符串转成驼峰形式
        /// </summary>
        [Fact]
        public void TestToCamel()
        {
            Assert.Equal("aBc", "ABc".ToCamel());
        }

        #endregion

        /// <summary>
        /// 将字符串转成驼峰形式
        /// </summary>
        [Fact]
        public void TestToCapit()
        {
            Assert.Equal("ABc", "_aBc".ToCapit());
        }

        /// <summary>
        /// 测试mysql属性转大驼峰写法
        /// </summary>
        [Fact]
        public void TestToProperCaseFromUnderLine(){
            var test = "business_database_informations";
            Assert.Equal("BusinessDatabaseInformation", test.ToProperCaseFromUnderLine());
        }
        
        /// <summary>
        /// 测试复数转单数
        /// </summary>
        [Fact]
        public void TestToSingular(){
            var word = "computers";
            Assert.Equal("computer", word.ToSingular());
        }
        
        /// <summary>
        /// 测试单数转复数
        /// </summary>
        [Fact]
        public void TestToPlural(){
            var word = "computer";
            Assert.Equal("computers", word.ToPlural());
        }
        
        #region ContainsChinese(是否包含中文)

        /// <summary>
        /// 大驼峰转下划线小写
        /// </summary>
        [Fact]
        public void TestToUnderLine(){
            var test = "BusinessDatabaseInformation";
            Assert.Equal("business_database_information",test.ToUnderLine());
        }
        
        /// <summary>
        /// 测试是否包含中文
        /// </summary>
        [Fact]
        public void TestContainsChinese()
        {
            Assert.True("a中1文b".ContainsChinese());
            Assert.False("a1b".ContainsChinese());
        } 
        
        /// <summary>
        /// 测试字符串压缩,解压
        /// </summary>
        [Fact]
        public void TestCompressString()
        {
            var a =
                "0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/1/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0.000444/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/1.66E-06/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0.00418/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/2.53E-05/0/0/0/0/0.00262/0.00199/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/8.08E-07/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/2.35E-05/0/0/0/0/1.96E-05/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0.0711/36.1/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/2.360340539506409E-07/1.8938299290632575E-07/3.864549454754161E-08/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/2.53E-05/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0.0025/0.015/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/9.92E-06/0/9E-06/0/0/0/0/0/0/0/0/0/0/0.00365/0/0/0/0/0/0/0/0/0/0/0/0/0.254/0/0/0/0/0/0/0/0/0/0/0/0/0/0.00118/0/0/0.115/0/0/0.000391/0/0/0/0/0/0/0/0/0/0/0/0.00493/0.00152/0/0/0/0/0/0/0/0/0/0/8.42/0/6.21E-05/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/7.02E-06/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0_58vhz48zpHVHlWLiuhO2";
            var b = GZipCompress.CompressString(a);
            
            Assert.Equal(a,GZipCompress.DecompressString(b));
        }

        #endregion

        #region TestContainsNumber(是否包含数字)

        /// <summary>
        /// 测试是否包含数字
        /// </summary>
        [Fact]
        public void TestContainsNumber()
        {
            Assert.True("a中1文b".ContainsNumber());
            Assert.True("a中2文b".ContainsNumber());
            Assert.True("a中3文b".ContainsNumber());
            Assert.True("a中4文b".ContainsNumber());
            Assert.True("a中5文b".ContainsNumber());
            Assert.True("a中6文b".ContainsNumber());
            Assert.True("a中7文b".ContainsNumber());
            Assert.True("a中8文b".ContainsNumber());
            Assert.True("a中9文b".ContainsNumber());
            Assert.True("a中0文b".ContainsNumber());
            Assert.False("ab".ContainsNumber());
        }

        #endregion

        #region Distinct(去除重复)

        /// <summary>
        /// 去除重复
        /// </summary>
        [Fact]
        public void TestDistinct()
        {
            Assert.Equal("5", "55555".Distinct());
            Assert.Equal("45", "45454545".Distinct());
        }

        #endregion

        #region Truncate(截断字符串)

        /// <summary>
        /// 截断字符串
        /// </summary>
        [Fact]
        public void TestTruncate()
        {
            string a = null;
            Assert.Equal(string.Empty, a.Truncate(4));
            a = string.Empty;
            Assert.Equal(string.Empty, a.Truncate(4));
            Assert.Equal("abcd", "abcdef".Truncate(4));
            Assert.Equal("abcd..", "abcdef".Truncate(4, 2));
            Assert.Equal("abcd--", "abcdef".Truncate(4, 2, "-"));
            Assert.Equal("ab", "ab".Truncate(4));
        }

        #endregion

        #region GetLastProperty(获取最后一个属性)

        /// <summary>
        /// 获取最后一个属性
        /// </summary>
        [Fact]
        public void TestGetLastProperty()
        {
            string a = null;
            Assert.Equal(string.Empty, a.GetLastProperty());
            Assert.Equal(string.Empty, string.Empty.GetLastProperty());
            Assert.Equal("A", "A".GetLastProperty());
            Assert.Equal("B", "A.B".GetLastProperty());
            Assert.Equal("C", "A.B.C".GetLastProperty());
        }

        #endregion

        /// <summary>
        /// 实现各进制数间的转换
        /// </summary>
        [Fact]
        public void TestConvertBase()
        {
            Assert.Equal("00010101", "21".ConvertBase(10, 2));
            Assert.Equal("25", "21".ConvertBase(10, 8));
            Assert.Equal("15", "21".ConvertBase(10, 16));
        }

        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        [Fact]
        public void TestStrLength()
        {
            Assert.Equal(5, "21中r".StrLength());
        }

        /// <summary>
        /// 如果字符串不为空则执行
        /// </summary>
        [Fact]
        public void TestStrIsNotNullAction()
        {
            var a = string.Empty;
            a.IfIsNullOrEmpty(() => a = "111");
            a.IfIsNotNullOrEmpty(r => a = "222");
            Assert.Equal("222", a);
        }

        /// <summary>
        /// 获取一侧n个字符
        /// </summary>
        [Fact]
        public void TestGetLengthOfChars()
        {
            var a = "qwerty";

            Assert.Equal("qw", a.Left(2));
            Assert.Equal("ty", a.Right(2));
        }

        /// <summary>
        /// 格式化创建字符串
        /// </summary>
        [Fact]
        public void TestFormateString()
        {
            var a = "a{0}{1}";
            Assert.Equal("abc", a.FormatWith("b", "c"));
        }

        /// <summary>
        /// 格式化创建字符串
        /// </summary>
        [Fact]
        public void TestStringEquale()
        {
            var a = "abc";
            Assert.True(a.IgnoreCaseEqual("Abc"));
        }

        /// <summary>
        /// 返回一个字符串用空格分隔
        /// </summary>
        [Fact]
        public void TestWordify()
        {
            var a = "aGoodPeople";
            Assert.Equal("a Good People", a.Wordify());
        }

        /// <summary>
        /// 翻转字符串
        /// </summary>
        [Fact]
        public void TestReverse()
        {
            var a = "abcde";
            Assert.Equal("edcba", a.Reverse());
        }

        /// <summary>
        /// 指定字符串是否在集合中
        /// </summary>
        [Fact]
        public void TestIsInArryString()
        {
            var a = "A";
            Assert.True(a.IsInArryString("A,B,C,D,E", ','));
        }

        /// <summary>
        /// 替换最后一个匹配的字符串
        /// </summary>
        [Fact]
        public void TestReplaceLast()
        {
            var a = "ABASDAS";
            Assert.Equal("ABASDMS", a.ReplaceLast("A", "M"));
        }

        /// <summary>
        /// 替换第一个匹配的字符串
        /// </summary>
        [Fact]
        public void TestReplaceFirst()
        {
            var a = "ABASDAS";
            Assert.Equal("MBASDAS", a.ReplaceFirst("A", "M"));
        }

        /// <summary>
        /// 替换第一个匹配的字符串
        /// </summary>
        [Fact]
        public void TestCountOccurences()
        {
            var a = "ABASDAS";
            Assert.Equal(3, a.CountOccurences("A"));
        }

        /// <summary>
        /// 匹配字符串
        /// </summary>
        [Fact]
        public void TestFindSubstringAsString()
        {
            var a = "ABASDAS";
            Assert.Equal(2, a.FindSubstringAsString("A.").Count);
            Assert.Equal(3, a.FindSubstringAsString("A.", false).Count); // AS子串出现了2次
            var b = "21_22/21";
            Assert.Equal(2, b.FindSubstringAsSInt(@"\d\d").Count);
            Assert.Equal(3, b.FindSubstringAsSInt(@"\d{2}", false).Count); // 2*子串出现了2次
        }

        /// <summary>
        /// 替换分组字符串
        /// </summary>
        [Fact]
        public void TestReplaceReg()
        {
            var b = "21_22/21";
            Assert.Equal("2x_2x/2x", b.ReplaceReg(@"(\d)(\d)", "x", 2));
        }

        /// <summary>
        /// 截取包含中文的字符串
        /// </summary>
        [Fact]
        public void TestCut()
        {
            var a = "12345678";
            Assert.Equal("12345", a.Cut(5));
            var b = "中国1234中国";
            Assert.Equal("中国1", b.Cut(5));
        }

        /// <summary>
        /// 转换数字金额主函数（包括小数）
        /// </summary>
        [Fact]
        public void TestConvertRmb()
        {
            var a = "12345678.123";
            Assert.Equal("壹仟贰佰叁拾肆万伍仟陆佰柒拾捌元壹角贰分", a.ConvertRMB());
        }

        /// <summary>
        /// 测试是否匹配模式
        /// </summary>
        [Fact]
        public void TestIsMatch()
        {
            var pattern = @"^\d.*";
            Assert.False("abc".IsMatch(pattern));
            Assert.True("123".IsMatch(pattern));
        }

        #region Splice(拼接集合元素)

        /// <summary>
        /// 拼接int集合元素，默认用逗号分隔，不带引号
        /// </summary>
        [Fact]
        public void TestSplice_Int()
        {
            Assert.Equal("1,2,3", (new List<int> { 1, 2, 3 }).Splice());
        }

        /// <summary>
        /// 拼接int集合元素，带单引号
        /// </summary>
        [Fact]
        public void TestSplice_Int_SingleQuotes()
        {
            Assert.Equal("'1','2','3'", (new List<int> { 1, 2, 3 }).Splice("'"));
        }

        /// <summary>
        /// 拼接int集合元素，空分隔符
        /// </summary>
        [Fact]
        public void TestSplice_Int_EmptySeparator()
        {
            Assert.Equal("123", (new List<int> { 1, 2, 3 }).Splice(string.Empty, string.Empty));
        }

        /// <summary>
        /// 拼接int集合元素，带双引号
        /// </summary>
        [Fact]
        public void TestSplice_Int_DoubleQuotes()
        {
            Assert.Equal("\"1\",\"2\",\"3\"", (new List<int> { 1, 2, 3 }).Splice("\""));
        }

        /// <summary>
        /// 拼接int集合元素，用空格分隔
        /// </summary>
        [Fact]
        public void TestSplice_Int_Blank()
        {
            Assert.Equal("1 2 3", (new List<int> { 1, 2, 3 }).Splice(string.Empty, " "));
        }

        /// <summary>
        /// 拼接int集合元素，用分号分隔
        /// </summary>
        [Fact]
        public void TestSplice_Int_Semicolon()
        {
            Assert.Equal("1;2;3", (new List<int> { 1, 2, 3 }).Splice(string.Empty, ";"));
        }

        /// <summary>
        /// 拼接字符串集合元素
        /// </summary>
        [Fact]
        public void TestSplice_String()
        {
            Assert.Equal("1,2,3", (new List<string> { "1", "2", "3" }).Splice());
        }

        /// <summary>
        /// 拼接字符串集合元素，带单引号
        /// </summary>
        [Fact]
        public void TestSplice_String_SingleQuotes()
        {
            Assert.Equal("'1','2','3'", (new List<string> { "1", "2", "3" }).Splice("'"));
        }

        /// <summary>
        /// 将字符串移除最后一个分隔符并转换为列表
        /// </summary>
        [Fact]
        public void TestSplitToList()
        {
            Assert.Equal("2", "1,2,".SplitToList(',')[1]);
            Assert.Equal(2, "1,2,".SplitToList(',').Count);
        }

        /// <summary>
        /// 拼接Guid集合元素
        /// </summary>
        [Fact]
        public void TestSplice_Guid()
        {
            Assert.Equal(
                            "83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A".ToLower(),
                (new List<Guid>
                     {
                         new("83B0233C-A24F-49FD-8083-1337209EBC9A"),
                         new("EAB523C6-2FE7-47BE-89D5-C6D440C3033A")
                     }).Splice(string.Empty));
        }

        /// <summary>
        /// 拼接Guid集合元素，带单引号
        /// </summary>
        [Fact]
        public void TestSplice_Guid_SingleQuotes()
        {
            Assert.Equal(
                            "'83B0233C-A24F-49FD-8083-1337209EBC9A','EAB523C6-2FE7-47BE-89D5-C6D440C3033A'".ToLower(),
                (new List<Guid>
                     {
                         new("83B0233C-A24F-49FD-8083-1337209EBC9A"),
                         new("EAB523C6-2FE7-47BE-89D5-C6D440C3033A")
                     }).Splice("'"));
        }

        #endregion


        /// <summary>
        /// 数字字符串转中文
        /// </summary>
        [Fact]
        public void TestNumberToChinese()
        {
            var num = "5875246";
            Assert.Equal("五八七五二四六",num.NumberToChinese());
        }


        /// <summary>
        /// 中文转数字字符串
        /// </summary>
        [Fact]
        public void TestChineseToNumber()
        {
            var num = "五八七五二四六";
            Assert.Equal("5875246",num.ChineseToNumber());
        }

        /// <summary>
        /// 中文转数字字符串
        /// </summary>
        [Fact]
        public void TestRemoveLastString()
        {
            var m = "12345</br>";
            var b = "</br>";
            Assert.Equal("12345", m.RemoveLastString(b));
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        [Fact]
        public void TestRemoveStartString()
        {
            var m = "</br>12345";
            var b = "</br>";
            Assert.Equal("12345", m.RemoveStartString(b));
        }
        
        /// <summary>
        /// 移除起始字符串
        /// </summary>
        [Fact]
        public void TestCamelToUnderline(){
            var a = "AvideMdikeIkjg";
            Assert.Equal("avide_mdike_ikjg", a.ToUnderLine());
        }
    }
}