using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bits.Api.Tests.DataModel
{
    public class User
    {
        public string title {  get; set; }  
        public string firstName { get; set; }
        public string lastName { get; set; }    
        public string dateOfBirth {  get; set; }    
        public string email { get; set; }   
        public string password { get; set; }    
        public int rating { get; set; }
    }
}
