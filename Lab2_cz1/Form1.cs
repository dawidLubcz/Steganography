using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dawid;

namespace Lab2_cz2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxKeyStegon.Text = "Dawid Lubczynski I2n-24";

            CPermArray Arr = new CPermArray(10, 100);
            bool k = Arr.generate(50);
            Int64[] arr = Arr.getArray();
        }

        private void bLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog _oLoad = new OpenFileDialog();
            _oLoad.Filter = "Bitmaps (.bmp)|*.bmp";
            _oLoad.FilterIndex = 1;
            _oLoad.Multiselect = false;

            DialogResult clickedOK = _oLoad.ShowDialog();
            if (clickedOK == DialogResult.OK)
            {
                pictureLoaded.Load(_oLoad.FileName);
                //Bitmap tmp = CBitmapExt.shuffleBitmap((Bitmap)pictureLoaded.Image);
                //pictureLoaded.Image = tmp;
            }
        }

        private int getSalt()
        {
            int iRet = 0;

            try
            {
                iRet = Convert.ToInt32(textBoxKeyStegon.Text);
            }
            catch (Exception ex)
            { }

            return iRet;
        }

        private void bInsert_Click(object sender, EventArgs e)
        {
            if (textBoxStringToHide.Text.Length > 0 && pictureLoaded.Image != null)
            {
                string _strDecrypted = null;
                CAES _oCrypto = new CAES();

                if (textBoxAESKey.Text.Length == 0)
                    _strDecrypted = _oCrypto.encrypt(textBoxStringToHide.Text);
                else
                    _strDecrypted = _oCrypto.encrypt(textBoxStringToHide.Text, textBoxAESKey.Text);


                Bitmap _editedBitmap = CBMPSteg.hideTxtInImg((Bitmap)pictureLoaded.Image, _strDecrypted, getSalt());
                if (null != _editedBitmap)
                {
                    pictureEdited.Image = _editedBitmap;
                }

            }
            else if (pictureLoaded.Image == null)
            { MessageBox.Show("Wybierz obrazek!"); }
            else
            { MessageBox.Show("Wprowadz text"); }
            
        }

        private void bReadMes_Click(object sender, EventArgs e)
        {
            if (pictureLoaded.Image != null)
            {
                CBMPSteg.CMessage msg = CBMPSteg.readHiddenText((Bitmap)pictureLoaded.Image, getSalt());

                string _strEncrypted = null;
                CAES _oCrypto = new CAES();

                if (textBoxAESKey.Text.Length == 0)
                    _strEncrypted = _oCrypto.decrypt(msg.m_strMessage);
                else
                    _strEncrypted = _oCrypto.decrypt(msg.m_strMessage, textBoxAESKey.Text);

                textBoxStringToHide.Text = _strEncrypted;
            }
            else
            {
                MessageBox.Show("Wybierz obrazek!");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void bSaveImg_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Bitmaps (.bmp)|*.bmp"; ;
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != "")
                {
                    System.IO.FileStream _sFs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    if (pictureEdited.Image != null)
                        pictureEdited.Image.Save(_sFs, System.Drawing.Imaging.ImageFormat.Bmp);
                    _sFs.Close();
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            pictureEdited.Image = null;
            pictureLoaded.Image = null;
            textBoxKeyStegon.Text = null;
            textBoxAESKey.Text = null;
            textBoxStringToHide.Text = null;
        }
    }
}
