using System;
using System.IO;

namespace Cod_Clima
{
    class Codificador
    {
        
        public string pathCod{get;}
        public string pathTxt{get;}

        public Codificador(string path_Cod, string path_Txt) => (pathCod,pathTxt) = (path_Cod,path_Txt);

        public long cod()
        {
            //DateAndTime,Climate
            //5464687,465468

            int z = 0;
            string[] readText = File.ReadAllLines(pathTxt);

            foreach(string x in readText)
            {
                if(z>0)
                {
                   StreamWriter NewInfo = File.AppendText(pathCod);
                   NewInfo.WriteLine($"{cod_DateAndTime(x)},{cod_Climate(x)}");
                   NewInfo.Close();

                }
                else
                z++;
            }

            return 0;
        }

        public long cod_DateAndTime(string x)
        {
            //2020-07-13T19:30:25.525-04:00
            //int onlyInt = readOnlyInt(string x);
            
            var Div = x.Split('T'); //Se divide en dos por T
            var Div_Date = Div[0].Split('-'); //Dentro de T[0] se divide por -
            long vyear = read((long)Convert.ToDouble(Div_Date[0]),48,4095);
            long vmonth = read((long)Convert.ToDouble((Div_Date[1])),44,12);
            long vday = read((long)Convert.ToDouble((Div_Date[2])),39,31);

            var Div_Time = Div[1].Split('-','+'); //Dentro de T[1] se divide por - generando 2
            var Div_Time_1 = Div_Time[0].Split(':'); //Dentro de -[0] se divide por : generando de nuevo dos
                
            //2020-07-13T19:30:25.525-04:00
            
            
            long vhour = read((long)Convert.ToDouble((Div_Time_1[0])),34,23);
            long vminute = read((long)Convert.ToDouble((Div_Time_1[1])),28,59);
            long vsecond = read((long)Convert.ToDouble((Div_Time_1[2].Replace(".",""))),12,59999);
            var Div_Time_2 = Div_Time[1].Split(':');
            long vzhour = read((long)Convert.ToDouble(Div_Time_2[0]),7,23);

            long vsign=0;
            if(Div[1][12]=='+') 
            {
                vsign = 1;
            }
            
            
            long vfinalResult = vyear + vmonth + vday + vhour + vminute + vsecond + vzhour + vsign;
            return vfinalResult;
        }

        public int cod_Climate(string x)
        {
            //DateAndTime,MinTemp,MaxTemp,Precipitation
            //2020-07-13T19:30:25.525-04:00,25,34,30
           
            var Div = x.Split(",");
            int minTemp = Convert.ToInt32(read((long)Convert.ToDouble((Div[1])),14,100));

            int maxTemp = Convert.ToInt32(read((long)Convert.ToDouble((Div[2])),7,100));

            int probabilidad = Convert.ToInt32(read((long)Convert.ToDouble((Div[3])),0,100));
            
            int climateTotal = minTemp + maxTemp + probabilidad;
            return climateTotal;
        }

        public long read(long x, int pow, int parametro)
        {
            int i = 1;
            long result = 0;
            if (x<=parametro)
            {
                do {
                    if ((x&i)==i)
                    {
                        result+=(long)Math.Pow(2,pow);
                    }
                    pow++;
                    i*=2;
                }while(i<=x);
            }
            else
                Console.WriteLine("Numero ingresado no valido");

            return result;
        }

    }
}