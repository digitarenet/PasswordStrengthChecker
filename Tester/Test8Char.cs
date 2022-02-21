using NUnit.Framework;

namespace digitarenet
{
    public class Test8Chars
    {
        private PasswordChecker _checker;
        private Strength strengthToCheck = Strength._4_Strong;
        
        [SetUp]
        public void Setup()
        { 
            _checker = new PasswordChecker(strengthToCheck,null,8,true,true, true,true);
        }

        [Test]
        public void Test_8char_lower()
        {
            var psw = "ciaociao";
            var valid= _checker.ValidatePassword(psw);

            if (valid) Assert.Fail();
            
            if (_checker.CalculatedStrength < strengthToCheck)
                Assert.Pass();
            else
                Assert.Fail();
        }
        
        [Test]
        public void Test_8char_lower_digit()
        {
            var psw = "ciaocia1";
            var valid= _checker.ValidatePassword(psw);

            if (valid) Assert.Fail();
            
            if (_checker.CalculatedStrength < strengthToCheck)
                Assert.Pass();
            else
                Assert.Fail();
        }
        
        [Test]
        public void Test_8char_lower_upper_digit()
        {
            var psw = "ciaociA1";
            var valid= _checker.ValidatePassword(psw);

            if (valid) Assert.Fail();
            
            if (_checker.CalculatedStrength < strengthToCheck)
                Assert.Pass();
            else
                Assert.Fail();
        }
        
        [Test]
        public void Test_8char_lower_upper_digit_special()
        {
            var psw = "ciaoCi1!";
            var valid= _checker.ValidatePassword(psw);

            if (valid) Assert.Pass();
            
            if (_checker.CalculatedStrength < strengthToCheck)
                Assert.Fail();
            else
                Assert.Pass();
        }
    }
}