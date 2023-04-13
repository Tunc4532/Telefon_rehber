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

namespace Telefon_rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");

         void listele()
        {
            DataTable dte = new DataTable();
            SqlDataAdapter dae = new SqlDataAdapter("select * from tbl_kisiler",baglanti);
            dae.Fill(dte);
            dataGridView1.DataSource = dte;
        }
        void temizle()
        {
            txtıd.Text = " ";
            txtad.Text = " ";
            txtsoyad.Text = " ";
            msktelefon.Text = " ";
            txtmail.Text = " ";
            txtad.Focus();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilen = dataGridView1.SelectedCells[0].RowIndex;
            txtıd.Text= dataGridView1.Rows[seçilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[seçilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[seçilen].Cells[2].Value.ToString();
            msktelefon.Text = dataGridView1.Rows[seçilen].Cells[3].Value.ToString();
            txtmail.Text = dataGridView1.Rows[seçilen].Cells[4].Value.ToString();
        }


        private void btnekle_Click(object sender, EventArgs e)
        {
           
            DialogResult sonuc;
            sonuc = MessageBox.Show("Kayıt ekleme işlemi gerçekleştirilsin'mi ?","Bilgi",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if( sonuc == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into tbl_kisiler (AD,SOYAD,TELEFON,MAİL) values (@e1,@e2,@e3,@e4)", baglanti);
                komut.Parameters.AddWithValue("@e1", txtad.Text);
                komut.Parameters.AddWithValue("@e2", txtsoyad.Text);
                komut.Parameters.AddWithValue("@e3", msktelefon.Text);
                komut.Parameters.AddWithValue("@e4", txtmail.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Eklendi");
                
            }
            else
            {
                MessageBox.Show("işlem iptal edildi");
            }
            listele();
            temizle();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
          
            DialogResult sonuç;
            sonuç = MessageBox.Show("Kayıt silme işlemi gerçekleştirilsin'mi ?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuç == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("Delete from tbl_kisiler where ID =" + txtıd.Text, baglanti);
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Silindi ");

            }
            else
            {
                MessageBox.Show("işlem iptal edildi");
            }
            listele();
            temizle();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            
            DialogResult sonu;
            sonu = MessageBox.Show("Kayıt güncelleme işlemi gerçekleştirilsin'mi ?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonu == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update tbl_kisiler set AD=@W1,SOYAD=@W2,TELEFON=@W3,MAİL=@W4 where ID= @W5", baglanti);
                komut3.Parameters.AddWithValue("@W1", txtad.Text);
                komut3.Parameters.AddWithValue("@W2", txtsoyad.Text);
                komut3.Parameters.AddWithValue("@W3", msktelefon.Text);
                komut3.Parameters.AddWithValue("@W4", txtmail.Text);
                komut3.Parameters.AddWithValue("@W5", txtıd.Text);
                komut3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Güncellendi");

            }
            else
            {
                MessageBox.Show("işlem iptal edildi");
            }
            listele();
            temizle();
        }
    }
}
