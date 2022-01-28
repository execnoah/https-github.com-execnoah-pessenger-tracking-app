using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bilet_satıs_uygulaması
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cmbotobus_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbotobus.Text)
            {
                case "Travego":KoltukDoldur(8, false);
                    break;
                case "Setra":KoltukDoldur(12, true);
                    break;
                case "Neoplan":KoltukDoldur(10, false);
                    break;
                    //ERDOGAN HOCAMA SELAMLAR <3

            }
            void KoltukDoldur(int sira, bool arkaBesliMi)
            {
                yavaslat:
                foreach (Control ctrl in this.Controls)
                {
                    if(ctrl is Button)
                    {
                        Button btn = ctrl as Button;
                        if(btn.Text=="Kaydet")
                        {
                            continue;
                        }
                        else
                        {
                            this.Controls.Remove(ctrl);
                            goto yavaslat;
                        }
                    }
                }
                int KoltukNo = 1;
                for(int i=0;i<sira;i++)
                {
                    for(int j=0;j<5;j++)
                    {
                        if (arkaBesliMi == true)
                        {
                            if (i != sira - 1 && j == 2)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (j == 2)
                                continue;
                        }

                        Button Koltuk = new Button();
                        Koltuk.Height = Koltuk.Width = 40;
                        Koltuk.Top = 30 + (i * 45);
                        Koltuk.Left = 5 + (j * 45);
                        Koltuk.Text = KoltukNo.ToString();
                        KoltukNo++;
                        Koltuk.ContextMenuStrip = contextMenuStrip1;
                        Koltuk.MouseDown += Koltuk_MouseDown;
                        this.Controls.Add(Koltuk);
                    }
                }
            }
           
        }
        Button tiklanan;
        private void Koltuk_MouseDown(object sender, MouseEventArgs e)
        {
            tiklanan = sender as Button;
        }

        private void rezerveEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cmbotobus.SelectedIndex==-1 || cmbnereden.SelectedIndex==-1|| cmbnereye.SelectedIndex==-1)
            {
                MessageBox.Show("Lütfen önce gerekli alanları doldurunuz.");
                return;
            }
            KayıtFormu kf = new KayıtFormu();
            DialogResult sonuc=kf.ShowDialog();
            if (sonuc==DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = string.Format("{0} {1}", kf.txtisim.Text, kf.txtsoyisim.Text);
                lvi.SubItems.Add(kf.mskdtelefon.Text);
                if(kf.rdbbay.Checked)
                {
                    lvi.SubItems.Add("BAY");
                    tiklanan.BackColor = Color.Turquoise;
                }
                if(kf.rdbbayan.Checked)
                {
                    lvi.SubItems.Add("BAYAN");
                    tiklanan.BackColor = Color.Pink;
                }
                lvi.SubItems.Add(cmbnereden.Text);
                lvi.SubItems.Add(cmbnereye.Text);
                lvi.SubItems.Add(tiklanan.Text);
                lvi.SubItems.Add(dtpTarih.Text);
                lvi.SubItems.Add(nudFiyat.Value.ToString());
                listView1.Items.Add(lvi);
            }
        }
    }
}
