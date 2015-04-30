using SerqAccess.EasyData;
using SerqAccess.EasyData.Oracle;
using SerqAccess.SampleEntity;
using SerqAccess.SampleQuery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerqAccess.SampleUI
{
    public partial class frmSqlServerSampleOp : Form
    {
        private string conString;
        DBManagerFactory dbManagerFactory;
        StoredProcMap storedProcMap;
        SQLMap sqlMap;
        PersonQueries pQueries;
        private static frmSqlServerSampleOp _instance;
        public static frmSqlServerSampleOp GetInstance()
        {
            return _instance = _instance ?? new frmSqlServerSampleOp();
        }
        private frmSqlServerSampleOp()
        {
            InitializeComponent();
           
        }

        private void frmSqlServerSampleOp_Load(object sender, EventArgs e)
        {
            
        }

        private void PopulatePeopleGrid()
        {
            List<Person> people = pQueries.GetAll();
            dgvPeople.DataSource = people;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbDatabase.SelectedItem.ToString().Equals("Oracle"))
            {
                Random r = new Random();
                sqlMap.InsertPerson = "insert into people values(" + r.Next().ToString() +
                    ",'" + txtName.Text + "','" + txtLastName.Text + "')";
            }
            pQueries.AddPerson(txtName.Text, txtLastName.Text);
            PopulatePeopleGrid();
        }

        private void frmSqlServerSampleOp_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instance = null;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(cbDatabase.SelectedItem.ToString().Equals("SQL Server"))
            {
                conString = ConfigurationManager.ConnectionStrings["EasyDataSQLSERVER"].ConnectionString;
                dbManagerFactory = new SQLDBManagerFactory(conString);
                storedProcMap = new SQLServerStoredProcMap();
                pQueries = new PersonQueries(dbManagerFactory, storedProcMap);
            }
            else if (cbDatabase.SelectedItem.ToString().Equals("Oracle"))
            {
                conString = ConfigurationManager.ConnectionStrings["EasyDataOracle"].ConnectionString;
                dbManagerFactory = new OracleDBManagerFactory(conString);
                sqlMap = new OracleSQLMap();
                sqlMap.GetAllPeople = "select * from people";
                pQueries = new PersonQueries(dbManagerFactory, sqlMap);
            }
            
            PopulatePeopleGrid();
        }
    }
}
