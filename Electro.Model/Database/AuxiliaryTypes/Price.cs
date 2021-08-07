using System;

namespace Electro.Model.Database.AuxiliaryTypes
{
    public class Price
    {
        private string _rangePriceString;

        public double To { get; private set; }

        public double Min { get; set; }

        public double Max { get; set; }

        public double From { get; private set; }

        public string RangePriceString
        {
            get { return _rangePriceString; }
            set
            {
                _rangePriceString = value;

                OnRangePriceStringChanged();
            }
        }

        public Price()
        {
            Min = 0;
            Max = 0;

            RangePriceString = "0;0";
        }

        public Price(double min, double max)
        {
            Min = min;
            Max = max;

            RangePriceString = $"{min};{max}";
        }

        private void OnRangePriceStringChanged()
        {
            if (!string.IsNullOrEmpty(RangePriceString))
            {
                var range = RangePriceString.Split(';');

                if (range != null ? range.Length == 2 : false)
                {
                    From = Convert.ToDouble(range[0]);
                    To = Convert.ToDouble(range[1]);
                }
            }
        }
    }
}
