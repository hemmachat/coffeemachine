using Moq;
using System;
using System.Linq;
using Xunit;
using CoffeeMachine.Interface;
using CoffeeMachine;
using System.IO;

namespace CoffeeTest
{
    public class BaristaCalculateCostTest
    {
        private readonly Mock<ICoffee> _coffee;
        private readonly Mock<IInputOutput> _io;
        private readonly IBarista _barista;

        public BaristaCalculateCostTest()
        {
            var factory = new MockRepository(MockBehavior.Loose);
            _coffee = factory.Create<ICoffee>();
            _io = factory.Create<IInputOutput>();
            _barista = new Barista(_io.Object, _coffee.Object);
        }

        [Fact]
        public void Default_Coffee()
        {
            var cost = _barista.CalculateCost();

            Assert.Equal(2.8, cost, 2);
        }

        [Fact]
        public void Small_Espresso_Coffee()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(false);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);
            var cost = _barista.CalculateCost();

            Assert.Equal(2.8, cost, 2);
        }

        [Fact]
        public void Large_Espresso_Coffee()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(false);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(2.8, cost, 2);
        }

        // with sprinkle
        // zero sugar cases
        [Fact]
        public void Large_Milk_Zero_Foam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.15, cost, 2);
        }

        [Fact]
        public void Large_Milk_Zero_NoFoam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.1, cost, 2);
        }

        [Fact]
        public void Small_Milk_Zero_Foam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.1, cost, 2);
        }

        [Fact]
        public void Small_Milk_Zero_NoFoam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.05, cost, 2);
        }

        // one sugar
        [Fact]
        public void Large_Milk_One_Foam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(1);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.25, cost, 2);
        }

        [Fact]
        public void Large_Milk_One_NoFoam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(1);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.2, cost, 2);
        }

        [Fact]
        public void Small_Milk_One_Foam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(1);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.2, cost, 2);
        }

        [Fact]
        public void Small_Milk_One_NoFoam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(1);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.15, cost, 2);
        }

        // two sugar
        [Fact]
        public void Large_Milk_Two_Foam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(2);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.35, cost, 2);
        }

        [Fact]
        public void Large_Milk_Two_NoFoam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(2);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.3, cost, 2);
        }

        [Fact]
        public void Small_Milk_Two_Foam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(2);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.3, cost, 2);
        }

        [Fact]
        public void Small_Milk_Two_NoFoam()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(2);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(true);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.25, cost, 2);
        }

        // without sprinkle
        // zero sugar cases
        [Fact]
        public void Large_Milk_Zero_Foam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.15, cost, 2);
        }

        [Fact]
        public void Large_Milk_Zero_NoFoam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.1, cost, 2);
        }

        [Fact]
        public void Small_Milk_Zero_Foam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.1, cost, 2);
        }

        [Fact]
        public void Small_Milk_Zero_NoFoam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(0);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.05, cost, 2);
        }

        // one sugar
        [Fact]
        public void Large_Milk_One_Foam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(1);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.25, cost, 2);
        }

        [Fact]
        public void Large_Milk_One_NoFoam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(1);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.2, cost, 2);
        }

        [Fact]
        public void Small_Milk_One_Foam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(1);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.2, cost, 2);
        }

        [Fact]
        public void Small_Milk_One_NoFoam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(1);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.15, cost, 2);
        }

        // two sugar
        [Fact]
        public void Large_Milk_Two_Foam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(2);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.35, cost, 2);
        }

        [Fact]
        public void Large_Milk_Two_NoFoam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(true);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(2);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.3, cost, 2);
        }

        [Fact]
        public void Small_Milk_Two_Foam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(2);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(true);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.3, cost, 2);
        }

        [Fact]
        public void Small_Milk_Two_NoFoam_NoSprinkle()
        {
            _coffee.Setup(_ => _.IsLarge).Returns(false);
            _coffee.Setup(_ => _.HasMilk).Returns(true);
            _coffee.Setup(_ => _.NumberOfSugar).Returns(2);
            _coffee.Setup(_ => _.MilkIsFoamed).Returns(false);
            _coffee.Setup(_ => _.HasSprinkles).Returns(false);

            var cost = _barista.CalculateCost();

            Assert.Equal(3.25, cost, 2);
        }
    }
}
