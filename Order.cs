using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace _123
{
    public partial class Order : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; user id = root; password = 0000; database = velos");
        string s, w;

        public Order()
        {
            InitializeComponent();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            con.Open();
            comboBox1.Text = Customers();
            comboBox2.Text = Bikes();
        }

        public void insertData()
        {
            try
            {
                string str = "INSERT INTO информация ( Срок, Сумма, Клиенты_idКлиенты, Велосипеды_idВелосипеды) VALUES ( @Срок, @Сумма, @Клиенты_idКлиенты, @Велосипеды_idВелосипеды)";
                MySqlCommand cmd = new MySqlCommand(str, con);

                // cmd.Parameters.AddWithValue("@ID", textBox_ID);
                cmd.Parameters.AddWithValue("@Клиенты_idКлиенты", int.Parse(textBox2.Text));
                cmd.Parameters.AddWithValue("@Велосипеды_idВелосипеды", int.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@Срок", int.Parse(textBox5.Text));
                cmd.Parameters.AddWithValue("@Сумма", double.Parse(textBox1.Text));
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException mex)
            {
                MessageBox.Show("Server error" + mex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void Found()
        {

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(comboBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            
                            break;
                        }
            }
        }

        private void Load_Cus()
        {
            string selectQuery = "SELECT * FROM клиенты";
            DataTable DATA = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, con);
            adapter.Fill(DATA);
            dataGridView1.DataSource = DATA;
        }

        private string Customers()
        {
            StringBuilder sb = new StringBuilder();
            string selectQuery = "SELECT Фамилия FROM клиенты";
            try
            {
                using (MySqlCommand com = new MySqlCommand(selectQuery, con))
                {
                    using (MySqlDataReader rd = com.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            comboBox1.Items.Add(rd[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sb.ToString();
        }

        private void Found_2()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(comboBox2.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }
        private void Load_Bank()
        {
            string selectQuery = "SELECT * FROM велосипеды";
            DataTable DATA = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, con);
            adapter.Fill(DATA);
            dataGridView1.DataSource = DATA;
        }
        private string Bikes()
        {
            StringBuilder sb = new StringBuilder();
            string selectQuery = "SELECT Модель FROM велосипеды";
            try
            {
                using (MySqlCommand com = new MySqlCommand(selectQuery, con))
                {
                    using (MySqlDataReader rd = com.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            comboBox2.Items.Add(rd[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sb.ToString();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Load_Cus();
            Found();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Load_Bank();
            Found_2();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            insertData();
            string updateQuery = "UPDATE велосипеды SET Статус = 'В пути' WHERE idВелосипеды = '" +int.Parse(textBox3.Text)+ "' AND Статус = 'В велопрокате'";
            MySqlCommand command = new MySqlCommand(updateQuery, con);
            command.ExecuteNonQuery();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            s = Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            textBox2.Text = s;
            int.Parse(textBox2.Text);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            w = Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            textBox3.Text = w;
            int.Parse(textBox3.Text);
        }
    }
}
