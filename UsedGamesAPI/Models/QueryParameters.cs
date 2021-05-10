namespace UsedGamesAPI.Models
{
    public class QueryParameters
    {
        private int _limit;

        private int _offset;

        public int Offset
        {
            get => _offset;
            set
            {
                _offset = value < 0 ? 0 : value;
            }
        }

        public int Limit
        {
            get => _limit;
            set
            {
                _limit = value > 50 ? 50 : (value < 0 ? 1 : value);
            }
        }
    }
}
