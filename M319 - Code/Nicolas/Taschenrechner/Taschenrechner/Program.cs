// See https://aka.ms/new-console-template for more information
var num1;
var num2;
var num3;
Console.WriteLine("Willkommen beim Taschenrechner \n Geben Sie 2 Nummern ein \n Nummer 1:  ");
num1 = int.Parse(Console.ReadLine());
Console.WriteLine("Nummer 2: ");
num2 = int.Parse(Console.ReadLine());

Console.WriteLine("Welche Rechnung willst du ausführen: +/- ");
char parameter = (char)Console.ReadLine();

switch (parameter)
{
    case '+':
        {
            num1 + num2 = num3;
            Console.Write(num3);
            break;
        }
    case '-':
        {
            num1 - num2 = num3;
            Console.Write(num3);
            break;
        }
    default:
        {
            Console.WriteLine("Falsche Eingabe");
            return;
        }

}