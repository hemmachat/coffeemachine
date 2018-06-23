using Moq;
using System;
using System.Linq;
using Xunit;
using CoffeeMachine.Interface;
using CoffeeMachine;
using System.IO;

namespace CoffeeTest
{
    public class BaristaConsoleTest
    {

        public BaristaConsoleTest()
        {
        }

        [Fact]
        public void Small_Espresso_Coffee()
        {
            var input = CreateInputValue("y", "y", "0", "y", "y");
            var expected = CreateExpectValue("3.15");

            var actual = TestConsole(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Small_Espresso_Coffee_All_Caps()
        {
            var input = CreateInputValue("Y", "Y", "0", "Y", "Y");
            var expected = CreateExpectValue("3.15");

            var actual = TestConsole(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Small_Espresso_Coffee_Invalid_Size()
        {
            var input = CreateInputValue("a", "y", "0", "y", "y");
            var expected = CreateExpectValue("3.1");

            var actual = TestConsole(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Small_Espresso_Coffee_Invalid_Milk()
        {
            var input = CreateInputValue("y", "1", "0", "y", "y");
            var expected = CreateExpectValue("2.85");

            var actual = TestConsole(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Small_Espresso_Coffee_Invalid_Sugar()
        {
            var input = CreateInputValue("y", "y", "y", "y", "y");
            var expected = CreateExpectValue("3.15");

            var actual = TestConsole(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Small_Espresso_Coffee_Invalid_Foam()
        {
            var input = CreateInputValue("y", "y", "0", "1", "y");
            var expected = CreateExpectValue("3.1");

            var actual = TestConsole(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Small_Espresso_Coffee_Invalid_Sprinkle()
        {
            var input = CreateInputValue("y", "y", "0", "y", "1");
            var expected = CreateExpectValue("3.15");

            var actual = TestConsole(input);

            Assert.Equal(expected, actual);
        }

        private string TestConsole(string input)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(input))
                {
                    Console.SetIn(sr);
                    Program.Main(new string[] { "test" });

                    return sw.ToString();
                }
            }
        }

        private string CreateExpectValue(string cost)
        {
            return $"{Barista.SIZE_MESSAGE}{Environment.NewLine}{Barista.PROMPT_MESSAGE}" +
                        $"{Barista.MILK_MESSAGE}{Environment.NewLine}{Barista.PROMPT_MESSAGE}" +
                        $"{Barista.SUGAR_MESSAGE}{Environment.NewLine}{Barista.SUGAR_RANGE_MESSAGE}" +
                        $"{Barista.FOAM_MESSAGE}{Environment.NewLine}{Barista.PROMPT_MESSAGE}" +
                        $"{Barista.SPRINKLE_MESSAGE}{Environment.NewLine}{Barista.PROMPT_MESSAGE}" +
                        $"{Barista.COST_MESSAGE}{cost}{Environment.NewLine}";
        }

        private string CreateInputValue(string size, string milk, string sugar, string foam, string sprinkle)
        {
            return $"{size}{Environment.NewLine}" +
                $"{milk}{Environment.NewLine}" +
                $"{sugar}{Environment.NewLine}" +
                $"{foam}{Environment.NewLine}" +
                $"{sprinkle}{Environment.NewLine}";
        }
    }
}
