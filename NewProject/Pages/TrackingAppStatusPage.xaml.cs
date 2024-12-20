using System;
using System.Collections.Generic;
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

namespace NewProject.Pages {
    public partial class TrackingAppStatusPage :Page {
        public TrackingAppStatusPage() {
            InitializeComponent();
            try {
                var context = UP_0101Entities3.GetContext();

                dgApps.ItemsSource = context.Application.ToList();

                cbClient.ItemsSource = context.Client.Select(x => x.ClientName).ToList();
                cbStatus.ItemsSource = context.AppStatus.Select(x => x.StatusName).ToList();
                cbWorker.ItemsSource = context.Worker.Select(x => x.WorkerName).ToList();
            }
            catch {
                MessageBox.Show("При загрузке данных произошла ошибка");
            }
        }

        private void tbAppNum_TextChanged(object sender, TextChangedEventArgs e) {
            ChangeData();
        }

        private void cbClient_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ChangeData();
        }

        private void cbWorker_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ChangeData();
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ChangeData();
        }

        public void ChangeData() {
            try {
                var context = UP_0101Entities3.GetContext();
                var items   = UP_0101Entities3.GetContext().Application.ToList();

                if(tbAppNum.Text != string.Empty) {
                    items = items.Where(x => x.ID == int.Parse(tbAppNum.Text)).ToList();
                }

                if(cbClient.SelectedItem != null) {
                    items = items.Where(x => x.Client ==
                    int.Parse(context.Client.Where(y => cbClient.SelectedItem.ToString() == y.ClientName).Select(y => y.ID).First().ToString())
                    ).ToList();
                }

                if(cbWorker.SelectedItem != null) {
                    items = items.Where(x => x.Responsible ==
                    int.Parse(context.Worker.Where(y => cbWorker.SelectedItem.ToString() == y.WorkerName).Select(y => y.ID).First().ToString())
                    ).ToList();
                }

                if(cbStatus.SelectedItem != null) {
                    items = items.Where(x => x.AppStatus ==
                    int.Parse(context.AppStatus.Where(y => cbStatus.SelectedItem.ToString() == y.StatusName).Select(y => y.ID).First().ToString())
                    ).ToList();
                }

                dgApps.ItemsSource = null;
                dgApps.ItemsSource = items;
            }
            catch {
                MessageBox.Show("При обновлении данных произошла ошибка");
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            tbAppNum.Text         = string.Empty;
            cbClient.SelectedItem = null;
            cbStatus.SelectedItem = null;
            cbWorker.SelectedItem = null;
            try {
                var context = UP_0101Entities3.GetContext();

                dgApps.ItemsSource = context.Application.ToList();
            }
            catch {
                MessageBox.Show("При загрузке данных произошла ошибка");
            }
        }
    }
}
