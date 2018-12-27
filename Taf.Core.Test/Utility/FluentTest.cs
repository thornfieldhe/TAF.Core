

using TAF.Core.Utility;
using TAF.Test;
using Xunit;
using System;
using System.Collections.Generic;

namespace TAF.Core.Test
{
    
    /// <summary>
    /// 
    /// </summary>
    
    public class FluentTest
    {
        [Fact]
        public void Test_Conditional_1_If_Excuted()
        {
            bool result = false;

            Fx.If(() =>
            {
                return true;
            }).Then(() =>
            {
                result = true;
            }).Else(() =>
            {
                result = false;
            });

            Assert.True(result);
        }
        [Fact]
        public void Test_Conditional_2_Else_Excuted()
        {
            bool result = false;
            Fx.If(() =>
            {
                return false;
            }).Then(() =>
            {
                result = true;
            }).Else(() =>
            {
                result = false;
            });
            Assert.False(result);
        }

        [Fact]
        public void Test_Conditional_3_ElseIf_Excuted()
        {
            int result = 0;
            int expected = 3;
            Fx
                .If(() => expected == 997).Then(() =>
                {
                    result = 1;
                })
                .ElseIf(() => expected == 998).Then(() =>
                {
                    result = 2;
                })
                .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_4_And_Evaluated()
        {
            int result = 0;
            int expected = 1;
            Fx
                .If(() =>
                {
                    return true;
                }).And(() =>
                {
                    return true;
                }).Then(() =>
                {
                    result = 1;
                })
                .ElseIf(() =>
                {
                    return expected == 998;
                }).Then(() =>
                {
                    result = 2;
                })
                .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_5_OrNot_Evaluated()
        {
            int result = 0;
            int expected = 2;
            Fx
                .If(() =>
                {
                    return true;
                }).And(() =>
                {
                    return false;
                }).Then(() =>
                {
                    result = 1;
                })
                .ElseIf(() =>
                {
                    return expected == 998;
                }).OrNot(() =>
                {
                    return false;
                }).Then(() =>
                {
                    result = 2;
                })
                .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_6_Long_Nesting_Evaluated()
        {
            int result = 0;
            int expected = 1;
            Fx
                .If(() =>
                {
                    return true;
                }).And(() =>
                {
                    return true;
                }).Or(() =>
                {
                    return false;
                }).Xor(() =>
                {
                    return false;
                }).AndNot(() =>
                {
                    return false;
                }).Then(() =>
                {
                    result = 1;
                })
                .ElseIf(() =>
                {
                    return expected == 998;
                }).OrNot(() =>
                {
                    return false;
                }).Then(() =>
                {
                    result = 2;
                })
                .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Loop_1_WhileTrue()
        {
            int result = 0;

            Fx.WhileTrue(() =>
            {
                ++result;
                return result != 6;
            });
            Assert.Equal(result, 6);
        }

        [Fact]
        public void Test_Loop_2_WhileFalse()
        {
            int result = 0;

            Fx.WhileFalse(() =>
            {
                ++result;
                return result == 6;
            });
            Assert.Equal(result, 6);
        }

        [Fact]
        public void Test_Loop_3_While_Do()
        {
            int result = 0;

            Fx.While(() =>
            {
                return result < 6;
            }).Do(() =>
            {
                ++result;
            });

            Assert.Equal(result, 6);
        }

        [Fact]
        public void Test_Loop_4_While_Do_Early_Break()
        {
            int result = 0;
            int conditionEvaluationCount = 0;
            Fx.While(() =>
            {
                ++conditionEvaluationCount;
                return result < 6;
            }).EarlyBreakOn(() =>
            {
                return result == 4;
            }).Do(() =>
{
    ++result;
});

            Assert.Equal(conditionEvaluationCount, 5);
        }

        [Fact]
        public void Test_Loop_5_While_Do_Late_Break()
        {
            int result = 0;
            int conditionEvaluationCount = 0;

            Fx.While(() =>
            {
                ++conditionEvaluationCount;
                return result < 6;
            }).LateBreakOn(() =>
            {
                return result == 4;
            }).Do(() =>
{
    ++result;
});

            Assert.Equal(conditionEvaluationCount, 4);
        }

        [Fact]
        public void Test_Loop_6_Do_While()
        {
            int result = 0;

            Fx.Do(() =>
            {
                ++result;
            }).While(() =>
            {
                return result < 5;
            });

            Assert.Equal(result, 5);
        }

        [Fact]
        public void Test_Try_1_Catch_NoException()
        {
            bool exceptionOccured = false;

            Fx.Try(() =>
            {
            }).Catch<Exception>(ex =>
            {
                exceptionOccured = true;
            });

            Assert.False(exceptionOccured);
        }

        [Fact]
        public void Test_Try_2_Catch_CatchExcuted()
        {
            bool exceptionOccured = false;

            Fx.Try(() =>
            {
                throw new NotImplementedException();
            }).Catch<NotImplementedException>(ex =>
            {
                exceptionOccured = true;
            });

            Assert.True(exceptionOccured);
        }

        [Fact]
        public void Test_Try_4_Catch_CatchExcuted_ByCorrectOrder()
        {
            bool exceptionOccured = false;

            Fx.Try(() =>
            {
                throw new NotImplementedException();
            }).Catch<NotImplementedException, Exception>(ex1 =>
            {
                exceptionOccured = true;
            }, ex2 =>
            {
            });

            Assert.True(exceptionOccured);
        }

        [Fact]
        public void Test_Try_5_SwallowIf()
        {
            int result = 0;
            Fx.Try(() =>
            {
                throw new NotImplementedException();
                //++result;
            }).SwallowIf<NotImplementedException>();

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Test_ForEach()
        {
            int result = 0;
            Fx.ForEach(new List<int>() { 1, 2, 3, 4 }, current =>
            {
                result += current;
            });

            Assert.Equal(result, 10);
        }

        [Fact]
        public void Test_Is_WithSafeNull()
        {
            User entity = null;
            bool isIDMatched = false;
            if (Fx.Is(() => entity.Name == null))
            {

                isIDMatched = true;
            }

            Assert.False(isIDMatched);
        }

        [Fact]
        public void Test_Switch_1_Types_NormalCase()
        {
            int result = -1;

            Fx.Switch<string>()
               .Case<int>().Execute(() =>
               {
                   result = 1;
               })
               .Case<string>().Execute(() =>
               {
                   result = 2;
               })
               .Default(() =>
               {
                   result = 0;
               });

            Assert.Equal(result, 2);
        }

        [Fact]
        public void Test_Switch_2_Types_Default()
        {
            int result = -1;
            Fx
               .Switch<short>()
               .Case<int>().Execute(() =>
               {
                   result = 1;
               })
               .Case<string>().Execute(() =>
               {
                   result = 2;
               })
               .Default(() =>
               {
                   result = 0;
               });

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Test_Switch_3_Instances_NormalCase()
        {
            string condition = "two";
            int result = -1;

            Fx.Switch(condition)
               .Case("one").Execute(() =>
               {
                   result = 1;
               })
               .Case("two").Execute(() =>
               {
                   result = 2;
               })
               .Default(() =>
               {
                   result = 0;
               });

            Assert.Equal(result, 2);
        }

        [Fact]
        public void Test_Switch_4_Instances_Default()
        {
            string condition = "three";
            int result = -1;
            Fx
               .Switch(condition)
               .Case("one").Execute(() =>
               {
                   result = 1;
               })
               .Case("two").Execute(() =>
               {
                   result = 2;
               })
               .Default(() =>
               {
                   result = 0;
               });

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Test_RetryOnFail()
        {
            int result = 0;

            Fx.RetryOnFail(() =>
            {
                ++result;
                return result > 5;
            }, attemptSleepInMilliSeconds: 10);

            Assert.Equal(result, 3);
        }
    }
}
