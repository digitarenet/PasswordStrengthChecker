using NUnit.Framework;

namespace digitarenet
{
    public class Test4Chars
    {
        private PasswordChecker _checker;
        private Strength strengthToCheck = Strength._4_Strong;
        
        [SetUp]
        public void Setup()
        { 
            _checker = new PasswordChecker(strengthToCheck,null,8,true,true, true,true);
        }

        [Test]
        public void Test_4char_lower()
        {
            var psw = "ciao";
            var valid= _checker.ValidatePassword(psw);

            if (valid) Assert.Fail();
            
            if (_checker.CalculatedStrength < strengthToCheck)
                Assert.Pass();
            else
                Assert.Fail();
        }
        
        [Test]
        public void Test_4char_lower_digit()
        {
            var psw = "cia1";
            var valid= _checker.ValidatePassword(psw);

            if (valid) Assert.Fail();
            
            if (_checker.CalculatedStrength < strengthToCheck)
                Assert.Pass();
            else
                Assert.Fail();
        }
        
        [Test]
        public void Test_4char_lower_upper_digit()
        {
            var psw = "Cia1";
            var valid= _checker.ValidatePassword(psw);

            if (valid) Assert.Fail();
            
            if (_checker.CalculatedStrength < strengthToCheck)
                Assert.Pass();
            else
                Assert.Fail();
        }
        
        [Test]
        public void Test_4char_lower_upper_digit_special()
        {
            var psw = "Ci1!";
            var valid= _checker.ValidatePassword(psw);

            if (valid) Assert.Fail();
            
            if (_checker.CalculatedStrength < strengthToCheck)
                Assert.Pass();
            else
                Assert.Fail();
        }
    }
}