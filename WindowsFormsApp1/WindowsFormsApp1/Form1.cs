using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {

        HttpClient client = new HttpClient();
        private DataGridViewRow dataGridViewRow;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //HttpClient client = new HttpClient();
           HttpResponseMessage mess = client.GetAsync("http://localhost:51403/api/students").Result;
            if (mess.IsSuccessStatusCode)
            {
               List<Student> sts = mess.Content.ReadAsAsync<List<Student>>().Result;
                dgv_students.DataSource = sts.Select(n => new { id = n.id, name = n.name, age = n.age, department = n.department.name }).ToList();
            }
            //HttpClient cli = new HttpClient();
           HttpResponseMessage resp =client.GetAsync("http://localhost:51403/api/departments").Result;
            if (resp.IsSuccessStatusCode)
            {
               List<department> depts = resp.Content.ReadAsAsync<List<department>>().Result;
                cb_depts.DisplayMember = "name";
                cb_depts.ValueMember = "id";
                cb_depts.DataSource = depts;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Student s = new Student()
            {
                id = int.Parse(txt_id.Text),
                name = txt_name.Text,
                age = int.Parse(txt_age.Text),
                deptid = (int)cb_depts.SelectedValue
            };
           HttpResponseMessage mess = client.PostAsJsonAsync("http://localhost:51403/api/students", s).Result;
            if (mess.IsSuccessStatusCode)
            {
                Form1_Load(null, null);
            }
            lbl_result.Text = "Data Added";
            txt_id.Text = txt_name.Text = txt_age.Text = "";
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            int id =(int) dgv_students.SelectedCells[s.id].Value;
                HttpResponseMessage mess = client.DeleteAsync($"http://localhost:51403/api/students/{id}").Result;
            
            if (mess.IsSuccessStatusCode)
            {
                Form1_Load(null, null);
            }
            lbl_result.Text = "dataRemoved";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

            Student s = new Student();
            s.name = txt_name.Text;
            s.age = int.Parse(txt_age.Text);
            s.deptid = (int)cb_depts.SelectedValue;



            //int id = (int)dgv_students.SelectedCells[s.id].Value;
            HttpResponseMessage mess = client.PutAsJsonAsync($"http://localhost:51403/api/students/{s.id}",s).Result;
            

            if (mess.IsSuccessStatusCode)
            {
                Form1_Load(null, null);
                lbl_result.Text = "Data Updated";
                txt_id.Enabled = true;
                txt_id.Text = txt_name.Text = txt_age.Text = " ";
            }
            

            lbl_result.Text = "dataUpdated";
        }

        private void dgv_students_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Student s = new Student();
            txt_id.Enabled = false;
            btn_add.Visible = false;
            lbl_result.Text = "";
            txt_name.Text = dgv_students.SelectedCells[1].Value.ToString();
            txt_age.Text = dgv_students.SelectedCells[2].Value.ToString();
            cb_depts.Text = dgv_students.SelectedCells[3].Value.ToString();
        }
    }
}
