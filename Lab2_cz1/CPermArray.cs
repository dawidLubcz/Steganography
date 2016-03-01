using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dawid
{
    public class CPixel : IComparable
    {
        public int m_iI_H;
        public int m_iJ_W;

        public CPixel(int a_iI_H, int a_iJ_W)
        {
            this.m_iI_H = a_iI_H;
            this.m_iJ_W = a_iJ_W;
        }

        public int CompareTo(object obj)
        {
            int iRet = 0;
            CPixel tmp = (CPixel)obj;

            if (this.m_iI_H > tmp.m_iI_H)
            {
                iRet = 1;
            }
            else if ((this.m_iI_H <= tmp.m_iI_H))
            {
                if (this.m_iJ_W > tmp.m_iJ_W)
                {
                    iRet = 1;
                }
                else if (this.m_iJ_W == tmp.m_iJ_W)
                {
                    iRet = 0;
                }
                else
                {
                    iRet = -1;
                }
            }
            return iRet;
        }
    }

    public class CPermArray
    {
        public struct sKey
        {
            public string m_strKey;
            public byte[] m_bHashedKey;
        }

        public class CItem : IComparable
        {
            public Int32 m_iSalt;
            public UInt64 m_iPos;

            public CItem(Int32 a_iSalt, UInt64 a_iPos)
            {
                this.m_iSalt = a_iSalt;
                this.m_iPos = a_iPos;
            }

            public int CompareTo(object obj)
            {
                int fRet = 0;
                CItem tmp = (CItem)obj;

                if (this.m_iPos > tmp.m_iPos)
                    fRet = 1;
                else if (this.m_iPos < tmp.m_iPos)
                    fRet = -1;
                return fRet;
            }
        }

        private Int64[] m_aiItems;
        private UInt32 m_uiDataSize;
        private UInt32 m_uiItemsCounter;
        private UInt64 m_uiImageSize;

        public CPermArray(UInt32 a_iDataSize, UInt64 a_iImageSize)
        {
            if (a_iDataSize > 0)
            {
                m_aiItems = new Int64[a_iDataSize];
                m_uiDataSize = a_iDataSize;
                m_uiItemsCounter = 0;
                m_uiImageSize = a_iImageSize;
            }
            else
                throw new System.ArgumentException("Wrong parameter value");
        }

        public bool generate(int a_iSalt)
        {
            bool fRet = false;
            m_uiItemsCounter = 0;

            Random _orRandom = new Random(a_iSalt);

            for (int i = 0; i < m_uiDataSize;)
            {                
                Int64 tmp = _orRandom.Next(32, (int)m_uiImageSize);

                if (false == itemExist(tmp))
                {
                    m_aiItems[i] = tmp;
                    ++i;
                    ++m_uiItemsCounter;
                }                
            }

            fRet = true;

            return fRet;
        }

        private bool itemExist(Int64 a_iItem)
        {
            bool fRes = false;

            for (int i = 0; i < m_uiItemsCounter; i++)
            {
                if (a_iItem == m_aiItems[i])
                {
                    fRes = true;
                }
            }
            return fRes;
        }

        byte[] Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                return sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }

        public Int64[] getArray()
        {
            return m_aiItems;
        }

        public Int64[] getSortedArray()
        {
            Array.Sort(m_aiItems);
            return m_aiItems;
        }

    }
}
