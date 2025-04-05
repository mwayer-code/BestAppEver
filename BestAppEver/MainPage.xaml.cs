
namespace BestAppEver
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            PopulateDatePickers();
            PopulateCountryPicker();
        }

        private void PopulateDatePickers()
        {
            // Populate Year Picker
            for (int year = DateTime.Now.Year; year >= 1900; year--)
            {
                YearPicker.Items.Add(year.ToString());
            }

            // Populate Month Picker
            for (int month = 1; month <= 12; month++)
            {
                MonthPicker.Items.Add(month.ToString());
            }

            // Populate Day Picker
            for (int day = 1; day <= 31; day++)
            {
                DayPicker.Items.Add(day.ToString());
            }

            // Set default values
            YearPicker.SelectedIndex = 0;
            MonthPicker.SelectedIndex = DateTime.Now.Month - 1;
            DayPicker.SelectedIndex = DateTime.Now.Day - 1;
        }
        private void PopulateCountryPicker()
        {
            var countries = new List<String>
            {
                "United States",
                "Indonesia",
                "Egypt",
                "Iceland",
                "Japan",
                "Sweden",
                "Australia",
                "Canada",
                "France",
                "India",
                "Austria",
                "Germany",
                "Belgium",

            };

            foreach (var country in countries)
            {
                CountryPicker.Items.Add(country);
            }
        }

        private int GetDrinkingAgeByCountry()
        {
            string? selectedCountry = CountryPicker.SelectedItem?.ToString();
            if (selectedCountry == null)
            {
                return 21; //default
            }

            switch (selectedCountry)
            {
                case "United States":
                    return 21;
                case "Indonesia":
                    return 21;
                case "Egypt":
                    return 21;
                case "Iceland":
                    return 20;
                case "Japan":
                    return 20;
                case "Sweden":
                    return 20;
                case "Canada":
                    return 18;
                case "Australia":
                    return 18;
                case "France":
                    return 18;
                case "India":
                    return 18;
                case "Austria":
                    return 16;
                case "Germany":
                    return 16;
                case "Belgium":
                    return 16;
                default:
                    return 21; 
            }
        }

        void Btn_Clicked(object sender, EventArgs e)
        {
            if (YearPicker.SelectedItem == null || MonthPicker.SelectedItem == null || DayPicker.SelectedItem == null)
            {
                Lbl_bday.Text = "Please select a valid date.";
                return;
            }

            DateTime bday = new(
                int.Parse(YearPicker.SelectedItem?.ToString() ?? "0"),
                int.Parse(MonthPicker.SelectedItem?.ToString() ?? "0"),
                int.Parse(DayPicker.SelectedItem?.ToString() ?? "0")
            );

            DateTime today = DateTime.Now;
            TimeSpan ts = today - bday;
            int years = (int)(ts.TotalDays / 365.25);
            int months = (int)((ts.TotalDays % 365.25) / 30.44);
            int days = (int)((ts.TotalDays % 365.25) % 30.44);

            int legalDrinkingAge = GetDrinkingAgeByCountry();
            bool canDrink = years >= GetDrinkingAgeByCountry();
            if (canDrink)
            {
                string messageCanDrink = "You are old enough to drink. Cheers!";
                Lbl_canDrinkMessage.Text = messageCanDrink;
            }
            else
            {
                DateTime legalDrinkingDate = bday.AddYears(legalDrinkingAge);
                TimeSpan timeUntilLegal = legalDrinkingDate - today;
                int yearsUntilLegal = (int)(timeUntilLegal.TotalDays / 365.25);
                int monthsUntilLegal = (int)((timeUntilLegal.TotalDays % 365.25) / 30.44);
                int daysUntilLegal = (int)((timeUntilLegal.TotalDays % 365.25) % 30.44);

                string messageCanDrink = $"You are not old enough to drink. You will be able to drink legally in {yearsUntilLegal} years, {monthsUntilLegal} months, and {daysUntilLegal} days.";
                Lbl_canDrinkMessage.Text = messageCanDrink;

            }

                
        }


    }

}
