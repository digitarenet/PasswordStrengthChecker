using System;
using System.Linq;

namespace digitarenet
{
    public class PasswordChecker : IDisposable
    {
          public bool ContainsAtLeastOneUppercase { get; private set; }
          public bool ContainsAtLeastOneLowercase { get; private set; }
          public bool ContainsAtLeastOneSpecialChar { get; private set; }
          public bool ContainsAtLeastOneDigit { get; private set; }
          public bool HasRequiredStrength { get; private set; }
          public bool HasRequiredLength { get; private set; }
          public bool PasswordIsValid { get; private set; }
          public Strength CalculatedStrength { get; private set; }

      
        private readonly Strength _minRequiredStrength;
        private readonly int _minStrongLength = 10;
        private readonly int _minRequiredLength = 8;
        private readonly bool _shouldContainsAtLeastOneUppercase;
        private readonly bool _shouldContainsAtLeastOneLowercase;
        private readonly bool _shouldContainsAtLeastOneSpecialChar;
        private readonly bool _shouldContainsAtLeastOneDigit;

        // Checker with default settings
        public PasswordChecker() : this(Strength._4_Strong,null,8,true,true,true)
        {
        }
        
        public PasswordChecker(Strength requiredStrength = Strength._4_Strong,
                               string password = null,
                               int requiredLength = 8,
                               bool shouldContainsUppercase = true,
                               bool shouldContainsLowercase = true,
                               bool shouldContainsDigit = true,
                               bool shouldContainsSpecial = true)
        {
            _shouldContainsAtLeastOneDigit = shouldContainsDigit;
            _shouldContainsAtLeastOneLowercase = shouldContainsLowercase;
            _shouldContainsAtLeastOneUppercase = shouldContainsUppercase;
            _shouldContainsAtLeastOneSpecialChar = shouldContainsSpecial;
            _minRequiredStrength = requiredStrength;
            _minRequiredLength = requiredLength;

            if (!string.IsNullOrEmpty(password))
                ValidatePassword(password);
        }

        /// <summary>
        /// Validate clause selected in constructor are all Passed
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>Return True if Length and 'Contains...' clause selected in constructor are all Passed</returns>
        public bool ValidatePassword(string password)
        {
            var s = CalculateStrength(password);
            
            PasswordIsValid = 
                   HasRequiredLength && 
                   // HasRequiredStrength && 
                   (_shouldContainsAtLeastOneDigit == ContainsAtLeastOneDigit) &&
                   (_shouldContainsAtLeastOneLowercase == ContainsAtLeastOneLowercase) &&
                   (_shouldContainsAtLeastOneUppercase == ContainsAtLeastOneUppercase) &&
                   (_shouldContainsAtLeastOneSpecialChar == ContainsAtLeastOneSpecialChar);

            return PasswordIsValid;
        }

        /// <summary>
        /// Calculate Strength Level for password
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>Return Strength enum Level (+1 if Length is greater than 10 chars)</returns>
        public Strength CalculateStrength(string password)
        {
            var score = 0;

            if (password.Any(char.IsUpper))
            {
                score++;
                ContainsAtLeastOneUppercase = true;
            }
            else ContainsAtLeastOneUppercase = false;

            if (password.Any(char.IsLower))
            {
                score++;
                ContainsAtLeastOneLowercase = true;
            }
            else ContainsAtLeastOneLowercase = false;
            
            if (password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                score++;
                ContainsAtLeastOneSpecialChar = true;
            }
            else ContainsAtLeastOneSpecialChar = false;
            
            if (password.Any(char.IsDigit))
            {
                score++;
                ContainsAtLeastOneDigit = true;
            }
            else ContainsAtLeastOneDigit = false;
            
            if (password.Length >= _minStrongLength)
                score++;
            // else
            //     score--;

            Strength passwordStrength;
            switch (score)
            {
                case 5:
                    passwordStrength = Strength._5_ExtremelyStrong;
                    break;
                case 4:
                    passwordStrength = Strength._4_Strong;
                    break;
                case 3:
                    passwordStrength = Strength._3_Medium;
                    break;
                case 2:
                    passwordStrength = Strength._2_Weak;
                    break;
                case 1:
                    passwordStrength = Strength._1_Crap;
                    break;
                default:
                    passwordStrength = Strength._0_NotAcceptable;
                    break;
            }

            HasRequiredStrength = passwordStrength >= _minRequiredStrength;
            HasRequiredLength = password.Length >= _minRequiredLength;
            
            return passwordStrength;
        }

        public void Dispose()
        {
            ContainsAtLeastOneDigit = false;
            ContainsAtLeastOneLowercase = false;
            ContainsAtLeastOneUppercase = false;
            ContainsAtLeastOneSpecialChar = false;

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}