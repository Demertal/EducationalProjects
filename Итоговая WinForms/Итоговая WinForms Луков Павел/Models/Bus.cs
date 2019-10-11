using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using Итоговая_WinForms_Луков_Павел.Interfaces;

namespace Итоговая_WinForms_Луков_Павел.Models
{
    [DataContract]
    public class Bus : Updating, IValidating
    {
        public event UpdatingHandler UpdateHandler;

        private int _id;
        [DataMember]
        public int Id
        {
            get => _id;
            set
            {
                if(!Directory.Exists("...\\Images\\" + value))
                {
                    Directory.CreateDirectory("...\\Images\\" + value);
                    if (Directory.Exists("...\\Images\\" + _id))
                    {
                        string[] file = Directory.GetFiles("...\\Images\\" + _id);
                        foreach (var f in file)
                        {
                            string[] temp = f.Split('\\');
                            File.Copy(f, "...\\Images\\" + value + "\\" + temp[temp.Length - 1]);
                            File.Delete(f);
                        }

                        Directory.Delete("...\\Images\\" + _id);
                    }
                }
                _id = value;
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        private int _idBrandBus;
        [DataMember]
        public int IdBrandBus
        {
            get => _idBrandBus;
            set
            {
                _idBrandBus = value;
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        private string _stateNumber;
        [DataMember]
        public string StateNumber
        {
            get => _stateNumber;
            set
            {
                _stateNumber = value;
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        private int _yearIssue;
        [DataMember]
        public int YearIssue
        {
            get => _yearIssue;
            set
            {
                _yearIssue = value;
                UpdateHandler?.Invoke(this, new UpdateEventArgs(IsValidated));
            }
        }

        public bool IsValidated =>
            Id != 0 && IdBrandBus != 0 && !string.IsNullOrEmpty(StateNumber) && YearIssue >= 1990;

        public Bus()
        {
            Directory.CreateDirectory("...\\Images\\" + Id);
            Image im = Resource1.Image1;
            FileStream fs = File.Create("...\\Images\\" + Id + "\\" + "1.png");
            im.Save(fs, ImageFormat.Bmp);
            fs.Close();
            fs = File.Create("...\\Images\\" + Id + "\\" + "2.png");
            im.Save(fs, ImageFormat.Bmp);
            fs.Close();
            fs = File.Create("...\\Images\\" + Id + "\\" + "3.png");
            im.Save(fs, ImageFormat.Bmp);
            fs.Close();
        }
    }
}
