

namespace UserManager
{
    abstract class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string ID { get; set; }
        public abstract string GetInfo();

    }
}
