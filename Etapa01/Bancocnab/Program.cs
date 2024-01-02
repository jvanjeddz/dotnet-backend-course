
class Program
{
    static void Main(string[] args)
    {
        int[] denominaciones = { 500, 200, 100, 50, 20, 10, 5, 1 };
        int[] retiros = new int[10];
        int numRetiros = 0;

        while (true)
        {
            Console.WriteLine("---------------------Bancocnab---------------------");
            Console.WriteLine("1. Ingresar cantidades de retiros hechos");
            Console.WriteLine("2. Revisar cantidad entregada de billetes y monedas");
            Console.WriteLine("3. Salir");
            Console.Write("Ingresar la opción: ");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        numRetiros = EnterAmounts(retiros);
                        break;
                    case 2:
                        numBilletesMonedas(retiros, numRetiros, denominaciones);
                        break;
                    case 3:
                        Console.WriteLine("Se agradece su preferencia. Hasta pronto ...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Introducir 1, 2 o 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opción no válida. Introducir 1, 2 o 3.");
            }

            Console.WriteLine("Presionar 'enter' para continuar ...");
            Console.ReadLine();
        }
    }

    static int EnterAmounts(int[] retiros)
    {
        Console.Write("Introducir la cantidad de retiros (1-10): ");
        if (int.TryParse(Console.ReadLine(), out int numRetiros) && numRetiros > 0 && numRetiros <= 10)
        {
            for (int i = 0; i < numRetiros; i++)
            {
                Console.Write($"Introducir la cantidad de dinero para el retiro {i + 1} (1-50000): ");
                if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0 && amount <= 50000)
                {
                    retiros[i] = amount;
                }
                else
                {
                    Console.WriteLine("Cantidad no válida. Debe ser un entero positivo entre 1 y 50000.");
                    i--;
                }
            }

            Console.WriteLine("Cantidades ingresadas correctamente.");
            return numRetiros;
        }
        else
        {
            Console.WriteLine("Número de retiros no válido. Debe ser un entero entre 1 y 10.");
            return 0;
        }
    }

    static void numBilletesMonedas(int[] retiros, int numRetiros, int[] denominaciones)
    {
        if (numRetiros == 0)
        {
            Console.WriteLine("Primero se deben ingresar las cantidades de dinero.");
            return;
        }

        for (int i = 0; i < numRetiros; i++)
        {
            int amount = retiros[i];
            Console.WriteLine($"Para el retiro {i + 1} de ${amount} MXN, se necesitan:");

            for (int j = 0; j < denominaciones.Length; j++)
            {
                int numDenominaciones = amount / denominaciones[j];
                if (numDenominaciones > 0)
                {
                    string tipoDenominacion = denominaciones[j] <= 10 ? "moneda(s)" : "billete(s)";
                    Console.WriteLine($"{numDenominaciones} {tipoDenominacion} de ${denominaciones[j]} MXN");
                    amount %= denominaciones[j];
                }
            }
        }
    }
}

