// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateTest.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// 何翔华
// Taf.Core.Test
// ValidateTest.cs

namespace Taf.Core.Test;

using System;

/// <summary>
/// $Summary$
/// </summary>
public class ValidateTest{
    [Fact]
    public void TestValidate2(){
        var u = new ValidateUser();
      var result=  new Taf.Core.Utility.Validator<ValidateUser>(u)
                  .AddRule(s=>s.Name?.StartsWith("hello")??false,"mmmmm",new []{"Name"})
         .Validate();
      Assert.False(result.IsValid);
      Assert.Equal(2,result.Count);
    }
}

public class ValidateUser{
    [Required(ErrorMessage = "xxxx")]
    public  string Name{ get; set; }
}