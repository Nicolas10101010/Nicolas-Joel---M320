// See https://aka.ms/new-console-template for more information

int number;

Random rnd = new Random();
int randomNumber = rnd.Next(1, 100);

Console.WriteLine("Welcome to 'Guess the Number'!");

do { 
    Console.Write("Enter a number between 1 and 100: ");
    number = Convert.ToInt32(Console.ReadLine());
    if (number < randomNumber)
    {
        Console.WriteLine("Too low! Try again.");
    }
    else if (number > randomNumber)
    {
        Console.WriteLine("Too high! Try again.");
    }
    else
    {
        Console.WriteLine("Congratulations! You've guessed the number!");
    }
} while (number != randomNumber);