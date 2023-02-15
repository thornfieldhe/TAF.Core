using Taf.Test;

namespace Taf.Core.Test{
    /// <summary>
    /// 
    /// </summary>
    public class FluentTest{
        [Fact]
        public void Test_Conditional_1_If_Excuted(){
            var result = false;

            Fx.If(() => { return true; }).Then(() => { result = true; }).Else(() => { result = false; });

            Assert.True(result);
        }

        [Fact]
        public void Test_Conditional_2_Else_Excuted(){
            var result = false;
            Fx.If(() => { return false; }).Then(() => { result = true; }).Else(() => { result = false; });
            Assert.False(result);
        }

        [Fact]
        public void Test_Conditional_3_ElseIf_Excuted(){
            var result   = 0;
            var expected = 3;
            Fx
               .If(() => expected     == 997).Then(() => { result = 1; })
               .ElseIf(() => expected == 998).Then(() => { result = 2; })
               .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_4_And_Evaluated(){
            var result   = 0;
            var expected = 1;
            Fx
               .If(() => { return true; }).And(() => { return true; }).Then(() => { result = 1; })
               .ElseIf(() => { return expected == 998; }).Then(() => { result              = 2; })
               .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_5_OrNot_Evaluated(){
            var result   = 0;
            var expected = 2;
            Fx
               .If(() => { return true; }).And(() => { return false; }).Then(() => { result                  = 1; })
               .ElseIf(() => { return expected == 998; }).OrNot(() => { return false; }).Then(() => { result = 2; })
               .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_6_Long_Nesting_Evaluated(){
            var result   = 0;
            var expected = 1;
            Fx
               .If(() => { return true; }).And(() => { return true; }).Or(() => { return false; })
               .Xor(() => { return false; }).AndNot(() => { return false; }).Then(() => { result             = 1; })
               .ElseIf(() => { return expected == 998; }).OrNot(() => { return false; }).Then(() => { result = 2; })
               .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Loop_1_WhileTrue(){
            var result = 0;

            Fx.WhileTrue(() => {
                ++result;
                return result != 6;
            });
            Assert.Equal(6, result);
        }

        [Fact]
        public void Test_Loop_2_WhileFalse(){
            var result = 0;

            Fx.WhileFalse(() => {
                ++result;
                return result == 6;
            });
            Assert.Equal(6, result);
        }

        [Fact]
        public void Test_Loop_3_While_Do(){
            var result = 0;

            Fx.While(() => { return result < 6; }).Do(() => { ++result; });

            Assert.Equal(6, result);
        }

        [Fact]
        public void Test_Loop_4_While_Do_Early_Break(){
            var result                   = 0;
            var conditionEvaluationCount = 0;
            Fx.While(() => {
                ++conditionEvaluationCount;
                return result < 6;
            }).EarlyBreakOn(() => { return result == 4; }).Do(() => { ++result; });

            Assert.Equal(5, conditionEvaluationCount);
        }

        [Fact]
        public void Test_Loop_5_While_Do_Late_Break(){
            var result                   = 0;
            var conditionEvaluationCount = 0;

            Fx.While(() => {
                ++conditionEvaluationCount;
                return result < 6;
            }).LateBreakOn(() => { return result == 4; }).Do(() => { ++result; });

            Assert.Equal(4, conditionEvaluationCount);
        }

        [Fact]
        public void Test_Loop_6_Do_While(){
            var result = 0;

            Fx.Do(() => { ++result; }).While(() => { return result < 5; });

            Assert.Equal(5, result);
        }

        [Fact]
        public void Test_Try_1_Catch_NoException(){
            var exceptionOccured = false;

            Fx.Try(() => { }).Catch<Exception>(_ => { exceptionOccured = true; });

            Assert.False(exceptionOccured);
        }

        [Fact]
        public void Test_Try_2_Catch_CatchExcuted(){
            var exceptionOccured = false;

            Fx.Try(() => throw new NotImplementedException())
              .Catch<NotImplementedException>(_ => { exceptionOccured = true; });

            Assert.True(exceptionOccured);
        }

        [Fact]
        public void Test_Try_4_Catch_CatchExcuted_ByCorrectOrder(){
            var exceptionOccured = false;

            Fx.Try(() => { throw new NotImplementedException(); })
              .Catch<NotImplementedException, Exception>(_ => { exceptionOccured = true; }, _ => { });

            Assert.True(exceptionOccured);
        }

        [Fact]
        public void Test_Try_5_SwallowIf(){
            var result = 0;
            Fx.Try(() => {
                throw new NotImplementedException();
                //++result;
            }).SwallowIf<NotImplementedException>();

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_ForEach(){
            var result = 0;
            Fx.ForEach(new List<int>(){ 1, 2, 3, 4 }, current => { result += current; });

            Assert.Equal(10, result);
        }

        [Fact]
        public void Test_Is_WithSafeNull(){
            User? entity      = null;
            var   isIdMatched = Fx.Is(() => entity.Name == null);

            Assert.False(isIdMatched);
        }

        [Fact]
        public void Test_Switch_1_Types_NormalCase(){
            var result = -1;

            Fx.Switch<string>()
              .Case<int>().Execute(() => { result    = 1; })
              .Case<string>().Execute(() => { result = 2; })
              .Default(() => { result                = 0; });

            Assert.Equal(result, 2);
        }

        [Fact]
        public void Test_Switch_2_Types_Default(){
            var result = -1;
            Fx
               .Switch<short>()
               .Case<int>().Execute(() => { result    = 1; })
               .Case<string>().Execute(() => { result = 2; })
               .Default(() => { result                = 0; });

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Test_Switch_3_Instances_NormalCase(){
            var condition = "two";
            var result    = -1;

            Fx.Switch(condition)
              .Case("one").Execute(() => { result = 1; })
              .Case("two").Execute(() => { result = 2; })
              .Default(() => { result             = 0; });

            Assert.Equal(result, 2);
        }

        [Fact]
        public void Test_Switch_4_Instances_Default(){
            var condition = "three";
            var result    = -1;
            Fx
               .Switch(condition)
               .Case("one").Execute(() => { result = 1; })
               .Case("two").Execute(() => { result = 2; })
               .Default(() => { result             = 0; });

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Test_RetryOnFail(){
            var result = 0;

            Fx.RetryOnFail(() => {
                ++result;
                return result > 5;
            }, attemptSleepInMilliSeconds: 10);

            Assert.Equal(result, 3);
        }
    }
}
