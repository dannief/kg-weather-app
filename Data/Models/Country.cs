namespace KG.Weather.Data.Models
{
    public class Country: Enumeration<Country>
    {
        public static Country Jamaica =
            new Country("Jamaica");

        private Country(): base()
        {
        }

        private Country(string value):
            base(value)
        {
        }

        public Country Clone()
        {
            return new Country(Value);
        }
    }
}
