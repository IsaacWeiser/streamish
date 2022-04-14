using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streamish;
using Streamish.test;
using Streamish.Utils;
using Xunit;

namespace Streamish.test
{
    public class StringUtilsTests
    {
        [Fact]
        public void MakeExciting_Method_Makes_A_String_Exciting()
        {
            // Arrange - Create any variables, objects or resources needed to run the test
            var testString = "There's a spider on me";

            // Act - Run the code you want to test (a.k.a "System Under Test")
            var result = StringUtils.MakeExciting(testString);

            // Assert - Verify that the code from the "Act" step did what we expected
            Assert.Equal("THERE'S A SPIDER ON ME!!!", result);
        }
    }
}
