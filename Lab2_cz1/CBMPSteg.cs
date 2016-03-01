using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dawid
{

    class CBMPSteg
    {
        public enum eParams
        {
            E_MSG_SIZE_OFFSET = sizeof(int) * 8
        }       

        public class CMessage
        {
            public string m_strMessage;
            public int m_iSize;

            public CMessage(string a_strMsg, int a_iSize)
            {
                m_strMessage = a_strMsg;
                m_iSize = a_iSize;
            }
        }

        public static CMessage readHiddenText(Bitmap a_aPicture, int a_iSaltRG)
        {
            CMessage _oRet = new CMessage("", 0);

            if (a_aPicture != null)
            {
                List<char> _oCharList = new List<char>();
                bool _fNoMoreWorkNeeded = false;
                int _iPixelStrSizeCounter = 0;
                int _iPixelCharBitCounter = 0;
                int _iCharCounter = 0;
                int _iStringSize = 0;
                int _iCharVal = 0;
                int _iDataBitCounter = 0;

                // get permutation array                
                CPermArray _oPermArray = null;//
                Int64[] _aiPermArray = new Int64[1];
                int _iPermArrayCounter = 0;

                for (int i = 0; i < a_aPicture.Height; i++)
                {
                    for (int j = 0; j < a_aPicture.Width; j++)
                    {
                        Color _oPixel = a_aPicture.GetPixel(j, i);

                        //Get only the LSB of each subpixel
                        int R = _oPixel.R & (1 << 0);
                        int G = _oPixel.G & (1 << 0);
                        int B = _oPixel.B & (1 << 0);

                        //obtain string length
                        if ((_iPixelStrSizeCounter) - sizeof(int) * 8 < 0)
                        {
                            if (R > 0)
                                _iStringSize += Convert.ToInt32(Math.Pow(2, _iPixelStrSizeCounter));
                            _iPixelStrSizeCounter++;
                            if (G > 0)
                                _iStringSize += Convert.ToInt32(Math.Pow(2, _iPixelStrSizeCounter));
                            _iPixelStrSizeCounter++;
                        }
                        // now read the string
                        else
                        {
                            if (null == _oPermArray)
                            {
                                _oPermArray = new CPermArray((UInt32)(_iStringSize * CHamming_8.m_iSizeAll), (UInt64)(a_aPicture.Height * a_aPicture.Width));
                                _oPermArray.generate(a_iSaltRG);
                                _aiPermArray = _oPermArray.getSortedArray();
                            }

                            if (_aiPermArray[_iPermArrayCounter] != _iDataBitCounter)
                            {
                                ++_iDataBitCounter;
                                continue;
                            }
                            else
                            {
                                if ((_iPermArrayCounter + 1) < (_iStringSize * CHamming_8.m_iSizeAll))
                                    ++_iPermArrayCounter;
                            }

                            if((R + B)>0)
                                _iCharVal += Convert.ToInt32(Math.Pow(2, _iPixelCharBitCounter));
                            _iPixelCharBitCounter++;
                            ++_iDataBitCounter;

                            //find the end of the coded char
                            if (_iPixelCharBitCounter % CHamming_8.m_iSizeAll == 0)
                            {
                                _iCharCounter++;
                                _iPixelCharBitCounter = 0;

                                // encode char
                                CHamming_8.sHammingRes sTmp;
                                sTmp.m_iResult = _iCharVal;
                                sTmp.m_iSize = CHamming_8.m_iSizeAll;
                                sTmp.m_iDataSize = 8;
                                _iCharVal = CHamming_8.getEncodedVal(sTmp);

                                // add to char list
                                _oCharList.Add((char)_iCharVal);
                                _iCharVal = 0;
                            }

                            if (_iCharCounter >= _iStringSize)
                            {
                                _fNoMoreWorkNeeded = true;
                                _oRet.m_strMessage = string.Join("", _oCharList.ToArray());
                                break;
                            }
                        }                        
                    }
                    if (_fNoMoreWorkNeeded) break;
                }
                _oRet.m_iSize = _iStringSize;
            }
            
            return _oRet;
        }

        public static Bitmap hideTxtInImg(Bitmap a_oPicture, string a_sTextToHide, int a_iSaltRG)
        {
            Bitmap _oBmp = null;

            if (a_sTextToHide != null && a_oPicture != null)
            {
                _oBmp = new Bitmap(a_oPicture);
                bool _fNoMoreWorkNeeded = false;
                int  _iPixelCounter = 0;                
                int  _iStringSize = a_sTextToHide.Length;
                int  _iStrByteIndex = 0;
                int  _iCharBitIndex = 0;
                int  _iDataBitCounter = 0;
                byte[] _abStrBytes = Encoding.ASCII.GetBytes(a_sTextToHide);

                // get chars from input string one by one
                CHamming_8.sHammingRes sCodedChar = CHamming_8.getCodedVal((int)a_sTextToHide[_iStrByteIndex]);

                // get permutation array                
                CPermArray _oPermArray = new CPermArray((UInt32)(a_sTextToHide.Length * CHamming_8.m_iSizeAll), (UInt64)(_oBmp.Height*_oBmp.Width));
                _oPermArray.generate(a_iSaltRG);                
                Int64[] _aiPermArray = _oPermArray.getSortedArray();                
                int _iPermArrayCounter = 0;
                
                int _iStrValue = sCodedChar.m_iResult;

                for (int i = 0; i < _oBmp.Height; i++)
                {
                    for (int j = 0; j < _oBmp.Width; j++)
                    {
                        Color _oPixel = _oBmp.GetPixel(j, i);

                        int R = _oPixel.R & ~(1 << 0);
                        int G = _oPixel.G & ~(1 << 0);
                        int B = _oPixel.B & ~(1 << 0);

                        //save string lenth
                        if ((_iPixelCounter) < sizeof(int) * 8)
                        {                            
                            R = R | Convert.ToInt32((_iStringSize & (1 << _iPixelCounter)) != 0);
                            _iPixelCounter++;
                            G = G | Convert.ToInt32((_iStringSize & (1 << _iPixelCounter)) != 0);                            
                        }
                        //string length is saved
                        else
                        {
                            if (_aiPermArray[_iPermArrayCounter] != _iDataBitCounter)
                            {
                                ++_iDataBitCounter;
                                continue;
                            }
                            else
                            {
                                if ( (_iPermArrayCounter + 1) < (a_sTextToHide.Length * CHamming_8.m_iSizeAll))
                                    ++_iPermArrayCounter;
                            }
                      
                            int X1 = _iStrValue % 2;
                            int tmp = _iStrValue / 2;
                            int X2 = tmp % 2;
                            int a1 = R % 2;
                            int a2 = G % 2;
                            int a3 = B % 2;

                            //parity method
                            if ((Convert.ToInt32(a1 != a3) == X1) && (Convert.ToInt32(a2 != a3) == X2))
                            {
                                // nothing to do
                            }
                            if ((Convert.ToInt32(a1 != a3) != X1) && (Convert.ToInt32(a2 != a3) == X2))
                            {
                                R = (R | (1 << 0));
                            }
                            else if ((Convert.ToInt32(a1 != a3) == X1) && (Convert.ToInt32(a2 != a3) != X2))
                            {
                                G = (G | (1 << 0));
                            }
                            else if ((Convert.ToInt32(a1 != a3) != X1) && (Convert.ToInt32(a2 != a3) != X2))
                            {
                                B = (B | (1 << 0));
                            }

                            _iStrValue = tmp;
                            ++_iCharBitIndex;
                            ++_iDataBitCounter;
                            if (_iCharBitIndex % sCodedChar.m_iSize == 0 )
                            {
                                ++_iStrByteIndex;
                                if (_iStrByteIndex < a_sTextToHide.Length)
                                {
                                    _iStrValue = a_sTextToHide[_iStrByteIndex];
                                    _iStrValue = CHamming_8.getCodedValInt(_iStrValue);
                                }
                            }
                        }

                        _oBmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                        ++_iPixelCounter;

                        if ((_iPixelCounter - (int)eParams.E_MSG_SIZE_OFFSET) >= CHamming_8.m_iSizeAll * sizeof(int) * a_sTextToHide.Length)
                        {
                            _fNoMoreWorkNeeded = true;
                            break;
                        }  
                    }
                    if (_fNoMoreWorkNeeded) break;
                }                
            }

            return _oBmp;
        }
    }
}
