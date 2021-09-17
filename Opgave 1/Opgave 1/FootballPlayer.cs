using System;

namespace Opgave_1
{
    public class FootballPlayer
    {
        private int _ID;
        private string _Name;
        private int _Price;
        private int _ShirtNumber;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
            }
        }

        public string Name
        {
            get => _Name;
            set
            {
                if (value.Length < 4) { throw new ArgumentOutOfRangeException("Name must be 4 char min."); }
                    _Name = value;
            }
        }

        public int Price
        {
            get => _Price;
            set
            {
                if(value <= 0) { throw new ArgumentOutOfRangeException("Positive numbers only"); }
                _Price = value;
            }
        }

        public int ShirtNumber
        {
            get => _ShirtNumber;
            set
            {
                if(value < 0 || value > 100) { throw new ArgumentOutOfRangeException("Must be between 1 and 100"); }
                _ShirtNumber = value;
            }
        }

        public override string ToString()
        {
            return $"{_ID} - {_Name}: Worth: {_Price} - Number: {_ShirtNumber}";
        }
    }
}
