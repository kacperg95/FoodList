using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ListApp
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsGood { get; set; }
        public DateTime CreationDate { get; set; }

        public static int LastID = 0;

        public Food()
        {
            LastID++;
        }

       public Food(int Id, string Name, string Category, bool IsGood, DateTime CreationDate)
        {
            this.Id = Id;
            this.Name = Name;
            this.Category = Category;
            this.IsGood = IsGood;
            this.CreationDate = CreationDate;

            LastID++;
        }

    }
}
