using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        public const string DataSource = @"DataSource=C:\sqlite\onetable.db";

        public Form1()
        {
            InitializeComponent();
           
        }

        //fills empty slots in grid when loaded
        private void Form1_Load(object sender, EventArgs e)
        {
            //uploads datatable from sqlite into datagridview  
            using (var connection = new SQLiteConnection(DataSource))
            {
                connection.Open();
                DataTable ds = new DataTable();
                var da = new SQLiteDataAdapter("select * from client", connection);
                da.Fill(ds);
                dataGridView1.DataSource = ds;
            }
        }

        //add client button
        private void button5_Click(object sender, EventArgs e)
        {
            //creates new client with new id
            using (var connection = new SQLiteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format("insert into client(name) values ('')");
                var reader = command.ExecuteReader();
            }
            //updates datagridview
            using (var connection = new SQLiteConnection(DataSource))
            {
                DataTable ds = new DataTable();
                var da = new SQLiteDataAdapter("select * from client", connection);
                da.Fill(ds);
                dataGridView1.DataSource = ds;
            }
        }

        //update button
        private void button1_Click(object sender, EventArgs e)
        {
            if (grandtotal.Text == "0" || grandtotal.Text == "")
            {
                MessageBox.Show("Jobs must be entered and calculated before updating", "Error");
                return;
            }
            else
            {
                dataGridView1.CurrentRow.Cells[1].Value = name.Text;
                dataGridView1.CurrentRow.Cells[2].Value = locations.Text;
                dataGridView1.CurrentRow.Cells[3].Value = remtot.Text;
                dataGridView1.CurrentRow.Cells[4].Value = trimtot.Text;
                dataGridView1.CurrentRow.Cells[5].Value = planttot.Text;
                dataGridView1.CurrentRow.Cells[6].Value = stumptot.Text;
                dataGridView1.CurrentRow.Cells[7].Value = grandtotal.Text;
            }
            //updates sql client table
            using (var connection = new SQLiteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format("update client set Name = '{0}', Location ='{1}', 'Tree Removal' ={2}, 'Tree Trimming' = {3}, 'Tree Planting' = {4}, 'Stump Grinding' = {5}, Total='{6}' where ID = '{7}'", name.Text, locations.Text, remtot.Text, trimtot.Text, planttot.Text, stumptot.Text, grandtotal.Text, dataGridView1.CurrentRow.Cells[0].Value);
                var reader = command.ExecuteReader();
            }
            //deletes client's old treeremoval job
            using (var connection = new SQLiteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format("delete from treeremoval where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                var reader = command.ExecuteReader();
            }
            //updates client's new treeremoval info if applicable
            if(remqty.Text!="0")
            {
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("insert into treeremoval(ID, QTY, 'Sub Total', Tax, Total) values ({0}, {1},{2},{3},{4})", dataGridView1.CurrentRow.Cells[0].Value, remqty.Text, remsub.Text, remtax.Text, remtot.Text);
                    var reader = command.ExecuteReader();
                }
            }
            //deletes client's old treetrimming job
            using (var connection = new SQLiteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format("delete from treetrimming where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                var reader = command.ExecuteReader();
            }
            //updates client's new treetrimming info if applicable
            if (trimqty.Text != "0")
            {
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("insert into treetrimming(ID, HRS, 'Sub Total', Tax, Total) values ({0}, {1},{2},{3},{4})", dataGridView1.CurrentRow.Cells[0].Value, trimqty.Text, trimsub.Text, trimtax.Text, trimtot.Text);
                    var reader = command.ExecuteReader();
                }
            }
            // ' ' tree planting
            using (var connection = new SQLiteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format("delete from treeplanting where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                var reader = command.ExecuteReader();
            }

            // ' ' tree planting
            if (plantqty.Text != "0")
            {
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("insert into treeplanting(ID, Ft, 'Sub Total', Tax, Total) values ({0}, {1},{2},{3},{4})", dataGridView1.CurrentRow.Cells[0].Value, plantqty.Text, plantsub.Text, plantax.Text, planttot.Text);
                    var reader = command.ExecuteReader();
                }
            }

            // ' ' stump grinding
            using (var connection = new SQLiteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format("delete from stumpgrinding where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                var reader = command.ExecuteReader();
            }

            // ' ' stump grinding
            if (stumpqty.Text != "0")
            {
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("insert into stumpgrinding(ID, INCH, 'Sub Total', Tax, Total) values ({0}, {1},{2},{3},{4})", dataGridView1.CurrentRow.Cells[0].Value, stumpqty.Text, stumpsub.Text, stumptax.Text, stumptot.Text);
                    var reader = command.ExecuteReader();
                }
            }
            // ' ' total
            using (var connection = new SQLiteConnection(DataSource))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = String.Format("delete from total where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                var reader = command.ExecuteReader();
            }

            // ' ' total
            if (grandtotal.Text != "0")
            {
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("insert into total(ID, 'Sub Total', Tax, Total) values ({0}, {1},{2},{3})", dataGridView1.CurrentRow.Cells[0].Value, grandsub.Text, totaltax.Text, grandtotal.Text);
                    var reader = command.ExecuteReader();
                }
            }
        }

        //delete button
        private void button2_Click(object sender, EventArgs e)
        {
            if (grandtotal.Text == "")
            {
                MessageBox.Show("No appointment selected", "Error");
                return;
            }
            DialogResult result = MessageBox.Show("Are you sure you want to delete this appointment?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) 
            {
                //if user responds yes, all data from every table that corresponds with the client's id is deleted
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("delete from client where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                    var reader = command.ExecuteReader();
                }
                //tree removal delete
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("delete from treeremoval where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                    var reader = command.ExecuteReader();
                }
                //tree trimming delete
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("delete from treetrimming where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                    var reader = command.ExecuteReader();
                }
                //tree planting delete
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("delete from treeplanting where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                    var reader = command.ExecuteReader();
                }
                //stump grinding delete
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("delete from stumpgrinding where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                    var reader = command.ExecuteReader();
                }
                //total delete
                using (var connection = new SQLiteConnection(DataSource))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = String.Format("delete from total where id={0}", dataGridView1.CurrentRow.Cells[0].Value);
                    var reader = command.ExecuteReader();
                }
                //datagridview is updated with new information
                using (var connection = new SQLiteConnection(DataSource))
                {
                    DataTable ds = new DataTable();
                    var da = new SQLiteDataAdapter("select * from client", connection);
                    da.Fill(ds);
                    dataGridView1.DataSource = ds;
                }
            }
        }

        //clear button
        private void button4_Click(object sender, EventArgs e)
        {
            if (grandtotal.Text == "")
            {
                MessageBox.Show("No appointment selected", "Error");
                return;
            }
            DialogResult result = MessageBox.Show("Are you sure you want to clear all job fields?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {   //if user responds yes, all job fields are set to 0
                remqty.Text = "0";
                trimqty.Text = "0";
                plantqty.Text = "0";
                stumpqty.Text = "0";
                grandtotal.Text = "0";
            }

            else return;
        }

        //calculate button
        private void button3_Click(object sender, EventArgs e)
        {
            if (grandtotal.Text == "")
            {
                MessageBox.Show("No appointment selected", "Error");
                return;
            }
            if(trimqty.Text =="")
            {
                trimqty.Text = "0";
            }
            if (remqty.Text == "")
            {
                remqty.Text = "0";
            }
            if (plantqty.Text == "")
            {
                plantqty.Text = "0";
            }
            if (stumpqty.Text == "")
            {
                stumpqty.Text = "0";
            }
            grandtotal.Text = (Convert.ToDouble(remtot.Text) + Convert.ToDouble(trimtot.Text) + Convert.ToDouble(planttot.Text) + Convert.ToDouble(stumptot.Text)).ToString("0.00");
            if (grandtotal.Text == "0.00")
            {
                grandtotal.Text = "0";
            }
        }

        //allows only numeric digits or . to be entered in tree removal textbox
        private void remqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != '.')
            {
                e.Handled = true;
                MessageBox.Show("Invalid entry, must be a number");
            }
        }

        //allows only numeric digits or . to be entered in tree trim textbox
        private void trimqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != '.')
            {
                e.Handled = true;
                MessageBox.Show("Invalid entry, must be a number");
            }
        }

        //allows only numeric digits or . to be entered in tree plant textbox
        private void plantqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != '.')
            {
                e.Handled = true;
                MessageBox.Show("Invalid entry, must be a number");
            }
        }

        //allows only numeric digits or . to be entered in stump stumping textbox
        private void stumpqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != '.')
            {
                e.Handled = true;
                MessageBox.Show("Invalid, entry must be a number", "Error");
            }
        }

        //tree removal quantity textbox-automatically fills sub tax and total textboxes in job row
        private void remqty_TextChanged(object sender, EventArgs e)
        {
            //when any job quantity is changed, grand total box is set to zero, to prevent user from updating client info without incorrect calculation
            grandtotal.Text = "0";

            if (string.IsNullOrEmpty(remqty.Text) || remqty.Text == ".")
            {
                remsub.Text = "0";
                remtax.Text = "0";
                remtot.Text = "0";
                return;
            }
            if (Convert.ToDouble(remqty.Text) == 0)
            {
                remsub.Text = "0";
                remtax.Text = "0";
                remtot.Text = "0";
            }
            else
            {
                remsub.Text = (Convert.ToDouble(remqty.Text) * 1000).ToString("0.00"); //equation fills sub total box for job
                remtax.Text = (Convert.ToDouble(remsub.Text) * .0885).ToString("0.00"); //equation fills tax box for job
                remtot.Text = ((Convert.ToDouble(remsub.Text) + Convert.ToDouble(remtax.Text))).ToString("0.00"); //equation fills total box for job
            }  
        }

        //tree trim quantity textbox-automatically fills sub tax and total textboxes in row
        private void trimqty_TextChanged(object sender, EventArgs e)
        {
            //when any job quantity is changed, grand total box is set to zero, to prevent user from updating client info without incorrect calculation
            grandtotal.Text = "0";

            //updates subtotal, tax, and total textboxes for this job
            if (string.IsNullOrEmpty(trimqty.Text) || trimqty.Text == ".")
            {
                trimsub.Text = "0";
                trimtax.Text = "0";
                trimtot.Text = "0";
                return;
            }
            
            if (Convert.ToDouble(trimqty.Text) == 0)
            {
                trimsub.Text = "0";
                trimtax.Text = "0";
                trimtot.Text = "0";
            }
            else
            {
                trimsub.Text = (50 + ((Convert.ToDouble(trimqty.Text) - 1) * 10)).ToString("0.00"); //equation fills sub total box for job row
                trimtax.Text = (Convert.ToDouble(trimsub.Text) * .0885).ToString("0.00");//equation fills tax box for job row
                trimtot.Text = (Convert.ToDouble(trimsub.Text) + Convert.ToDouble(trimtax.Text)).ToString("0.00"); //equation fills total box for row
            }
        }

        //tree plant quantity textbox-automatically fills sub tax and total textboxes in row
        private void plantqty_TextChanged(object sender, EventArgs e)
        {
            // ' '
            grandtotal.Text = "0";

            if (string.IsNullOrEmpty(plantqty.Text)||plantqty.Text==".")
            {
                plantsub.Text = "0";
                plantax.Text = "0";
                planttot.Text = "0";
                return;
            }
            if (Convert.ToDouble(plantqty.Text) < 3)
            {
                if(Convert.ToDouble(plantqty.Text) == 0)
                {
                    plantsub.Text = "0";
                    plantax.Text = "0";
                    planttot.Text = "0";
                }
                else
                {
                    plantsub.Text = "75";//sub for under 3 ft
                    plantax.Text =  (75 * .085).ToString("0.00");//tax for under 3 ft
                    planttot.Text = ((Convert.ToDouble(plantsub.Text) + Convert.ToDouble(plantax.Text))).ToString("0.00");//total for under 3 ft
                }
            }
            else
            {
                plantsub.Text =  (75 + ((Convert.ToDouble(plantqty.Text) - 3) * 5)).ToString("0.00");//sub for over 3 ft
                plantax.Text =  (Convert.ToDouble(plantsub.Text)*.0885).ToString("0.00");//tax for over 3 ft
                planttot.Text = (Convert.ToDouble(plantsub.Text) + Convert.ToDouble(plantax.Text)).ToString("0.00");//total for over 3 ft
            }  
        }

        //stump stumping quantity textbox-automatically fills sub tax and total textboxes in row
        private void stumpqty_TextChanged(object sender, EventArgs e)
        {
            //' '
            grandtotal.Text = "0";

            if (string.IsNullOrEmpty(stumpqty.Text)||stumpqty.Text==".")
            {
                stumpsub.Text = "0";
                stumptax.Text = "0";
                stumptot.Text = "0";
                return;
            }
            if(Convert.ToDouble(stumpqty.Text)==0)
            {
                stumpsub.Text = "0";
                stumptax.Text = "0";
                stumptot.Text = "0";
            }
            else
            {
                stumpsub.Text =  (30 + (Convert.ToDouble(stumpqty.Text) * 5)).ToString("0.00");//sub
                stumptax.Text = (Convert.ToDouble(stumpsub.Text) * .0885).ToString("0.00");//tax
                stumptot.Text = (Convert.ToDouble(stumpsub.Text) + Convert.ToDouble(stumptax.Text)).ToString("0.00");//total
            }
        }

        //grand total quantity textbox-automatically fills sub tax and total textboxes in row
        private void grandtotal_TextChanged(object sender, EventArgs e)
        {
            //after calcuate button is pressed, grand total text box calculates total, then automatically fills tax and sub boxes
            if (Convert.ToDouble(grandtotal.Text) == 0|| grandtotal.Text == null)
            {
                grandsub.Text = "0";
                totaltax.Text = "0";
            }
            else
            {
                //fills grand tax and grand sub total text boxes
                grandsub.Text = (((Convert.ToDouble(grandtotal.Text) / 1.0885))).ToString("0.00");
                totaltax.Text = (((Convert.ToDouble(grandsub.Text) * .0885))).ToString("0.00");
            }
        }

        //when cell in clicked in grid all quantities and totals are loaded
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //generic data is displayed for empty client slot
            if (dataGridView1.CurrentRow.Cells[1].Value.ToString() == "")
            {
                name.Text = "Enter Name";
            }
            //if data exists, it is copied from datagridview into the text boxes below
            else name.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "")
            {
                locations.Text = "Enter Location";
            }
            else locations.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "" || dataGridView1.CurrentRow.Cells[3].Value.ToString() == "0")
            {
                remqty.Text = "0";
            }
            else
            {
                remtot.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                remqty.Text = ((Convert.ToDouble(remtot.Text)) / 1088.5).ToString("0.0"); //equation takes total for job and solves for quantity
            }

            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "" || dataGridView1.CurrentRow.Cells[4].Value.ToString() == "0")
            {
                trimqty.Text = "0";
            }
            else
            {
                trimtot.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                trimqty.Text = ((Convert.ToDouble(trimtot.Text) - 43.54) / 10.885).ToString("0.0"); //equation takes total for job and solves for quantity
            }

            if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "" || dataGridView1.CurrentRow.Cells[5].Value.ToString() == "0")
            {
                plantqty.Text = "0";
            }
            else
            {
                planttot.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                plantqty.Text = ((Convert.ToDouble(planttot.Text) - 65.31) / 5.4425).ToString("0.0"); //equation takes total for job and solves for quantity
            }
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "" || dataGridView1.CurrentRow.Cells[6].Value.ToString() == "0")
            {
                stumpqty.Text = "0";
            }
            else
            {
                stumptot.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                stumpqty.Text = ((Convert.ToDouble(stumptot.Text) - 32.655) / 5.4425).ToString("0.0"); //equation takes total for job and solves for quantity
            }
            if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "" || dataGridView1.CurrentRow.Cells[7].Value.ToString() == "0")
            {
                grandtotal.Text = "0";
            }
            else
            {
                grandtotal.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }
        }

    }
}
