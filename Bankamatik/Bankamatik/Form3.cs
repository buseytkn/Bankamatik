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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public string hesapno;
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KACV7HQ\\SQLEXPRESS;Initial Catalog=Bankamatik;Integrated Security=True");
        private void Form3_Load(object sender, EventArgs e)
        {
            LblHesapNo.Text = hesapno;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLKISILER where hesapno=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1", LblHesapNo.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
                LblTC.Text = dr[3].ToString();
                LblTelefon.Text = dr[4].ToString();
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select BAKIYE from TBLHESAP where hesapno=@a1",baglanti);
            komut2.Parameters.AddWithValue("@a1", LblHesapNo.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblBakiye.Text = dr2[0].ToString();
            }
            baglanti.Close();

            SqlDataAdapter da = new SqlDataAdapter("select (AD+' '+SOYAD) as ALICI,TUTAR from TBLHAREKET inner join TBLKISILER on TBLHAREKET.ALICI = TBLKISILER.HESAPNO where GONDEREN="+hesapno,baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            SqlDataAdapter da2 = new SqlDataAdapter("select (AD+' '+SOYAD) as GONDEREN,TUTAR from TBLHAREKET inner join TBLKISILER on TBLHAREKET.GONDEREN = TBLKISILER.HESAPNO where ALICI="+hesapno,baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            //Gönderilen hesabın para artışı
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLHESAP set bakıye=bakıye+@p1 where hesapno=@p2",baglanti);
            komut.Parameters.AddWithValue("@p1",decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@p2", MskHesapNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İşlem Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Gönderen hesabın para azalışı
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("update TBLHESAP set bakıye=bakıye-@a1 where hesapno=@a2",baglanti);
            komut2.Parameters.AddWithValue("@a1", decimal.Parse(TxtTutar.Text));
            komut2.Parameters.AddWithValue("@a2", hesapno);
            komut2.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("insert into TBLHAREKET (GONDEREN,ALICI,TUTAR) values (@b1,@b2,@b3)",baglanti);
            komut3.Parameters.AddWithValue("@b1", hesapno);
            komut3.Parameters.AddWithValue("@b2", MskHesapNo.Text);
            komut3.Parameters.AddWithValue("@b3", TxtTutar.Text);
            komut3.ExecuteNonQuery();
            baglanti.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
