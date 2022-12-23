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
    public partial class Regist : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; user id = root; password = 0000; database = velos");
        public Regist()
        {
            InitializeComponent();
        }

        private void showData()
        {
            con.Open();
            string selectQuery = "SELECT * FROM клиенты";
            DataTable DATA = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, con);
            adapter.Fill(DATA);
            dataGridView1.DataSource = DATA;
            con.Close();
        }

        public void insertData()
        {
            
            try
            {
                string str = "INSERT INTO клиенты ( Фамилия, Имя, Отчество, Паспорт) VALUES ( @Фамилия, @Имя, @Отчество, @Паспорт)";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(str, con);

                // cmd.Parameters.AddWithValue("@ID", textBox_ID);
                cmd.Parameters.AddWithValue("@Фамилия", textBox1.Text);
                cmd.Parameters.AddWithValue("@Имя", textBox2.Text);
                cmd.Parameters.AddWithValue("@Отчество", textBox3.Text);
                cmd.Parameters.AddWithValue("@Паспорт", int.Parse(textBox4.Text));//конвертация в тип int
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException mex)
            {
                MessageBox.Show("Server error: " + mex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void TextBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void TextBox3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void TextBox4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            insertData();
            showData();
        }

        private void Regist_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox5.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void TextBox5_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            con.Open();
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            String selectedCustomersID = "";
            selectedCustomersID = dataGridView1.Rows[selectedIndex].Cells["idКлиенты"].Value.ToString();
            dataGridView1.Rows.RemoveAt(selectedIndex);
            string deleteQuery = "DELETE FROM клиенты WHERE idКлиенты = " + selectedCustomersID;
            MySqlCommand command = new MySqlCommand(deleteQuery, con);
            MySqlDataReader myReader;
            myReader = command.ExecuteReader();
            dataGridView1.Refresh();
            con.Close();
        }
    }
}
