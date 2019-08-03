using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WicramaElectricals
{  
    public partial class Confirm : Form
    {
        public Home MyParent { get; set; }
        public Confirm()
        {
            InitializeComponent();
        }

        private void Confirm_Load(object sender, EventArgs e)
        {
            LoadFromArray();
        }

        void LoadFromArray()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Index", "Index");
            dataGridView1.Columns.Add("Value", "Description");
            for (int i = 0; i < MyParent.ItemList.Length; i++)
            {
                dataGridView1.Rows.Add(new object[] { i + 1, MyParent.ItemList[i] });
            }
        }

        private void Confirm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyParent.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Array.Clear(MyParent.ItemList, 0, 5);
            MyParent.TotalPrice = 0;
            MyParent.UpdateQuery = "";
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                userdetail user = new userdetail();

                MySqlConnection conn = new MySqlConnection(@"datasource=127.0.0.1;port=3306;SslMode=none;username=root;password=;database=wicrama;");
                string query = "INSERT INTO `orders`(`user`,`details`,`price`) VALUES('"+user.getUname()+"','"+ArrayToString()+"',"+MyParent.TotalPrice+");"+MyParent.UpdateQuery+"";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Success");
                button1_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                label1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                label2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                button2.Enabled = true;
            }
        }

        public string ArrayToString()
        {
           string items="";
           for(int i = 0; i < 10; i++)
            {
                items += MyParent.ItemList[i];
            }
            return items;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyParent.ItemList[int.Parse(label1.Text)-1] = null;
            LoadFromArray();
            button2.Enabled = false;
        }
    }
}
