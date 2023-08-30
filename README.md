# PasswordStrengthChecker
Simple Password Strength Validator and Checker

## Usage Example

Add NuGet package: PasswordStrengthChecker to your Project, create a console Application (>= .NET 5.0)

    using System;
    using digitarenet;

    namespace ConsoleAppTest2
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Simple Password Strength Checker");
                Console.WriteLine("--------------------------------");


                Console.Write("Select Strength Level to Check (from 1=Crap to 5=ExtremelyStrong): ");
                var level = (Strength) (int.Parse(Console.ReadLine()));

                Console.WriteLine("Selected Level: " + level.ToString());
                var psc = new PasswordChecker(level);

                do
                {
                    Console.Write("Type a password or CTRL+C to exit: ");
                    var psw = Console.ReadLine();
                    Console.WriteLine("Strength Level: " + psc.CalculateStrength(psw));
                    Console.WriteLine("Password Validated: " + psc.ValidatePassword(psw));
                    Console.WriteLine();
                } while (true);        }
        }
    }
