using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bankamatik
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KACV7HQ\\SQLEXPRESS;Initial Catalog=Bankamatik;Integrated Security=True");
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKISILER (AD,SOYAD,TC,TELEFON,HESAPNO,SIFRE) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTC.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p5", MskHesapNo.Text);
            komut.Parameters.AddWithValue("@p6", TxtSifre.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Bilgileri Sisteme Kaydedildi");

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("insert into TBLHESAP (hesapno) values (@a1)", baglanti);
            komut2.Parameters.AddWithValue("@a1", MskHesapNo.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            this.Hide();

        }          
           
    

        private void BtnHesapNo_Click(object sender, EventArgs e)
        {
            Random rast = new Random(); 
            MskHesapNo.Text = rast.Next(100000,1000000).ToString();

            int sorgu;
            baglanti.Open();
            SqlCommand kontrol = new SqlCommand("select hesapno from TBLKISILER", baglanti);
            SqlDataReader drkontrol = kontrol.ExecuteReader();
            while (drkontrol.Read())
            {
                sorgu = Convert.ToInt32(drkontrol[0]);
                if (sorgu == Convert.ToInt32(MskHesapNo.Text))
                {
                   MessageBox.Show("Bu hesap numarası mevcut.Tekrar deneyiniz");
                }
            }
            baglanti.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
