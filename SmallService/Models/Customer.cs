namespace SmallService.Models
{
    public class Customer
    {
        private decimal _score;

        public long CustomerId { get; set; }

        public decimal Score 
        {
            get
            {
                return _score;
            }

            set
            {
                _score = value;
                if (_score < -1000)
                {
                    _score = -1000;
                }

                if (_score > 1000)
                {
                    _score = 1000;
                }
            }
        }
    }
}
