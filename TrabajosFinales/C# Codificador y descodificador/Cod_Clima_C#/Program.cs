using System;

namespace Cod_Clima
{
    class Program
    {
        public static string pathCode = @"C:\Codes\Cod_Clima_C#\Cod";
        public static string pathTxt = @"C:\Codes\Cod_Clima_C#\pathTxt";
        public static string pathDescod = @"C:\Codes\Cod_Clima_C#\Descod";

        static void Main(string[] args)
        {
            // Codificador codclima = new Codificador(pathCode,pathTxt);
            // codclima.cod();

            Descodificador descodclima = new Descodificador(pathCode,pathDescod);
            descodclima.Descod();
        } 
    }
}
