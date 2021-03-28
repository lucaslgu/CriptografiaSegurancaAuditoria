﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CriptografiaSegurancaAuditoria.CriptografiaDescriptografia
{
    class Algoritmo
    {
        public string Criptografia(string msgOriginal)
        {
            string msgComChavePorLetra = msgOriginal;
            string msgCriptografada = "";
            bool bloqueio = false;
            Random randNum = new Random();

            for (int i = 0; i < msgComChavePorLetra.Length; i++)
            {
                if (i > 0 && !bloqueio)
                {
                    for (int x = 1; x < msgComChavePorLetra.Length; x++)
                    {
                        msgCriptografada += RetornoLetras(Convert.ToChar(msgComChavePorLetra[x]), randNum.Next(0, 1));
                        i++;

                        if (x == msgComChavePorLetra.Length - 1)
                        {
                            bloqueio = true;
                        }
                    }
                }
                else if (!bloqueio)
                {
                    msgCriptografada += RetornoLetras(Convert.ToChar(msgComChavePorLetra[i]), randNum.Next(99));
                }
            }

            return msgCriptografada;
        }

        public string Descriptografia(string msgCriptografia)
        {
            //Ele vai armazenar cada letra em hexadecimal dentro do array
            string[] msgCriptografadaPorLetra = new string[msgCriptografia.Length / 4];
            int x = 0;
            string msgOriginal = "";

            for (int i = 0; i < msgCriptografadaPorLetra.Length; i++)
            {
                if (i < msgCriptografadaPorLetra.Length + 4)
                {

                    msgCriptografadaPorLetra[i] = msgCriptografia.Substring(x, 4);
                    x += 4;
                }
                else
                {
                    msgCriptografadaPorLetra[i] = msgCriptografia.Substring(i);
                }
            }

            int countBloco = 0;
            for (int i = 0; i < msgCriptografadaPorLetra.Length; i++)
            {
                if (countBloco < msgCriptografadaPorLetra.Length - 3)
                {
                    if (msgCriptografadaPorLetra[countBloco].Split('x')[0] == "0")
                    {
                        if (msgCriptografadaPorLetra[countBloco + 2].Split('x')[0] == "0")
                        {
                            msgOriginal += DesembaralharLetras(msgCriptografadaPorLetra[countBloco + 2], 0);
                        }
                        if (msgCriptografadaPorLetra[countBloco + 2].Split('x')[0] == "1")
                        {
                            msgOriginal += DesembaralharLetras(msgCriptografadaPorLetra[countBloco + 2], 1);
                        }
                        if (msgCriptografadaPorLetra[countBloco + 2].Split('x')[0] == "2")
                        {
                            msgOriginal += DesembaralharLetras(msgCriptografadaPorLetra[countBloco + 2], 2);
                        }
                        countBloco += 5;
                    }
                }
                if (countBloco < msgCriptografadaPorLetra.Length - 5)
                {
                    if (msgCriptografadaPorLetra[countBloco].Split('x')[0] == "1")
                    {
                        if (msgCriptografadaPorLetra[countBloco + 3].Split('x')[0] == "0")
                        {
                            msgOriginal += DesembaralharLetras(msgCriptografadaPorLetra[countBloco + 3], 0);
                        }
                        if (msgCriptografadaPorLetra[countBloco + 3].Split('x')[0] == "1")
                        {
                            msgOriginal += DesembaralharLetras(msgCriptografadaPorLetra[countBloco + 3], 1);
                        }
                        if (msgCriptografadaPorLetra[countBloco + 3].Split('x')[0] == "2")
                        {
                            msgOriginal += DesembaralharLetras(msgCriptografadaPorLetra[countBloco + 3], 2);
                        }
                        countBloco += 6;
                    }
                }
            }

            return msgOriginal;
        }

        public string DesembaralharLetras(string caractereHex, int qualCondicao)
        {
            string convertDecimalParaChar = Encoding.ASCII.GetString(new byte[] { Convert.ToByte(HexParaDec(caractereHex, qualCondicao)) });
            string convertCharParaString = convertDecimalParaChar.ToString();

            return convertCharParaString;
        }

        public string RetornoLetras(char letraComChavePorLetra, int varianteTamanho)
        {
            string retornoCaractere = "";
            Random randNum = new Random();
            int min = 30;
            int max = 99;

            //Vai ser add 4 caracteres, os caracteres responsaveis é os 4 primeiros números divisiveis do valor max da condição + 129
            //Sendo dois dos caracteres antes do caracter criptografado e dois após a inserção do caracter criptografado
            //Caso aconteceça de letras se repetirem, ele muda o padrão --- a letra criptografada passa a ficar como antipenultima e é add
            //mais uma letra em formato hexadecimal add no final da condição, sendo a mesma, o mínimo da função + o max da função dividido por 2
            //o zero do hex, vai ser substituido por um numero sequencial de ordem 0 a 2 repetindo esse ciclo
            // Ex.: 64/2 = 32 --- 32/2 = 16 --- 16/2 = 8 --- 8/2 = 4
            // + 129

            //trocar esse retorno fixo de caracteres

            if (varianteTamanho == 0)
            {
                retornoCaractere += "0" + "x" + randNum.Next(min, max).ToString("X");
                retornoCaractere += randNum.Next(9).ToString("X") + "x" + randNum.Next(min, max).ToString("X");
                retornoCaractere += EmbaralharCaracteres(Convert.ToInt32(letraComChavePorLetra), randNum.Next(0, 2));
                retornoCaractere += randNum.Next(9).ToString("X") + "x" + randNum.Next(min, max).ToString("X");
                retornoCaractere += randNum.Next(9).ToString("X") + "x" + randNum.Next(min, max).ToString("X");
            }
            else
            {
                retornoCaractere += "1" + "x" + randNum.Next(min, max).ToString("X");
                retornoCaractere += randNum.Next(9).ToString("X") + "x" + randNum.Next(min, max).ToString("X");
                retornoCaractere += randNum.Next(9).ToString("X") + "x" + randNum.Next(min, max).ToString("X");
                retornoCaractere += EmbaralharCaracteres(Convert.ToInt32(letraComChavePorLetra), randNum.Next(0, 2));
                retornoCaractere += randNum.Next(9).ToString("X") + "x" + randNum.Next(min, max).ToString("X");
                retornoCaractere += randNum.Next(9).ToString("X") + "x" + randNum.Next(min, max).ToString("X");
            }
            return retornoCaractere;
        }

        //Ele soma o valor da letra + o valor da letra sucessora de acordo com a tabela ASCII - Até o 64
        //Ele soma o valor da letra + o valor da letra antecessora da antecessora - 64 de acordo com a tabela ASCII - Até o 96
        //Ele soma o valor da letra + o valor da letra sucessora da sucessora - 64 de acordo com a tabela ASCII - Até o 128
        //O valor que fica na criptografia é hexadecimal
        public string EmbaralharCaracteres(int NumeroletraComChave, int numeroCondicao)
        {
            string hexValue = "";

            if (numeroCondicao == 0)
            {
                int embaralha = NumeroletraComChave + NumeroletraComChave + 1;
                hexValue = numeroCondicao + "x" + embaralha.ToString("X");
            }
            else if (numeroCondicao == 1)
            {
                int embaralha = NumeroletraComChave + NumeroletraComChave - 2 - 64;
                hexValue = numeroCondicao + "x" + embaralha.ToString("X");
            }
            else if (numeroCondicao == 2)
            {
                int embaralha = NumeroletraComChave + NumeroletraComChave + 2 - 64;
                hexValue = numeroCondicao + "x" + embaralha.ToString("X");
            }

            return hexValue;
        }

        //x = (y - 1) / 2 --- O resultado tem que ser até 64 e maior 32, caso seja maior,
        //vai para próxima condição, sendo y o valor hexadecimal criptografado
        //x = y/2 + 33 - O resultado tem que ser até 96 e maior que 64
        //x = y/2 + 31 - O resultado tem que ser até 128 e maior que 96
        public decimal HexParaDec(string caractereHex, int qualCondicao)
        {
            caractereHex = caractereHex.Substring(2, 2);
            int hexParaDec = Convert.ToInt32(caractereHex, 16);
            if (qualCondicao == 1)
            {
                float x = (hexParaDec + 66) / 2;
                return Convert.ToDecimal(x);
            }
            else if (qualCondicao == 2)
            {
                float x = (hexParaDec + 62) / 2;
                return Convert.ToDecimal(x);
            }
            else
            {
                float x = (hexParaDec - 1) / 2;
                return Convert.ToDecimal(x);
            }
        }
    }
}
