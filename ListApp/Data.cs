using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ListApp
{
    class Data
    {

        ObservableCollection<string> categories = new ObservableCollection<string>();
        public ObservableCollection<string> Categories
        {
            get { return categories;  }
        }

        ObservableCollection<Food> foods = new ObservableCollection<Food>();
        public ObservableCollection<Food> Foods
        {
            get
            {
                return foods;
            }
        }

        public void AddCategory(string category)
        {
            categories.Add(category);
        }

        public void RemoveCategory(string category)
        {
            categories.Remove(category);
            foods = foods.Where(x => x.Category != category).ToObservableCollection<Food>();
            
        }

        public void MoveCategoryUp(int index)
        {
            if(index != 0)
                categories.Move(index, index - 1);
        }

        public void MoveCategoryDown(int index)
        {
            if(index != categories.Count - 1)
            categories.Move(index, index + 1);
        }


       public void Save()
        {
            XmlSerializer categorySerializer = new XmlSerializer(typeof(ObservableCollection<String>));
            XmlSerializer foodSerializer = new XmlSerializer(typeof(ObservableCollection<Food>));
            using (TextWriter categoryWriter = new StreamWriter(@"CategoryXml.xml"))
            using (TextWriter foodWriter = new StreamWriter(@"FoodXml.xml"))
            {
                categorySerializer.Serialize(categoryWriter, categories);
                foodSerializer.Serialize(foodWriter, foods);
            }
        }



        public void Load()
        {
            XmlSerializer categoryDeserializer = new XmlSerializer(typeof(ObservableCollection<string>));
            XmlSerializer foodDeserializer = new XmlSerializer(typeof(ObservableCollection<Food>));

            try
            {
                using (TextReader categoryReader = new StreamReader(@"CategoryXml.xml"))
                { 
                    object obj = categoryDeserializer.Deserialize(categoryReader);
                    categories = (ObservableCollection<string>)obj;
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(@"CategoryXml.xml");
            }

            try
            {
                using (TextReader foodReader = new StreamReader(@"FoodXml.xml"))
                {
                    object obj = foodDeserializer.Deserialize(foodReader);
                    foods = (ObservableCollection<Food>)obj;
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(@"FoodXml.xml");
            }

        }

    }
}
