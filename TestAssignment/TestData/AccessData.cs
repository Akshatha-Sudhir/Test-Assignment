using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestAssignment.TestData
{
    class AccessData
    {
        public string read(string path,int row)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode node = doc.SelectSingleNode("/TestData/Data" + row);
            string read_value = node.InnerText;

            return read_value;

            
        }
    }
}
