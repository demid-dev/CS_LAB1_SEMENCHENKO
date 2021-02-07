using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CS_LAB1_SEMENCHENKO
{
    static class TextEncoder
    {
        public static char[] B64Encode(string path)
        {
            string text = File.ReadAllText(path, Encoding.UTF8);
            text = text.Replace("і", "i");
            text = text.Replace("І", "I");
            byte[] input = Encoding.Default.GetBytes(text);
            int inputLength = input.Length;
            int blocksQuantity = 0;
            int spaces;
            byte block1, block2, block3;
            byte temp, temp1, temp2, temp3, temp4;

            if ((inputLength % 3) == 0)
            {
                spaces = 0;
                blocksQuantity = inputLength / 3;
            }
            else
            {
                spaces = 3 - (inputLength % 3);
                blocksQuantity = (inputLength + spaces) / 3;
            }

            int lengthProcessed = inputLength + spaces;
            byte[] inputProcessed = new byte[lengthProcessed];

            for (int i = 0; i < inputProcessed.Length; i++)
            {
                if (i < inputLength)
                {
                    inputProcessed[i] = input[i];
                }
                else
                {
                    inputProcessed[i] = 0;
                }
            }

            byte[] tempResult = new byte[blocksQuantity * 4];
            char[] result = new char[blocksQuantity * 4];

            for (int i = 0; i < blocksQuantity; i++)
            {
                block1 = inputProcessed[i * 3];
                block2 = inputProcessed[i * 3 + 1];
                block3 = inputProcessed[i * 3 + 2];

                temp1 = (byte)((block1 & 252) >> 2);

                temp = (byte)((block1 & 3) << 4);
                temp2 = (byte)((block2 & 240) >> 4);
                temp2 += temp;

                temp = (byte)((block2 & 15) << 2);
                temp3 = (byte)((block3 & 192) >> 6);
                temp3 += temp;

                temp4 = (byte)(block3 & 63);

                tempResult[i * 4] = temp1;
                tempResult[i * 4 + 1] = temp2;
                tempResult[i * 4 + 2] = temp3;
                tempResult[i * 4 + 3] = temp4;
            }

            for (int i = 0; i < blocksQuantity * 4; i++)
            {
                result[i] = SixBitToChar(tempResult[i]);
            }

            switch (spaces)
            {
                case 0:
                    break;
                case 1:
                    result[blocksQuantity * 4 - 1] = '=';
                    break;
                case 2:
                    result[blocksQuantity * 4 - 1] = '=';
                    result[blocksQuantity * 4 - 2] = '=';
                    break;
                default:
                    break;
            }

            File.WriteAllText(path.Replace(".txt", "_encoded.txt"), new string(result));

            return result;
        }

        private static char SixBitToChar(byte b)
        {
            char[] enconding = new char[64] {
             'A','B','C','D','E','F','G','H','I','J','K','L','M',
             'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
             'a','b','c','d','e','f','g','h','i','j','k','l','m',
             'n','o','p','q','r','s','t','u','v','w','x','y','z',
             '0','1','2','3','4','5','6','7','8','9','+','/'
            };

            if ((b >= 0) && (b <= 63))
            {
                return enconding[b];
            }
            return ' ';
        }
    }
}

