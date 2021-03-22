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

namespace _5951071099_TranDinhThao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loaddata();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LA497N55;Initial Catalog=DemoCRUD;Integrated Security=True");
        private void Loaddata()
        {
            string query = "SELECT * FROM StudentsTb";

            SqlCommand cmd = new SqlCommand(query,con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            con.Close();

            StudentRecordData.DataSource = dt;
        }


        private bool IsValidData()
        {
            if(txtHo.Text == string.Empty ||txtTen.Text == string.Empty || txtDiaChi.Text == string.Empty || string.IsNullOrEmpty(txtDienThoai.Text)|| string.IsNullOrEmpty(txtSoBaoDanh.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                string query = "Insert into StudentsTb Values (@Name,@FatherName,@RollNumber,@Address,@Moblie)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtHo.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTen.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSoBaoDanh.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Moblie", txtDienThoai.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Loaddata();
            }
        }

        public int StudentID1;

        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                StudentID1 = Convert.ToInt32(StudentRecordData.Rows[0].Cells[0].Value);
                txtHo.Text = StudentRecordData.Rows[0].Cells[1].Value.ToString();
                txtTen.Text = StudentRecordData.Rows[0].Cells[2].Value.ToString();
                txtSoBaoDanh.Text = StudentRecordData.Rows[0].Cells[3].Value.ToString();
                txtDiaChi.Text = StudentRecordData.Rows[0].Cells[4].Value.ToString();
                txtDienThoai.Text = StudentRecordData.Rows[0].Cells[5].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Loaddata();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (StudentID1 > 0)
            {
                SqlCommand cmd = new SqlCommand("Delete from StudentsTb where StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID1);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Loaddata();
            }
            else
            {
                MessageBox.Show("Xoá bị lỗi !!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (StudentID1 > 0)
            {
                SqlCommand cmd = new SqlCommand("Update StudentsTb Set Name = @Name, FatherName = @FatherName,RollNumber = @RollNumber,Address = @Address, @Moblie = @Mobile where StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHo.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTen.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSoBaoDanh.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Moblie", txtDienThoai.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID1);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Cập Nhật Xong");
                Loaddata();
            }
            else
            {
                MessageBox.Show("Cập Nhật Bị Lỗi !!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
