using CountryInfo.EF;
using CountryInfo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace CountryInfo
{
    public partial class Form1 : Form
    {
        //List<City> listCities = new List<City>();
        //List<CountryRegion> listRegion = new List<CountryRegion>();
        //List<Country> listCountries = new List<Country>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tmp = textBox1.Text;
            string countryName = $"https://restcountries.eu/rest/v2/name/{tmp}";
            GetData(countryName);
        }

        void GetData(string name)
        {
            try
            {
                WebClient webClient = new WebClient();
                string str = webClient.DownloadString(name);
                str = str.Substring(1, str.Length - 2);
                Restcoutry rcountry = Newtonsoft.Json.JsonConvert.DeserializeObject<Restcoutry>(str);

                label8.Text = rcountry.Name;
                label9.Text = rcountry.Alpha2Code;
                label10.Text = rcountry.Capital;
                label11.Text = rcountry.Area.ToString();
                label12.Text = rcountry.Population.ToString();
                label13.Text = rcountry.Region;
                if(MessageBox.Show("Загрузить информацию в базу?","",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    WriteData();
                }
            }
            catch { MessageBox.Show("Страна с таким именем не найдена"); }

        }

        void WriteData()
        {
            City city = new City();
            CountryRegion region = new CountryRegion();
            Country country = new Country();
            region.Name = label13.Text;
            city.Name = label10.Text;
            country.Name = label8.Text;
            country.Code = label9.Text;
            country.Capital = city;
            country.Area = double.Parse(label11.Text);
            country.Population =int.Parse(label12.Text);
            country.Region = region;

            using (CountryDataContext db = new CountryDataContext())
            {
                db.Cities.Add(city);
                db.Regions.Add(region);
                db.Countries.Add(country);
                db.SaveChanges();
                MessageBox.Show("Save ok!");
            }
        }



        void LoadData()
        {
            City city = new City();
            CountryRegion region = new CountryRegion();
            Country country = new Country();
            listView1.Items.Clear();
            using (CountryDataContext db = new CountryDataContext())
            {
               foreach(var item in db.Countries.Include(t=>t.Capital).Include(t=>t.Region))
                {

                    ListViewItem viewItem = new ListViewItem();
                    viewItem.Text = item.Name;
                    viewItem.SubItems.Add(item.Code.ToString());
                    viewItem.SubItems.Add(item.Capital.Name);
                    viewItem.SubItems.Add(item.Area.ToString());
                    viewItem.SubItems.Add(item.Population.ToString());
                    viewItem.SubItems.Add(item.Region.Name);
                    listView1.Items.Add(viewItem);

                }
             
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
