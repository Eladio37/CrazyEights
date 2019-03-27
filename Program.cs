using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ProjectoFinal {
    class Program {
        public static string currentCarta = "";
        static void Main (string[] args) {
            Console.Clear ();
            //1. Crear cartas
            bool[] cartas = new bool[52];
            int currentPlayer = 1;
            int cardtoPlay = 0;
            bool finish = false;
            bool draw = false;
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

            //4. Carta Inicial
            MostrarCarta (cartas);

            //Turnos de jugadores
            while (finish != true || draw != true){
                Console.WriteLine ($"\n Le toca al Jugador {currentPlayer}. Las cartas del Jugador {currentPlayer} son: ");
            if (currentPlayer == 1)
            Imprimir (Jugador1);
            else if (currentPlayer == 2)
            Imprimir(Jugador2);

            Console.Write($"Jugador {currentPlayer}, elige una carta de tu mano para jugar (-1 para pasar): ");
            cardtoPlay = int.Parse(Console.ReadLine()) - 1;
            if (cardtoPlay == -1) {
            Console.WriteLine("Has cedido tu turno.");
            }
            else if (currentPlayer == 1)
            Jugador1.RemoveAt(cardtoPlay); 
            else if (currentPlayer == 2)
            Jugador2.RemoveAt(cardtoPlay); 

            Console.WriteLine($"Jugador {currentPlayer}, tus cartas ahora son: ");
            if (currentPlayer == 1){
            Imprimir (Jugador1);
            currentPlayer = 2;
            }
            else if (currentPlayer == 2){
            Imprimir(Jugador2);
            currentPlayer = 1;
            }
            

            }
            
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
            currentCarta = TextoCarta (cartaActual);
            Console.WriteLine ($"\n La carta actual en la mesa es: {currentCarta}. Restan [{contador}] en el maso.");
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