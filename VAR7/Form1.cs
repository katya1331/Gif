using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VAR7
{
    public partial class Form1 : Form
    {
        class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        List<Product> products = new List<Product>();
        List<Group> groups = new List<Group>();
        class Group
        {
            public int InventoryNumber { get; set; }
            public int Id { get; set; }
            public string GroupName { get; set; }
            public int Price { get; set; }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            products.Clear();
            groups.Clear();
            StreamReader sr = new StreamReader("1.txt");
            while (!sr.EndOfStream)
            {
                string[] pr = sr.ReadLine().Split(',');
                Product p = new Product();
                p.Id = Convert.ToInt32(pr[0]);
                p.Name = pr[1].ToString();
                products.Add(p);
            }
            StreamReader sr2 = new StreamReader("2.txt");
            while (!sr2.EndOfStream)
            {
                string[] pr = sr2.ReadLine().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Group pp = new Group();
                pp.InventoryNumber = Convert.ToInt32(pr[0]);
                pp.Id = Convert.ToInt32(pr[1]);
                pp.GroupName = pr[2].ToString();
                pp.Price = Convert.ToInt32(pr[3]);
                groups.Add(pp);
            }
            var result = from g in groups
                         join p in products on g.Id equals p.Id
                         group new { g.GroupName, p.Id, p.Name, g.InventoryNumber, g.Price } by g.GroupName into grouped
                         select grouped;
            foreach (var group in result)
            {
               listBox1.Items.Add(group.Key);
                foreach (var item in group)
                {
                    listBox1.Items.Add($"{item.Id} {item.Name} {item.InventoryNumber} {item.Price}");
                }
                listBox1.Items.Add("--------------------------------------------------");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""|| textBox1.Text == "Кулинария" || textBox1.Text == "Масло")
            {
                MessageBox.Show("Group don't exist!!!!!");
            }
            else
            {
                string groupNameToRemove = '"'+textBox1.Text+'"';
                List<Group> updatedGroups = RemoveGroup(groups, groupNameToRemove);
                foreach (var group in updatedGroups)
                {
                    listBox1.Items.Add($"{group.GroupName} {group.InventoryNumber} {group.Id} {group.Price}");
                }
            }
         
        }

        static List<Group> RemoveGroup(List<Group> groups, string groupName)
        {
            return groups.Where(g => g.GroupName != groupName).ToList();
        }
    }
    
}
