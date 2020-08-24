//--------------------------------------------------------------------------
// @author:  Nico Solitana, Christian Kevin Villanueva
// @subject: Advanced Operating System
// @course:  MS Computer Science
// @university: De La Salle University - Manila
//--------------------------------------------------------------------------

namespace SerialSimulation.Models
{
    public class Person
    {
        private string _firstName;
        public string firstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        private string _lastName;
        public string lastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        private string _address;
        public string address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
    }
}
