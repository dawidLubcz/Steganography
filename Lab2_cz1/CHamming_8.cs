using System;

namespace Dawid
{
    class CHamming_8
    {
        public static int m_iSizeAll = 12;

        public struct sHammingRes
        {
            public int m_iResult;
            public int m_iSize;
            public int m_iDataSize;
        }

        public struct sBit
        {
            public int m_iVal;
            public int m_iPos;
        }

        enum eDataBitsPos
        {
            DATA_HAMMING_POS_3 = 0,
            DATA_HAMMING_POS_5,
            DATA_HAMMING_POS_6,
            DATA_HAMMING_POS_7,
            DATA_HAMMING_POS_9,
            DATA_HAMMING_POS_10,
            DATA_HAMMING_POS_11,
            DATA_HAMMING_POS_12
        }

        public static int getEncodedVal(sHammingRes a_oArg)
        {
            short _bitToCorr = 0;            
            sBit[] _oDataArray = new sBit[a_oArg.m_iDataSize];

            int _iP1 = (a_oArg.m_iResult & (1 << 0));
            int _iP2 = (a_oArg.m_iResult & (1 << 1));
            int _iP4 = (a_oArg.m_iResult & (1 << 3));
            int _iP8 = (a_oArg.m_iResult & (1 << 7));

            //sBit d3, d5, d6, d7, d9, d10, d11, d12;
            /*d3.m_iVal = ((a_oArg.m_iResult & (1 << 2)) >> 2); d3.m_iPos = 3;  _oDataList[0] = d3;
            d5.m_iVal = ((a_oArg.m_iResult & (1 << 4)) >> 3); d5.m_iPos = 5; _oDataList[1] = (d5);
            d6.m_iVal = ((a_oArg.m_iResult & (1 << 5)) >> 3); d6.m_iPos = 6; _oDataList[2] = (d6);
            d7.m_iVal = ((a_oArg.m_iResult & (1 << 6)) >> 3); d7.m_iPos = 7; _oDataList[3] = (d7);
            d9.m_iVal = ((a_oArg.m_iResult & (1 << 8)) >> 4); d9.m_iPos = 9; _oDataList[4] = (d9);
            d10.m_iVal = ((a_oArg.m_iResult & (1 << 9)) >> 4); d10.m_iPos = 10; _oDataList[5] = (d10);
            d11.m_iVal = ((a_oArg.m_iResult & (1 << 10)) >> 4); d11.m_iPos = 11; _oDataList[6] = (d11);
            d12.m_iVal = ((a_oArg.m_iResult & (1 << 11)) >> 4); d12.m_iPos = 12; _oDataList[7] = (d12);*/

            int powCounter = 0;
            int dataCounter = 0;
            for (int i = 0; i < a_oArg.m_iSize; i++)
            {
                if ((i+1) == (int)Math.Pow(2, powCounter))
                {
                    powCounter++;
                    continue;
                }
                else
                {
                    sBit tmp;
                    tmp.m_iVal = ((a_oArg.m_iResult & (1 << i)) >> powCounter);
                    tmp.m_iPos = i+1;
                    _oDataArray[dataCounter] = tmp;
                    dataCounter++;
                }
            }

            if ( (_iP1>0) != ( Convert.ToBoolean(_oDataArray[0].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[1].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[3].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[5].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[7].m_iVal)  )
               )
            {
                _bitToCorr += 1;
            }
            if ( (_iP2>0) != ( Convert.ToBoolean(_oDataArray[0].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[2].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[3].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[5].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[6].m_iVal)  )
                )
            {
                _bitToCorr += 2;
            }
            if ( (_iP4>0) != ( Convert.ToBoolean(_oDataArray[1].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[2].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[3].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[7].m_iVal)) )
            {
                _bitToCorr += 4;
            }
            if ( (_iP8>0) != ( Convert.ToBoolean(_oDataArray[4].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[5].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[6].m_iVal) != 
                               Convert.ToBoolean(_oDataArray[7].m_iVal)) )
            {
                _bitToCorr += 8;
            }

            if (0 < _bitToCorr)
            {
                for (int i = 0; i < a_oArg.m_iDataSize; i++)
                {
                    if(_oDataArray[i].m_iPos == _bitToCorr) 
                        _oDataArray[i].m_iVal ^= 1 << i;
                }
            }

            int res = 0;
            for (int i = 0; i < a_oArg.m_iDataSize; i++)
            {
                res |= _oDataArray[i].m_iVal;
            }
            
            return res;
        }

        public static sHammingRes getCodedVal(int a_iInput = 0)
        {
            sHammingRes sRes;
            sRes.m_iSize = m_iSizeAll;
            sRes.m_iDataSize = 8;

            int _iP1 = Convert.ToInt32( Convert.ToBoolean(a_iInput & (1 << 0)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 1)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 3)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 4)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 6)));
            int _iP2 = Convert.ToInt32( Convert.ToBoolean(a_iInput & (1 << 0)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 2)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 3)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 5)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 6)));
            int _iP4 = Convert.ToInt32( Convert.ToBoolean(a_iInput & (1 << 1)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 2)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 3)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 7)));
            int _iP8 = Convert.ToInt32( Convert.ToBoolean(a_iInput & (1 << 4)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 5)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 6)) != 
                                        Convert.ToBoolean(a_iInput & (1 << 7)));

            /*int powCounter = 0;
            for (int i = 0; i < sRes.m_iSize; i++)
            {
                if ((i + 1) == (int)Math.Pow(2, powCounter))
                {
                    
                    powCounter++;
                    continue;
                }
                else
                {
                    
                }
            }*/

            int d3 = (a_iInput & (1 << 0));
            int d5 = (a_iInput & (1 << 1));
            int d6 = (a_iInput & (1 << 2));
            int d7 = (a_iInput & (1 << 3));
            int d9 = (a_iInput & (1 << 4));
            int d10 = (a_iInput & (1 << 5));
            int d11 = (a_iInput & (1 << 6));
            int d12 = (a_iInput & (1 << 7));

            sRes.m_iResult =    (_iP1 << 0) | 
                                (_iP2 << 1) | 
                                ((a_iInput & (1 << 0)) << 2) | 
                                (_iP4 << 3) | 
                                ((a_iInput & (1 << 1)) << 3) | 
                                ((a_iInput & (1 << 2)) << 3) | 
                                ((a_iInput & (1 << 3)) << 3) | 
                                (_iP8 << 7) | 
                                ((a_iInput & (1 << 4)) << 4) | 
                                ((a_iInput & (1 << 5)) << 4) | 
                                ((a_iInput & (1 << 6)) << 4) | 
                                ((a_iInput & (1 << 7)) << 4);

            return sRes;
        }

        /*private short getParityBit(int a_iData, int a_iPos, int a_iSize)
        {
            int powCounter = 0;
            int offset = a_iPos - 1;
            for (int i = 0; i < a_iSize; i++)
            {
                if ((i + 1) == (int)Math.Pow(2, powCounter))
                {

                    powCounter++;
                    continue;
                }
                else
                {

                }

                if (offset == a_iPos)
                {
                    offset = 0;
                }
                else
                {

                    offset++;
                }
            }
        }*/
        public static int getCodedValInt(int a_iInput)
        {
            sHammingRes sRes = getCodedVal(a_iInput);

            return sRes.m_iResult;
        }
    }
}
