using System.Windows;

namespace MedicalReferral
{
    public partial class AddReferralWindow : Window
    {
        public AddReferralWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var referral = new Referral
            {
                PatientName = PatientNameTextBox.Text,
                DoctorName = DoctorNameTextBox.Text,
                ExaminationType = ExaminationTypeTextBox.Text,
                Date = DatePicker.SelectedDate ?? System.DateTime.Now
            };
            DatabaseHelper.AddReferral(referral);
            this.Close();
        }
    }
}
