using System;

namespace AES_encrypt.Lib
{
    internal class AESCore
    {
        private byte[,] state = new byte[4, 4];
        private byte[,] expanKey;
        private byte[] planText;
        private byte[] CihperText;
        private byte[] key;
        

        public byte[] Encrypt128bit(byte[] planText, byte[] key)
        {
            this.planText = planText;
            this.key = key;
            if (this.key.Length != 16)
            {
                return null;
            }
            else
            {
                int length = this.planText.Length;
                if (this.planText.Length % 16 != 0) length = this.planText.Length + (16 - this.planText.Length % 16);
                byte[] result = new byte[length];
                KeyExpantion(4, 10);
                int index = 0;
                int resultIndex = 0;
                while (index < this.planText.Length) 
                {
                    for (int m = 0; m < 4; m++)
                    {
                        for (int n = 0; n < 4; n++)
                        {
                            if (index < this.planText.Length)
                            {
                                state[n, m] = this.planText[index];
                                index++;
                            }
                            else
                            {
                                state[n, m] = 0x00;
                            }
                        }
                    }
                    StateLog();
                    AddRoundKey(0);
                    StateLog();
                    for (int i = 1; i <= 9; i++)
                    {
                        Console.WriteLine("round "+i);
                        SubBytes();
                        StateLog();

                        ShiftRows();
                        StateLog();

                        MixColumns();
                        StateLog();

                        AddRoundKey(i * 4);
                        StateLog();
                    }
                    SubBytes();
                    ShiftRows();
                    AddRoundKey(40);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            result[resultIndex++] = state[j, i];
                        }
                    }
                }
                return result;
            }
        }

        public byte[] Decrypt128bit(byte[] CihperText, byte[] key)
        {
            this.CihperText = CihperText;
            this.key = key;
            if (this.key.Length != 16)
            {
                return null;
            }
            else
            {
                int length = this.CihperText.Length;
                if (this.CihperText.Length % 16 != 0) length = this.CihperText.Length + (16 - this.CihperText.Length % 16);
                byte[] result = new byte[length];
                KeyExpantion(4, 10);
                int index = 0;
                int resultIndex = 0;
                while (index < this.CihperText.Length)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        for (int n = 0; n < 4; n++)
                        {
                            if (index < CihperText.Length)
                            {
                                state[n, m] = this.CihperText[index];
                                index++;
                            }
                            else
                            {
                                state[n, m] = 0x00;
                            }
                        }
                    }
                    AddRoundKey(40);
                    StateLog();

                    for (int i = 9; i >= 1; i--)
                    {
                        Console.WriteLine("round" + i);
                        InvShiftRows();
                        StateLog();

                        InvSubBytes();
                        StateLog();

                        AddRoundKey(i * 4);
                        StateLog();

                        InvMixColumns();
                        StateLog();

                    }
                    InvShiftRows();
                    InvSubBytes();
                    AddRoundKey(0);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            result[resultIndex++] = state[j, i];
                        }
                    }
                }
                return result;
            }
        }

        public byte[] Encrypt192bit(byte[] planText, byte[] key)
        {
            this.planText = planText;
            this.key = key;
            if (this.key.Length != 24)
            {
                return null;
            }
            else
            {
                int length = this.planText.Length;
                if (this.planText.Length % 16 != 0) length = this.planText.Length + (16 - this.planText.Length % 16);
                byte[] result = new byte[length];
                KeyExpantion(6, 12);
                int index = 0;
                int resultIndex = 0;
                while (index < this.planText.Length)
                {
                    Console.WriteLine($"{index} {this.planText.Length}");
                    for (int m = 0; m < 4; m++)
                    {
                        for (int n = 0; n < 4; n++)
                        {
                            if (index < planText.Length)
                            {
                                state[n, m] = this.planText[index];
                                index++;
                            }
                            else
                            {
                                state[n, m] = 0x00;
                            }
                        }
                    }
                    AddRoundKey(0);
                    Console.WriteLine("Add round key \n");
                    StateLog();
                    for (int i = 1; i <= 11; i++)
                    {
                        Console.WriteLine("round"+i);
                        SubBytes();
                        Console.WriteLine("SubByte \n");
                        StateLog();

                        ShiftRows();
                        Console.WriteLine("shift \n");
                        StateLog();

                        MixColumns();
                        Console.WriteLine("mix \n");
                        StateLog();


                        AddRoundKey(i * 4);
                        Console.WriteLine("add \n");
                        StateLog();
                    }
                    Console.WriteLine("end \n");
                    SubBytes();
                    StateLog();

                    ShiftRows();
                    StateLog();

                    AddRoundKey(48);
                    StateLog();
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            result[resultIndex] = state[j, i];
                            resultIndex++;
                        }
                    }
                }
                return result;
            }
        }

        public byte[] Decrypt192bit(byte[] CihperText, byte[] key)
        {
            this.CihperText = CihperText;
            this.key = key;
            if (this.key.Length != 24)
            {
                return null;
            }
            else
            {
                int length = this.CihperText.Length;
                if (this.CihperText.Length % 16 != 0) length = this.CihperText.Length + (16 - this.CihperText.Length % 16);
                byte[] result = new byte[length];
                KeyExpantion(6, 12);
                int index = 0;
                int resultIndex = 0;
                while (index < this.CihperText.Length)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        for (int n = 0; n < 4; n++)
                        {
                            if (index < CihperText.Length)
                            {
                                state[n, m] = this.CihperText[index];
                                index++;
                            }
                            else
                            {
                                state[n, m] = 0x00;
                            }
                        }
                    }
                    AddRoundKey(48);
                    for (int i = 11; i >= 1; i--)
                    {
                        Console.WriteLine("roud " + i);
                        InvShiftRows();
                        Console.WriteLine("shif");
                        StateLog();

                        InvSubBytes();
                        Console.WriteLine("sub");
                        StateLog();

                        AddRoundKey(i * 4);
                        Console.WriteLine("add");
                        StateLog();

                        InvMixColumns();
                        Console.WriteLine("mix");
                        StateLog();
                    }
                    Console.WriteLine("end");
                    InvShiftRows();
                    InvSubBytes();
                    AddRoundKey(0);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            result[resultIndex++] = state[j, i];
                        }
                    }
                }
                return result;
            }
        }

        public byte[] Encrypt256bit(byte[] planText, byte[] key)
        {
            this.planText = planText;
            this.key = key;
            if (this.key.Length != 32)
            {
                return null;
            }
            else
            {
                int length = this.planText.Length;
                if (this.planText.Length % 16 != 0) length = this.planText.Length + (16 - this.planText.Length % 16);
                byte[] result = new byte[length];
                KeyExpantion(8, 14);
                int index = 0;
                int resultIndex = 0;
                while (index < this.planText.Length)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        for (int n = 0; n < 4; n++)
                        {
                            if (index < planText.Length)
                            {
                                state[n, m] = this.planText[index];
                                index++;
                            }
                            else
                            {
                                state[m, n] = 0x00;
                            }
                        }
                    }
                    AddRoundKey(0);
                    for (int i = 1; i <= 13; i++)
                    {
                        SubBytes();
                        ShiftRows();
                        MixColumns();
                        AddRoundKey(i * 4);
                    }
                    SubBytes();
                    ShiftRows();
                    AddRoundKey(56);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            result[resultIndex] = state[j, i];
                            resultIndex++;
                        }
                    }
                }
                return result;
            }
        }

        public byte[] Decrypt256bit(byte[] CihperText, byte[] key)
        {
            this.CihperText = CihperText;
            this.key = key;
            if (this.key.Length != 32)
            {
                return null;
            }
            else
            {
                int length = this.CihperText.Length;
                if (this.CihperText.Length % 16 != 0) length = this.CihperText.Length + (16 - this.CihperText.Length % 16);
                byte[] result = new byte[length];
                KeyExpantion(8, 14);
                int index = 0;
                int resultIndex = 0;
                while (index < this.CihperText.Length)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        for (int n = 0; n < 4; n++)
                        {
                            if (index < CihperText.Length)
                            {
                                state[n, m] = this.CihperText[index];
                                index++;
                            }
                            else
                            {
                                state[n, m] = 0x00;
                            }
                        }
                    }
                    AddRoundKey(56);
                    for (int i = 13; i >= 1; i--)
                    {
                        InvShiftRows();
                        InvSubBytes();
                        AddRoundKey(i * 4);
                        InvMixColumns();
                    }
                    InvShiftRows();
                    InvSubBytes();
                    AddRoundKey(0);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            result[resultIndex++] = state[j, i];
                        }
                    }
                }
                return result;
            }
        }

        private void SubBytes()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = SubByte(state[i, j]);
                }
            }
        }

        private void InvSubBytes()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = InvSubByte(state[i, j]);
                }
            }
        }

        private byte SubByte(byte alterByte)
        {
            int x = alterByte & 0xf;
            int y = (alterByte & 0xf0) >> 4;
            return Sbox.sbox[y, x];
        }

        private byte InvSubByte(byte alterByte)
        {
            int x = alterByte & 0xf;
            int y = (alterByte & 0xf0) >> 4;
            return Sbox.rsbox[y, x];
        }

        private void ShiftRows()
        {
            ShiftRow(1, 1);
            ShiftRow(2, 2);
            ShiftRow(3, 3);
        }

        private void InvShiftRows()
        {
            ShiftRow(1, 3);
            ShiftRow(2, 2);
            ShiftRow(3, 1);
        }

        /*
         * @param[in]
         *      int Nrow : Index of staterow are shifted
         *      int shift : number of round are shifted
         *
         * return : none
         */
        private void ShiftRow(int NRow, int shift)
        {
            for (int i = 1; i <= shift; i++)
            {
                byte temp = state[NRow, 0];
                state[NRow, 0] = state[NRow, 1];
                state[NRow, 1] = state[NRow, 2];
                state[NRow, 2] = state[NRow, 3];
                state[NRow, 3] = temp;
            }
            //StateLog();
        }

        private void MixColumns()
        {
            byte[] result = new byte[4];
            for (int c = 0; c < 4; c++)
            {
                result[0] = (byte)(GaloisMul.Mul2[state[0, c]] ^ GaloisMul.Mul3[state[1, c]] ^ state[2, c] ^ state[3, c]);
                result[1] = (byte)(state[0, c] ^ GaloisMul.Mul2[state[1, c]] ^ GaloisMul.Mul3[state[2, c]] ^ state[3, c]);
                result[2] = (byte)(state[0, c] ^ state[1, c] ^ GaloisMul.Mul2[state[2, c]] ^ GaloisMul.Mul3[state[3, c]]);
                result[3] = (byte)(GaloisMul.Mul3[state[0, c]] ^ state[1, c] ^ state[2, c] ^ GaloisMul.Mul2[state[3, c]]);
                state[0, c] = result[0];
                state[1, c] = result[1];
                state[2, c] = result[2];
                state[3, c] = result[3];
            }
        }

        private void InvMixColumns()
        {
            byte[] result = new byte[4];
            for (int c = 0; c < 4; c++)
            {
                result[0] = (byte)(GaloisMul.Mul14[state[0, c]] ^ GaloisMul.Mul11[state[1, c]] ^ GaloisMul.Mul13[state[2, c]] ^ GaloisMul.Mul9[state[3, c]]);
                result[1] = (byte)(GaloisMul.Mul9[state[0, c]] ^ GaloisMul.Mul14[state[1, c]] ^ GaloisMul.Mul11[state[2, c]] ^ GaloisMul.Mul13[state[3, c]]);
                result[2] = (byte)(GaloisMul.Mul13[state[0, c]] ^ GaloisMul.Mul9[state[1, c]] ^ GaloisMul.Mul14[state[2, c]] ^ GaloisMul.Mul11[state[3, c]]);
                result[3] = (byte)(GaloisMul.Mul11[state[0, c]] ^ GaloisMul.Mul13[state[1, c]] ^ GaloisMul.Mul9[state[2, c]] ^ GaloisMul.Mul14[state[3, c]]);
                state[0, c] = result[0];
                state[1, c] = result[1];
                state[2, c] = result[2];
                state[3, c] = result[3];
            }
        }

        private byte GMul(byte a, byte b)
        {
            byte p = 0;
            for (int counter = 0; counter < 8; counter++)
            {
                if ((b & 1) != 0)
                {
                    p ^= a;
                }
                bool hi_bit_set = (a & 0x80) != 0;
                a <<= 1;
                if (hi_bit_set)
                {
                    a ^= 0x1B;
                }
                b >>= 1;
            }
            return p;
        }

        private void AddRoundKey(int index)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] ^= expanKey[i, index + j];
                }
            }
        }

        private void KeyExpantion(int Nk, int Nr)
        {
            expanKey = new byte[4, 4 * (Nr + 1)];
            int i = 0;
            while (i < Nk)
            {
                expanKey[0, i] = key[i * 4];
                expanKey[1, i] = key[i * 4 + 1];
                expanKey[2, i] = key[i * 4 + 2];
                expanKey[3, i] = key[i * 4 + 3];
                i++;
            }
            while (i < 4 * (Nr + 1))
            {
                byte[] temp = new byte[4] { expanKey[0, i - 1], expanKey[1, i - 1], expanKey[2, i - 1], expanKey[3, i - 1] };
                if (i % Nk == 0)
                {
                    temp = RotWords(temp);
                    temp[0] = (byte)(SubByte(temp[0]) ^ Sbox.rcon[(i / Nk) - 1]);
                    temp[1] = SubByte(temp[1]);
                    temp[2] = SubByte(temp[2]);
                    temp[3] = SubByte(temp[3]);
                }
                else if (Nk > 6 && i % Nk == 4)
                {
                    temp[0] = SubByte(temp[0]);
                    temp[1] = SubByte(temp[1]);
                    temp[2] = SubByte(temp[2]);
                    temp[3] = SubByte(temp[3]);
                }
                expanKey[0, i] = (byte)(expanKey[0, i - Nk] ^ temp[0]);
                expanKey[1, i] = (byte)(expanKey[1, i - Nk] ^ temp[1]);
                expanKey[2, i] = (byte)(expanKey[2, i - Nk] ^ temp[2]);
                expanKey[3, i] = (byte)(expanKey[3, i - Nk] ^ temp[3]);
                i++;
            }
            ExpanLog(Nr);
            return;
        }
        private byte[] RotWords(byte[] array)
        {
            if (array.Length != 4)
            {
                throw new Exception("RotWords error !");
            }
            byte temp = array[0];
            array[0] = array[1];
            array[1] = array[2];
            array[2] = array[3];
            array[3] = temp;
            return array;
        }


        private void StateLog()
        {
            Console.WriteLine("");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write($"{state[i, j],3:x}");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        private void ExpanLog(int Nr)
        {
            Console.WriteLine("");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < (Nr + 1) * 4; j++)
                {
                    Console.Write($"{expanKey[i, j],3:x}");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
    }
}
