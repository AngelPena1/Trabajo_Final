def codClima(x):
    Div = x.split('T')
    Div_Date = Div[0].split('-')
    vyear = read(Div_Date[0],48,4095)
    vmonth = read(Div_Date[1],44,12) 
    vday = read(Div_Date[2],39,31)
    Div_Time = Div[1].split('-')
    Div_Time_1 = Div_Time[0].split(':')
    vhour = read(Div_Time_1[0],34,23)
    vminute = read(Div_Time_1[1],28,59)
    vsecond = read(Div_Time_1[2].replace(".",""),12,59999)
    Div_Time_2 = Div_Time[1].split(':')
    
    vzhour = read(Div_Time_2[0],7,23)
    vsign=0
    if Div[1][12]=='+': 
        vsign = 1
    finalResult = vyear + vmonth + vday + vhour + vminute + vsecond + vzhour + vsign
    return finalResult

def read(x,pow1,parametro):
    i = 1
    result = 0
    x = int(x)
    if x<=parametro:
        while True:
            if ((x&i)==i):
                result+=pow(2,pow1)
            pow1+=1
            i*=2
            if(i>=x):
                break
        
    else: 
        print("parametro no valido")
    
    return result

def cod_Climate(x):
    Div = x.split(",")
    minTemp = read(Div[1],14,100)
    maxTemp = read(Div[2],7,100)
    probabilidad = read(Div[3],0,100)
    climateTotal = minTemp + maxTemp + probabilidad
    return climateTotal

pathTxt = open("pathTxt","r")
codTxt = open("DatosCodificados","a")
for linea in pathTxt.readlines():
    print (linea)
    codTxt.write(str(codClima(linea)))
    codTxt.write(",")
    codTxt.write(str(cod_Climate(linea)))
    codTxt.write("\n")
pathTxt.close() 


        