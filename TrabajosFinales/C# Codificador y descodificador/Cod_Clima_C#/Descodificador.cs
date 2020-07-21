using System;
using System.IO;

namespace Cod_Clima
{
    class Descodificador
    {
        public string pathCod{get;}
        public string pathDescod{get;}

        public Descodificador(string path_Cod, string path_Descod) => (pathCod,pathDescod) = (path_Cod,path_Descod);

        public long Descod()
        {
            int z = 0;
            string[] readText = File.ReadAllLines(pathCod);

            foreach(string x in readText)
            {
                if(z>0)
                {
                    var Div = x.Split(',');
                    StreamWriter NewInfo = File.AppendText(pathDescod);
                    NewInfo.WriteLine($"{Descod_DateAndTime(Div[0])},{Descod_Climate(Div[1])}");
                    NewInfo.Close();

                }
                else
                z++;
            }
            return 0;

        }

        public string Descod_DateAndTime(string x)
        {
            //2020-07-13T19:30:25.525-04:00,25,34,30
            int vyear = read(x,48,59);
            string vmonth = CorrectFormat(read(x,44,47));
            string vday = CorrectFormat(read(x,39,43));
            string vhour = CorrectFormat(read(x,34,38));
            string vminute = CorrectFormat(read(x,28,33));
            string vsecond = Div(CorrectFormat(read(x,12,27)));
            string vhourz = CorrectFormat(read(x,7, 11));
            string vsing = checkSing(x);
            string result = $"{vyear}-{vmonth}-{vday}T{vhour}:{vminute}:{vsecond}{vsing}{vhourz}:00";
            return result;
        }

        public string Descod_Climate(string x)
        {
            string minTemp = CorrectFormat(read(x,14,20));
            string maxTemp = CorrectFormat(read(x,7,13));
            string probabilidad = CorrectFormat(read(x,0,6));
            string result = $"{minTemp},{maxTemp},{probabilidad}";
            return result;
        
        }
        
        public string Div(string div)
        {
            //Coloca un punto 
            string y="";
            int z = 0;
            foreach(char i in div)
            {
                y+=i;
                if(z==1)
                {
                    y+='.'; 
                }
                z++;
            }
            return y;
        }
        public string checkSing(string check)
        {
            //verifica el signo de la zona horaria
            long check_1 = (long)Convert.ToDouble(check);
            if((check_1&1)==1)
            {
                return "+";
            }
            else 
                return "-";
        }
        public static string CorrectFormat(int read)
        {
            //Le agrega un cero a los numeros si son menores que 10
            string readR="";
            if(read<10)
            {
                readR = "0"+ read.ToString();
            }
            else
            {
                return read.ToString();
            }
            return readR;
        }

        public static int read(string x, int pow, int powLimit)
        {  
            //Se encarga de traducir el entero por los parametros definidos
            //long x sera el numero digitado
            long x_1 = (long)Convert.ToDouble(x);
            long result = 0;
            int y =0;
            int acumulador = 1;
                do 
                {
                    result=(long)Math.Pow(2,pow); // Se eleva pow que en el primer caso es 48
                    if ((x_1&result)==result) // Se compara x con result 
                    {
                        y+=acumulador;
                    }
                    pow++;
                    acumulador*=2;
                }while(pow<=powLimit);

            return y;
        }
    }
}
