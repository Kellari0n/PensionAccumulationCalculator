using PensionAccumulationCalculator.Forms.InsuranceRecord;
using PensionAccumulationCalculator.Forms.MiliraryRecord;
using PensionAccumulationCalculator.Forms.Users;
using PensionAccumulationCalculator.Forms.WorkRecord;
using PensionAccumulationCalculator.Repos.Implementations;
using PensionAccumulationCalculator.Services.Implementations;
using PensionAccumulationCalculator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PensionAccumulationCalculator.Forms
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void client_button_Click(object sender, EventArgs e)
        {
            UsersList form = new UsersList(new UserService(new UserRepo()));
            form.Show(this);
        }

        private void insurance_record_button_Click(object sender, EventArgs e)
        {
            InsuranceRecordList form = new InsuranceRecordList(new InsuranceRecordService(new InsuranceRecordRepo()));
            form.Show(this);
        }

        private void military_record_button_Click(object sender, EventArgs e)
        {
            MilitaryRecordList form = new MilitaryRecordList(new MilitaryRecordService(new MilitaryRecordRepo()));
            form.Show(this);
        }

        private void work_record_button_Click(object sender, EventArgs e)
        {
            WorkRecordList form = new WorkRecordList(new WorkRecordService(new WorkRecordRepo()));
            form.Show(this);
        }

        private void ipc_accum_button_Click(object sender, EventArgs e)
        {
            IndividualPensionCoefficientAccumulationList form = new IndividualPensionCoefficientAccumulationList();
            form.Show(this);
        }
    }
}
