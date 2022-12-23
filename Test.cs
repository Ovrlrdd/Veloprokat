using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _123
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();

            List<Phone> phones = new List<Phone>
        {
            new Phone { Id=11, Name="Samsung Galaxy Ace 2", Year=2012},
            new Phone { Id=12, Name="Samsung Galaxy S4", Year=2013},
            new Phone { Id=13, Name="iPhone 6", Year=2014},
            new Phone { Id=14, Name="Microsoft Lumia 435", Year=2015},
            new Phone { Id=15, Name="Xiaomi Mi 5", Year=2015}
        };
            comboBox1.DataSource = phones;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Id";
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Phone phone = (Phone)comboBox1.SelectedItem;
            listBox1.Items.Add(phone);
        }

        class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
    }
}
