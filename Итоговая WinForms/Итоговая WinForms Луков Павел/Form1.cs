using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using Итоговая_WinForms_Луков_Павел.Interfaces;
using Итоговая_WinForms_Луков_Павел.Models;

namespace Итоговая_WinForms_Луков_Павел
{
    public partial class Form1 : Form
    {
        private const string BrandBusFileName = "brandBus.json",
            BusFileName = "bus.json",
            BusStopFileName = "busStop.json",
            RouteFileName = "route.json";

        public static string BrandBusFileNameGet => BrandBusFileName;

        public Form1()
        {
            InitializeComponent();
            TCMain.SelectedIndex = 3;
            LoadAll();
        }

        private void MIInit_Click(object sender, EventArgs e)
        {
            Init();
            LoadAll();
        }

        private void LoadAll()
        {
            LoadBrandBus();
            LoadBus();
            LoadBusStop();
            LoadRoute();
        }

        private void Init()
        {
            try
            {
                if (Directory.Exists("...\\Images"))
                {
                    string[] dir = Directory.GetDirectories("...\\Images");
                    foreach (var d in dir)
                    {
                        string[] files = Directory.GetFiles(d);
                        foreach (var f in files)
                        {
                            File.Delete(f);
                        }

                        Directory.Delete(d);
                    }
                }
                List<BrandBus> br = new List<BrandBus>();
                for (int i = 1; i <= 5; i++)
                {
                    br.Add(new BrandBus {Id = i, Title = "Марка" + i});
                }
                List<Bus> b = new List<Bus>();
                Random rand = new Random();
                for (int i = 1, j = 1; i <= 10; i++, j++)
                {
                    if (j == 6) j = 1;
                    string temp = "";
                    for (int l = 0; l < 5; l++)
                    {
                        temp += rand.Next(0, 9);
                    }
                    b.Add(new Bus { Id = i, IdBrandBus = j, YearIssue = rand.Next(1990, 2010), StateNumber = temp});
                }
                List<BusStop> bs = new List<BusStop>();
                for (int i = 1; i <= 20; i++)
                {
                    bs.Add(new BusStop { Id = i, Title = "Остановка" + i });
                }
                List<Route> r = new List<Route>();
                for (int i = 1; i <= 5; i++)
                {
                    ObservableCollection<RouteBusStop> temp = new ObservableCollection<RouteBusStop>();
                    for (int j = 1; j <= 5; j++)
                    {
                        temp.Add(new RouteBusStop { Id = j, IdBusStop = rand.Next(1, 20)});
                    }
                    r.Add(new Route { Id = i, IdBus = i, IdBusStopList = temp});
                }
                Utils.Save(br, BrandBusFileName);
                Utils.Save(b, BusFileName);
                Utils.Save(bs, BusStopFileName);
                Utils.Save(r, RouteFileName);
                
                SSTLmain.Text = "Данные инициализированы";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region BrandBus
        
        //Загрузка марок автобуса из файла
        private void LoadBrandBus()
        {
            try
            {
                if (!File.Exists(BrandBusFileName)) return;
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<BrandBus>));
                using (FileStream fs = new FileStream(BrandBusFileName, FileMode.Open))
                {
                    var list = (List<BrandBus>)jsonFormatter.ReadObject(fs);
                    foreach (var ob in list)
                    {
                        ob.UpdateHandler += UpdateBrandBus;
                    }
                    brandBusBindingSource.SuspendBinding();
                    brandBusBindingSource.DataSource = list;
                    brandBusBindingSource.ResumeBinding();
                }
                SSTLmain.Text = "Марки автобусов загружены";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Обработчик кнопки на добавление марок автобуса
        private void BNBrandBusAddNewItem_Click(object sender, EventArgs e)
        {
            try
            {
                DGVBusBrand["IdDGVBusBrand", DGVBusBrand.RowCount - 1].Value = Utils.GetMaxId(brandBusBindingSource.DataSource as List<BrandBus>) + 1;
                (brandBusBindingSource.DataSource as List<BrandBus>)[(brandBusBindingSource.DataSource as List<BrandBus>).Count - 1].UpdateHandler += UpdateBrandBus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Обработчик кнопки на удаление марок автобуса

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(DGVBusBrand.SelectedRows.Count == 0)return;
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить марку", "Предупредждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) != DialogResult.Yes) return;
                LoadBus();

                brandBusBindingSource.SuspendBinding();
                foreach (DataGridViewRow row in DGVBusBrand.SelectedRows)
                {
                    int count = (busBindingSource.DataSource as List<Bus>).Count(bus => bus.IdBrandBus == (row.DataBoundItem as BrandBus).Id);
                    if (count == 0)
                    {
                        brandBusBindingSource.Remove(row.DataBoundItem as BrandBus);
                        Utils.Save(brandBusBindingSource.DataSource as List<BrandBus>, BrandBusFileName);
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить выбранную марку так как она используется в таблице Автобусы", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                }
                brandBusBindingSource.ResumeBinding();
                MessageBox.Show("Марка удалена", "Сообщение", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Проверка валидности значений
        private void DGVBusBrand_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            string err = "";
            if (DGVBusBrand[1, e.RowIndex].Value == null)
                err = "Не введена марка";
            DGVBusBrand.Rows[e.RowIndex].ErrorText = err;
            e.Cancel = !string.IsNullOrEmpty(err);
        }

        //Событие при изменении значений
        private void UpdateBrandBus(object sender, UpdateEventArgs e)
        {
            if (!e.IsValidated) return;
            Utils.Save(brandBusBindingSource.DataSource as List<BrandBus>, BrandBusFileName);
        }
        #endregion

        #region Bus

        //Загрузка автобусов из файла
        private void LoadBus()
        {
            try
            {
                if (!File.Exists(BusFileName)) return;
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Bus>));

                using (FileStream fs = new FileStream(BusFileName, FileMode.Open))
                {

                    var list = (List<Bus>)jsonFormatter.ReadObject(fs);
                    foreach (var ob in list)
                    {
                        ob.UpdateHandler += UpdateBus;
                    }
                    busBindingSource.SuspendBinding();
                    busBindingSource.DataSource = list;
                    busBindingSource.ResumeBinding();
                }
                SSTLmain.Text = "Автобусы загружены";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBus(object sender, UpdateEventArgs e)
        {
            if (!e.IsValidated) return;
            Utils.Save(busBindingSource.DataSource as List<Bus>, BusFileName);
        }

        //Обработчик кнопки на добавление автобуса
        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            try
            {
                DGVBus["IdDGVBus", DGVBus.RowCount - 1].Value = Utils.GetMaxId(busBindingSource.DataSource as List<Bus>) + 1;
                DGVBus[1, DGVBus.RowCount - 1].Value = 1;
                (busBindingSource.DataSource as List<Bus>)[(busBindingSource.DataSource as List<Bus>).Count - 1].UpdateHandler += UpdateBus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Обработчик кнопки на удаление автобуса
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (DGVBus.SelectedRows.Count == 0) return;
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить автобус", "Предупредждение", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) != DialogResult.Yes) return;
                LoadRoute();

                busBindingSource.SuspendBinding();
                foreach (DataGridViewRow row in DGVBus.SelectedRows)
                {
                    int count = (routeBindingSource.DataSource as List<Route>).Count(route => route.IdBus == (row.DataBoundItem as Bus).Id);
                    if (count == 0)
                    {
                        if (Directory.Exists("...\\Images\\" + (row.DataBoundItem as Bus).Id))
                        {
                            string[] file = Directory.GetFiles("...\\Images\\" + (row.DataBoundItem as Bus).Id);
                            foreach (var f in file)
                            {
                                File.Delete(f);
                            }
                            Directory.Delete("...\\Images\\" + (row.DataBoundItem as Bus).Id);
                        }
                        busBindingSource.Remove(row.DataBoundItem as Bus);
                        Utils.Save(busBindingSource.DataSource as List<Bus>, BusFileName);
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить выбранный автобус так как он используется в таблице Маршруты", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                busBindingSource.ResumeBinding();
                MessageBox.Show("Автобус удален", "Сообщение", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGVBus_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            string err = "";
            if (DGVBus[1, e.RowIndex].Value != null && (int)DGVBus[1, e.RowIndex].Value == 0)
                err = "Укажите марку автобуса";
            else if(DGVBus[2, e.RowIndex].Value != null && string.IsNullOrEmpty((string)DGVBus[2, e.RowIndex].Value))
                err = "Укажите гос. номер";
            else if(DGVBus[3, e.RowIndex].Value != null && (int)DGVBus[3, e.RowIndex].Value < 1990)
                err = "Год выпуска должен быть больше 1990";
            DGVBus.Rows[e.RowIndex].ErrorText = err;
            e.Cancel = !string.IsNullOrEmpty(err);
        }

        private void DGVBus_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FormBus fr = new FormBus(DGVBus.SelectedRows[0].DataBoundItem as Bus);
            fr.Show();
        }
        #endregion

        #region BusStop

        //Загрузка автобусных остановок из файла
        private void LoadBusStop()
        {
            try
            {
                if (!File.Exists(BusStopFileName)) return;
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<BusStop>));

                using (FileStream fs = new FileStream(BusStopFileName, FileMode.Open))
                {

                    var list = (List<BusStop>)jsonFormatter.ReadObject(fs);
                    foreach (var ob in list)
                    {
                        ob.UpdateHandler += UpdateBusStop;
                    }
                    busStopBindingSource.SuspendBinding();
                    busStopBindingSource.DataSource = list;
                    busStopBindingSource.ResumeBinding();
                }
                SSTLmain.Text = "Автобусные остановки загружены";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorAddNewItem2_Click(object sender, EventArgs e)
        {
            try
            {
                DGVBusStop["IdDGVBusStop", DGVBusStop.RowCount - 1].Value = Utils.GetMaxId(busStopBindingSource.DataSource as List<BusStop>) + 1;
                (busStopBindingSource.DataSource as List<BusStop>)[(busStopBindingSource.DataSource as List<BusStop>).Count - 1].UpdateHandler += UpdateBusStop;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if(DGVBusStop.SelectedRows.Count == 0)return;
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить остановку", "Предупредждение", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) != DialogResult.Yes) return;
                LoadRoute();

                busStopBindingSource.SuspendBinding();
                foreach (DataGridViewRow row in DGVBusStop.SelectedRows)
                {
                    int count = (routeBindingSource.DataSource as List<Route>).Count(route => route.IdBusStopList.Count(routeBusStop => routeBusStop.IdBusStop == (row.DataBoundItem as BusStop).Id) != 0);
                    if (count == 0)
                    {
                        busStopBindingSource.Remove(row.DataBoundItem as BusStop);
                        Utils.Save(busStopBindingSource.DataSource as List<BusStop>, BusStopFileName);
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить выбранную остановку так как она используется в таблице Маршруты", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                busStopBindingSource.ResumeBinding();
                MessageBox.Show("Остановка удалена", "Сообщение", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGVBusStop_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            string err = "";
            if (DGVBusStop[1, e.RowIndex].Value == null)
                err = "Не введено название остановки";
            DGVBusStop.Rows[e.RowIndex].ErrorText = err;
            e.Cancel = !string.IsNullOrEmpty(err);
        }

        private void UpdateBusStop(object sender, UpdateEventArgs e)
        {
            if (!e.IsValidated) return;
            Utils.Save(busStopBindingSource.DataSource as List<BusStop>, BusStopFileName);
        }

        #endregion

        #region Route

        //Загрузка маршрутов из файла
        private void LoadRoute()
        {
            try
            {
                if (!File.Exists(RouteFileName)) return;
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Route>));

                using (FileStream fs = new FileStream(RouteFileName, FileMode.Open))
                {

                    var list = (List<Route>)jsonFormatter.ReadObject(fs);
                    foreach (var ob in list)
                    {
                        ob.UpdateHandler += UpdateRoute;
                    }
                    idBusStopListBindingSource.SuspendBinding();
                    routeBindingSource.SuspendBinding();
                    routeBindingSource.DataSource = list;
                    routeBindingSource.ResumeBinding();
                    idBusStopListBindingSource.ResumeBinding();
                }
                SSTLmain.Text = "Маршруты загружены";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorAddNewItem3_Click(object sender, EventArgs e)
        {
            try
            {
                (routeBindingSource.DataSource as List<Route>)[(routeBindingSource.DataSource as List<Route>).Count - 1].UpdateHandler += UpdateRoute;
                DGVRoute["IdDGVRoute", DGVRoute.RowCount - 1].Value = Utils.GetMaxId(routeBindingSource.DataSource as List<Route>) + 1;
                DGVRoute[1, DGVRoute.RowCount - 1].Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorDeleteItem3_Click(object sender, EventArgs e)
        {
            Utils.Save(routeBindingSource.DataSource as List<Route>, RouteFileName);
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            string err = "";
            if (dataGridView1[0, e.RowIndex].Value == null)
                err = "Не указан номер остановки";
            else if((int)dataGridView1[0, e.RowIndex].Value == 0)
                err = "Номер остановки не может быть равен 0";
            if (dataGridView1[1, e.RowIndex].Value == null)
                err = "Не указана остановка";
            dataGridView1.Rows[e.RowIndex].ErrorText = err;
            e.Cancel = !string.IsNullOrEmpty(err);
        }

        private void DGVRoute_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVRoute.SelectedRows.Count != 0)
            {
                toolStripLabel1.Text = "Остановки маршрута № " + (DGVRoute.SelectedRows[0].DataBoundItem as Route).Id;
            }
            else
            {
                toolStripLabel1.Text = "";
            }
        }

        private void DGVRoute_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Bus b = new Bus();
            foreach (Bus ob in busBindingSource.List)
            {
                if (ob.Id == (DGVRoute.SelectedRows[0].DataBoundItem as Route).IdBus)
                {
                    b = ob;
                    break;
                }
            }
            FormBus fr = new FormBus(b);
            fr.Show();
        }

        private void DGVRoute_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            string err = "";
            if (DGVRoute[1, e.RowIndex].Value == null)
                err = "Не введено название остановки";
            DGVRoute.Rows[e.RowIndex].ErrorText = err;
            e.Cancel = !string.IsNullOrEmpty(err);
        }

        private void UpdateRoute(object sender, UpdateEventArgs e)
        {
            if (!e.IsValidated) return;
            Utils.Save(routeBindingSource.DataSource as List<Route>, RouteFileName);
            int id = routeBindingSource.Position;
            LoadRoute();
            routeBindingSource.Position = id;
        }

        #endregion
        private void TCMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TCMain.SelectedIndex)
            {
                case 0:
                    LoadBrandBus();
                    break;
                case 1:
                    LoadBrandBus();
                    LoadBus();
                    break;
                case 2:
                    LoadBusStop();
                    break;
                case 3:
                    tabControl1.SelectedIndex = 0;
                    LoadBus();
                    LoadBusStop();
                    LoadRoute();
                    break;
                default:
                    break;
            }
        }
    }
}
