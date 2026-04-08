using Xunit;

namespace TestProject1
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsFour_WhenAddingTwoAndTwo()
        {
            int result = Calculator.Add(2, 2);

            Assert.Equal(4, result);
        }
    }
}
