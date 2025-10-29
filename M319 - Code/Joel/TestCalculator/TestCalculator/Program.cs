// See https://aka.ms/new-console-template for more information
int num1;
int num2;
int num3 = 0;

Console.WriteLine("Willkommen beim Taschenrechner \n Geben Sie 2 Nummern ein \n Nummer 1:  ");
num1 = int.Parse(Console.ReadLine());
Console.WriteLine("Nummer 2: ");
num2 = int.Parse(Console.ReadLine());

Console.WriteLine("Welche Rechnung willst du ausführen: +/- ");
char parameter = Console.ReadLine()[0];

switch (parameter)
{
    case '+':
        {
            num3 = num1 + num2;
            Console.Write(num3);
            break;
        }
    case '-':
        {
            num3 = num1 - num2;
            Console.Write(num3);
            break;
        }
    default:
        {
            Console.WriteLine("Falsche Eingabe");
            return;
        }

}