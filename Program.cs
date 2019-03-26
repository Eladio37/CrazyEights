using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ProjectoFinal {
    class Program {
        public static string currencyCarta = "";
        static void Main (string[] args) {
            Console.Clear ();
            //1. Crear cartas
            bool[] cartas = new bool[52];

            //Crear los dos jugadores
            List<int> Jugador1 = new List<int> ();
            List<int> Jugador2 = new List<int> ();

            //2. Inicializar cartas
            for (int i = 0; i < 52; i++) {
                cartas[i] = false;
            }

            //3. Repartir cartas
            Repartir (Jugador1, cartas);
            Repartir (Jugador2, cartas);

            //Imprimir por pantalla las cartas
            Console.WriteLine ("\nLas cartas del Jugador 1 son: ");
            Imprimir (Jugador1);
            Console.WriteLine ("\nLas cartas del Jugador 2 son: ");
            Imprimir (Jugador2);

            Console.WriteLine ("\n Presione una tecla para continuar.");
            Console.ReadKey ();
            MostrarCarta (cartas);
        }

        public static void Repartir (List<int> Jugador, bool[] cartas) {
            Random random = new Random ();
            int newValue;
            for (int b = 0; b < 5; b++) {

                newValue = random.Next (0, 52);
                while (cartas[newValue] != false) {
                    newValue = random.Next (0, 52);
                }
                Jugador.Add (newValue);
                cartas[newValue] = true;

            }
        }
        public static void TomarCarta (List<int> Jugador, bool[] cartas) {

        }
        public static void Imprimir (List<int> Jugador) {
            int numero = 1;
            string carta = "";

            foreach (var Imprimir in Jugador) {
                carta = TextoCarta (Imprimir);
                Console.WriteLine (numero + ". " + carta);
                numero++;
            }
        }
        public static string TextoCarta (int valor) {
            int modulo;
            string carta = "", tipo = "";
            modulo = valor % 13;

            if (valor < 13) {
                tipo = "♥ ♥ ♥ ♥";
            } else if (valor < 26) {
                tipo = "♦ ♦ ♦ ♦";
            } else if (valor < 39) {
                tipo = "♣ ♣ ♣ ♣";
            } else {
                tipo = "♠️ ♠ ️♠ ️♠️";
            }

            if (modulo == 0) {
                carta = "A  " + tipo;
            } else if (modulo < 10) {
                if (modulo == 9) {
                    carta = (modulo + 1).ToString () + " " + tipo;
                } else {
                    carta = (modulo + 1).ToString () + "  " + tipo;
                }
            } else {
                switch (modulo) {
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
        public static void MostrarCarta (bool[] cartas) {
            //Console.Clear ();
            int contador = contarCartas (cartas);
            Random random = new Random ();
            int cartaActual = random.Next (0, 52);
            while (cartas[cartaActual] != false) {
                cartaActual = random.Next (0, 52);
            }
            currencyCarta = TextoCarta (cartaActual);
            Console.WriteLine ("\nEl maso contiene [{0}] cartas : Carta actual => {1}", contador, currencyCarta);
        }
        public static int contarCartas (bool[] cartas) {
            int contador = 0;
            for (int i = 0; i < 52; i++) {
                if (cartas[i] == false) {
                    contador++;
                }
            }
            return contador;

        }

    }
}