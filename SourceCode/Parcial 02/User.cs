namespace Parcial_02
{
    public class User
    {
        public string fullName { get; set;}
        public string userName { get; set;}
        public string password { get; set;}
        public bool userType { get; set;}
        
        
        

        public User()
        {
            fullName = "";
            userName = "";
            password = "";
            userType = false;
        }
    }
}