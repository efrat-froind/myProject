using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
    public class Client {
        public int Id { get; init; }
        public string UserName { get; set; }
        public string? Address { get; set; }
        public int? Phone { get; set; }

        public Client(int id,string userName,string address,int phone) 
        { 
           Id = id;
           UserName = userName;
           Address = address;
           Phone = phone;
        }
}

