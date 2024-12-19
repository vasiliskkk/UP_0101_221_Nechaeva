using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace NewProject.Pages {
    public partial class EditAppPage :Page {
        Worker worker;
        public EditAppPage(Worker worker) {
            InitializeComponent();
            this.worker = worker;

            if(worker.WorkerRole == 1) {
                lblDueDate.Visibility = Visibility.Visible;
                tbDueDate.Visibility  = Visibility.Visible;
                lblWorker.Visibility  = Visibility.Visible;
                cbWorker.Visibility   = Visibility.Visible;
            }
            else {
                lblDueDate.Visibility = Visibility.Collapsed;
                tbDueDate.Visibility  = Visibility.Collapsed;
                lblWorker.Visibility  = Visibility.Collapsed;
                cbWorker.Visibility   = Visibility.Collapsed;
            }

            try{
                var context = UP_0101Entities1.GetContext();

                cbAppStatus.ItemsSource = context.AppStatus.Select(x => x.StatusName).ToList();
                cbWorker.ItemsSource    = context.Worker.Select(x => x.WorkerName).ToList();
            }
            catch {
                MessageBox.Show("При загрузке данных произошла ошибка");
            }
        }

        private void btnEditApplication_Click(object sender, RoutedEventArgs e) {
            if(worker.WorkerRole == 1) {
                if(string.IsNullOrEmpty(tbDescription.Text) ||
                    string.IsNullOrEmpty(tbDueDate.Text) ||
                    string.IsNullOrEmpty(cbAppStatus.Text) ||
                    string.IsNullOrEmpty(cbWorker.Text) ||
                    string.IsNullOrEmpty(tbAppNum.Text)
                ) {
                    MessageBox.Show("Не все поля для изменения заполнены!");
                    return;
                }

                try {
                    Application CurrentApp = UP_0101Entities1.GetContext().Application.ToList().Where(x => x.ID == int.Parse(tbAppNum.Text)).First();
                    if(CurrentApp == null) return;

                    var db = new UP_0101Entities1();

                    CurrentApp.AppDescription = tbDescription.Text;
                    CurrentApp.AppStatus      = int.Parse(db.AppStatus.Where(x => cbAppStatus.Text == x.StatusName).Select(x => x.ID).First().ToString());
                    CurrentApp.Responsible    = int.Parse(db.Worker.Where(x => cbWorker.Text == x.WorkerName).Select(x => x.ID).First().ToString());
                    CurrentApp.DueDate        = DateTime.Parse(tbDueDate.Text);

                    if(CurrentApp.AppStatus == 4) CurrentApp.DateOfEnd = DateTime.Now;

                    db.Application.AddOrUpdate(CurrentApp);
                    db.SaveChanges();

                    MessageBox.Show("Заявка успешно изменена!");

                    tbDueDate.Text           = string.Empty;
                    tbDescription.Text       = string.Empty;
                    cbAppStatus.SelectedItem = null;
                    cbWorker.SelectedItem    = null;
                }
                catch {
                    MessageBox.Show("При попытке изменить заявку произошла ошибка");
                }
                
            }
            else {
                if(string.IsNullOrEmpty(tbDescription.Text) ||
                    string.IsNullOrEmpty(cbAppStatus.Text) ||
                    string.IsNullOrEmpty(tbAppNum.Text)
                ) {
                    MessageBox.Show("Не все поля для изменения заполнены!");
                    return;
                }

                try {
                    Application CurrentApp = UP_0101Entities1.GetContext().Application.ToList().Where(x => x.ID == int.Parse(tbAppNum.Text)).First();
                    if(CurrentApp == null) return;

                    var db = new UP_0101Entities1();

                    CurrentApp.AppDescription = tbDescription.Text;
                    CurrentApp.AppStatus      = int.Parse(db.AppStatus.Where(x => cbAppStatus.Text == x.StatusName).Select(x => x.ID).First().ToString());

                    if(CurrentApp.AppStatus == 4) CurrentApp.DateOfEnd = DateTime.Now;

                    db.Application.AddOrUpdate(CurrentApp);
                    db.SaveChanges();

                    MessageBox.Show("Заявка успешно изменена!");

                    tbDescription.Text       = string.Empty;
                    cbAppStatus.SelectedItem = null;
                }
                catch {
                    MessageBox.Show("При попытке изменить заявку произошла ошибка");
                }
            }
        }

        private void tbAppNum_TextChanged(object sender, TextChangedEventArgs e) {
            Application CurrentApp;
            try {
                CurrentApp = UP_0101Entities1.GetContext().Application.ToList().Where(x => x.ID == int.Parse(tbAppNum.Text)).First();
            }
            catch {
                MessageBox.Show("Заявки с таким ID не существует");
                return;
            }

            if(CurrentApp == null) return;

            tbDescription.Text       = CurrentApp.AppDescription;
            cbAppStatus.SelectedItem = UP_0101Entities1.GetContext().AppStatus.ToList().Where(x => x.ID == CurrentApp.AppStatus).Select(x => x.StatusName).First();
            cbWorker.SelectedItem    = UP_0101Entities1.GetContext().Worker.ToList().Where(x => x.ID == CurrentApp.Responsible).Select(x => x.WorkerName).First();
            tbDueDate.Text           = CurrentApp.DueDate.ToString();
        }
    }
}
