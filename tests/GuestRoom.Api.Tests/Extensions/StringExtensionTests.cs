using FluentAssertions;
using GuestRoom.Api.Extensions;
using Xunit;

namespace GuestRoom.Api.Tests.Extensions
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("j.doe@email.com", "j....@emai...")]
        [InlineData("j.doeemail.com", "j.doeemail.com")]
        [InlineData("j.doe@e.de", "j....@e....")]
        [InlineData("j@email.com", "j...@emai...")]
        [InlineData("j.doe@", "j.doe@")]
        [InlineData("@email.com", "@email.com")]
        public void ToEmailForLogging_Splits_And_Obfuscates_Email_Slightly(string input, string expectedOutput)
        {
            var result = input.ToEmailForLogging();

            result.Should().Be(expectedOutput);
        }
    }
}