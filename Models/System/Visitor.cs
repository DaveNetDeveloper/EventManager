namespace EventManager.Models.System
{
    public class Visitor
    {
        #region [ ctors. ]

        public Visitor() { }
        public Visitor(int id, string ip, string ciudad, string region)
        {
            Id = id;
            IP = ip;
            Ciudad = ciudad;
            Region = region;
        }

        #endregion

        #region [ public properties ]

        public int Id { get; set; }
        public string IP { get; set; }
        public string Ciudad { get; set; }
        public string Region { get; set; }

        #endregion
    }
}