using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bankamatik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KACV7HQ\\SQLEXPRESS;Initial Catalog=Bankamatik;Integrated Security=True");
        private void LnkKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 fr = new Form2();
            fr.Show();
        }

        private void BtnGirişYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select AD,Soyad from TBLKISILER where hesapno=@p1 and sıfre=@p2",baglanti);
            komut.Parameters.AddWithValue("@p1", MskHesapNo.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();   
            if(dr.Read()) 
            {
                Form3 fr = new Form3();
                fr.hesapno = MskHesapNo.Text;
                fr.Show();
            }
            else
            {
                MessageBox.Show("Girdiğiniz Bilgiler Hatalı");
            }
            baglanti.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
