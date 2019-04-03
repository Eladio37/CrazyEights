using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ProjectoFinal
{
    class Program
    {
        public static string currentCarta = "";
        public static string tipoCartaActual = "";
        public static int numeroCartaActual;
        static void Main(string[] args)
        {
            Console.Clear();
            //1. Crear cartas
            #region Variables
            bool[] cartas = new bool[52];
            int currentPlayer = 1;
            int cardtoPlay = 0;
            bool stockExhausted = false;
            #endregion
            //Crear los dos jugadores
            List<int> Jugador1 = new List<int>();
            List<int> Jugador2 = new List<int>();

            //2. Inicializar cartas en falso, asi de da a entender que no se han utilizado ninguna.
            for (int i = 0; i < 52; i++)
            {
                cartas[i] = false;
            }

            //3. Repartir cartas
            Repartir(Jugador1, cartas);
            Repartir(Jugador2, cartas);

            //3.1. Pone la primera carta aleatoria en la mesa.
            Random random = new Random();
            int cartaActual = random.Next(0, 52);
            while (cartas[cartaActual] != false)
            {
                cartaActual = random.Next(0, 52);
            }
            cartas[cartaActual] = true;
            int cartasRestantes = ContarCartas(cartas);

            while (true)
            {
                Console.Clear();
                //4. Este codigo muestra la carta actual que esta en la mesa.
                Console.WriteLine($"\n La carta actual en la mesa es: {TextoCarta(cartaActual)}. Restan [{cartasRestantes}] en el maso.");
                numeroCartaActual = ObtenerNumeroCarta(cartaActual);
                tipoCartaActual = ObtenerTipoCarta(cartaActual);
                //5. Determina cual jugador va actualmente, mediante la variable currentPlayer
                Console.WriteLine($"\n Le toca al Jugador {currentPlayer}. Las cartas del Jugador {currentPlayer} son: ");
                if (currentPlayer == 1)
                    Imprimir(Jugador1); // Si es jugador 1, imprime
                else if (currentPlayer == 2)
                    Imprimir(Jugador2); // Si es jugador 2, imprime

                playGame:
                Console.Write($"Jugador {currentPlayer}, elige una carta de tu mano para jugar (0 para tomar del maso): ");
                //Aqui se captura la carta que va a jugar, o si va a ceder turno (-1)
                // Se guarda en la variable cardtoPlay, para saber el index de la carta en la mano de cada jugador.
                // Para asi poder eliminarla de su mano mas tarde.
                cardtoPlay = int.Parse(Console.ReadLine()) - 1; // El -1 es porque los indices son en base a 0, y esto empieza en 1.
                #region TakeCard
                if (cardtoPlay == -1)
                {
                    if (currentPlayer == 1)
                    {
                        if (cartasRestantes > 0)
                        {
                            TomarCarta(Jugador1, cartas);
                            cartasRestantes--;
                            if (ObtenerNumeroCarta(Jugador1[Jugador1.Count - 1]) != numeroCartaActual && ObtenerTipoCarta(Jugador1[Jugador1.Count - 1]) != tipoCartaActual && ObtenerNumeroCarta(Jugador1[Jugador1.Count - 1]) != 8)
                            {
                                Console.WriteLine("\n Jugador 1, no has tenido suerte tomando cartas del maso, pierdes tu turno.");
                                Console.WriteLine("\n Presiona enter para continuar con el turno del Jugador 2");
                                Console.ReadKey();

                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay mas cartas en el maso!");
                            stockExhausted = true;
                            Console.ReadKey();
                        }
                        Console.Clear();

                    }
                    else if (currentPlayer == 2)
                    {
                        if (cartasRestantes > 0)
                        {
                            TomarCarta(Jugador2, cartas);
                            cartasRestantes--;
                            if (ObtenerNumeroCarta(Jugador2[Jugador2.Count - 1]) != numeroCartaActual && ObtenerTipoCarta(Jugador2[Jugador2.Count - 1]) != tipoCartaActual && ObtenerNumeroCarta(Jugador2[Jugador2.Count - 1]) != 8)
                            {
                                Console.WriteLine("\n Jugador 2, no has tenido suerte tomando cartas del maso, pierdes tu turno.");
                                Console.WriteLine("\n Presiona enter para continuar con el turno del Jugador 1");
                                Console.ReadKey();
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay mas cartas en el maso!");
                            stockExhausted = true;
                            Console.ReadKey();
                        }
                        Console.Clear();
                    }
                }
                #endregion
                //Aqui se determina que jugador es que se va a ejecutar el codigo.
                else if (currentPlayer == 1)
                {
                    //Este if es para ver si el tipo, o el numero coincide.
                    if (ObtenerNumeroCarta(Jugador1[cardtoPlay]) == numeroCartaActual || ObtenerTipoCarta(Jugador1[cardtoPlay]) == tipoCartaActual || ObtenerNumeroCarta(Jugador1[cardtoPlay]) == 8)
                    {
                        // Si todo coincide, se le quita la carta de la mano para simular que la jugo.
                        cartaActual = Jugador1[cardtoPlay];
                        Jugador1.RemoveAt(cardtoPlay);
                        if (Jugador1.Count == 0)
                        {
                            Console.WriteLine("El Jugador 1 ha ganado!");
                            return;
                        }
                    }
                    else
                    {
                        if (stockExhausted != false && Jugador1.Count == 1)
                        {
                            Console.WriteLine("El juego ha terminado empate!");
                            return;

                        }
                        else
                        {
                            Console.WriteLine("No puedes jugar esta carta!");
                            goto playGame;
                        }
                    }
                }

                else if (currentPlayer == 2)
                {
                    if (ObtenerNumeroCarta(Jugador2[cardtoPlay]) == numeroCartaActual || ObtenerTipoCarta(Jugador2[cardtoPlay]) == tipoCartaActual || ObtenerNumeroCarta(Jugador2[cardtoPlay]) == 8)
                    {
                        cartaActual = Jugador2[cardtoPlay];
                        Jugador2.RemoveAt(cardtoPlay);
                        if (Jugador2.Count == 0)
                        {
                            Console.WriteLine("El Jugador 2 ha ganado!");
                            return;
                        }
                    }
                    else
                    {
                        if (stockExhausted != false && Jugador2.Count == 1)
                        {
                            Console.WriteLine("El juego ha terminado empate!");
                            return;

                        }
                        else
                        {
                            Console.WriteLine("No puedes jugar esta carta!");
                            goto playGame;
                        }
                    }
                }

                if (currentPlayer == 1)
                {
                    currentPlayer = 2;
                }
                else if (currentPlayer == 2)
                {
                    currentPlayer = 1;
                }
                Console.Clear();
            }

        }
        public static void Repartir(List<int> Jugador, bool[] cartas)
        {
            Random random = new Random();
            int newValue;
            for (int b = 0; b < 5; b++)
            {
                newValue = random.Next(0, 52);
                while (cartas[newValue] != false)
                {
                    newValue = random.Next(0, 52);
                }
                Jugador.Add(newValue);
                cartas[newValue] = true;

            }
        }
        public static void TomarCarta(List<int> Jugador, bool[] cartas)
        {
            Random random = new Random();
            int newValue;
            newValue = random.Next(0, 52);
            while (cartas[newValue] != false)
            {
                newValue = random.Next(0, 52);
            }
            Jugador.Add(newValue);
            cartas[newValue] = true;

        }
        public static string ObtenerTipoCarta(int carta)
        {
            if (carta < 13)
            {
                return "Corazones";
            }
            else if (carta < 26)
            {
                return "Diamantes";
            }
            else if (carta < 39)
            {
                return "Trebol";
            }
            else if (carta < 52)
            {
                return "Picas";
            }
            else
                return null;
        }
        public static int ObtenerNumeroCarta(int carta)
        {
            int modulo;
            modulo = carta % 13;
            if (modulo == 0)
            {
                return 1;
            }
            else if (modulo < 10)
            {
                return modulo + 1;
            }
            else
            {
                switch (modulo)
                {
                    case 10:
                        return 11;
                    case 11:
                        return 12;
                    case 12:
                        return 13;
                }
            }
            return 0;
        }
        public static void Imprimir(List<int> Jugador)
        {
            int numero = 1;
            string carta = "";

            foreach (var imp in Jugador)
            {
                carta = TextoCarta(imp);
                Console.WriteLine(numero + ". " + carta);
                numero++;
            }
        }
        public static string TextoCarta(int valor)
        {
            int modulo;
            string carta = "", tipo = "";
            modulo = valor % 13;

            if (valor < 13)
            {
                tipo = "de Corazones";
            }
            else if (valor < 26)
            {
                tipo = "de Diamantes";
            }
            else if (valor < 39)
            {
                tipo = "de Treboles";
            }
            else
            {
                tipo = "de Picas";
            }

            if (modulo == 0)
            {
                carta = "A  " + tipo;
            }
            else if (modulo < 10)
            {
                if (modulo == 9)
                {
                    carta = (modulo + 1).ToString() + " " + tipo;
                }
                else
                {
                    carta = (modulo + 1).ToString() + "  " + tipo;
                }
            }
            else
            {
                switch (modulo)
                {
                    case 10:
                        carta = "J  " + tipo;
                        break;
                    case 11:
                        carta = "Q  " + tipo;
                        break;
                    case 12:
                        carta = "K  " + tipo;
                        break;
                }
            }
            return carta;
        }
        public static int ContarCartas(bool[] cartas)
        {
            int contador = 0;
            for (int i = 0; i < 52; i++)
            {
                if (cartas[i] == false)
                {
                    contador++;
                }
            }
            return contador;

        }
    }
}